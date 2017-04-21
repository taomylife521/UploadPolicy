using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.UploadPolicyImpl.Middleware.Qunar
{
    public class QunarIncrementalDispatcherMiddleware : HandlerMiddleware
    {
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarIncrementalDispatcherMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }

        public QunarIncrementalDispatcherMiddleware()
        {
        }

        public override void Invoke(IHandlerContext context)
        {
            try
            {
                QunarUploadPolicyRequest qunarRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);
                PolicyRecord lastPolicyRec = new PolicyRecord() { LastPolicyId = 0, LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) };
                context.UploadResponse.PolicyRec[UploadType.Incremental] = lastPolicyRec;//保存上次更新记录
                #region 判断是否分批并交由下个中间件处理
                int upLoadCount = System.Configuration.ConfigurationManager.AppSettings["MaxUploadCount"] == null ? 1000 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxUploadCount"].ToString());
                if (qunarRequest.PolicyDataOrgin.Count < upLoadCount)//小于10000，自动上传
                {
                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "当前政策为" + qunarRequest.PolicyDataOrgin.Count + "条,小于最高限制条数:" + upLoadCount + "条,不用分批上传" });
                    List<Policies> lstAddPolicies = qunarRequest.PolicyDataOrgin.Where(x => x.DelDegree == 1).ToList();
                    List<Policies> lstDelPolicies = qunarRequest.PolicyDataOrgin.Where(x => x.DelDegree == 0).ToList();
                    qunarRequest.PolicyData.Add(UploadTypeDetail.IncrementalAdd, lstAddPolicies);
                    qunarRequest.PolicyData.Add(UploadTypeDetail.IncrementalDelete, lstDelPolicies);
                    context.SetRequest(qunarRequest);
                    Next.Invoke(context);

                }
                else
                {
                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "当前政策为" + qunarRequest.PolicyDataOrgin.Count + "条,大于最高限制条数:" + upLoadCount + "条,开始分批上传.." });
                    int index = 1;
                    while (qunarRequest.PolicyDataOrgin.Count > 0)
                    {
                        List<Policies> lstPolicies = new List<Policies>();
                        lstPolicies = qunarRequest.PolicyDataOrgin.Take(upLoadCount).ToList();//取一万条先上传
                        qunarRequest.UploadCount = lstPolicies.Count;
                        if (lstPolicies.Count > 0)
                        {
                            qunarRequest.PolicyDataOrgin.RemoveRange(0, lstPolicies.Count);
                        }
                        OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "收到政策数量:" + lstPolicies.Count + "条,开始第" + index + "次分批上传" });
                        List<Policies> lstAddPolicies = lstPolicies.Where(x => x.DelDegree == 1).ToList();
                        List<Policies> lstDelPolicies = lstPolicies.Where(x => x.DelDegree == 0).ToList();
                        PolicyRecord rec = new PolicyRecord() { LastPolicyId = lstPolicies.LastOrDefault().Id, LastUpdateTime = lstPolicies.LastOrDefault().UpdateTime };
                        context.UploadResponse.BeforePolicyRecord = rec;//每次都保留上回更新的记录
                        qunarRequest.PolicyData.Remove(UploadTypeDetail.IncrementalAdd);//先移除后添加，防止key冲突
                        qunarRequest.PolicyData.Remove(UploadTypeDetail.IncrementalDelete);
                        qunarRequest.PolicyData.Add(UploadTypeDetail.IncrementalAdd, lstAddPolicies);
                        qunarRequest.PolicyData.Add(UploadTypeDetail.IncrementalDelete, lstDelPolicies);
                        context.SetRequest(qunarRequest);
                        if (index <= 1)
                        {
                            Next.Invoke(context);
                        }
                        else
                        {
                            //先判断是否上传成功
                            OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "查询是否上传成功..." });
                           bool flag = SearchNotifyStatus(context.UploadResponse.UploadStatusId);
                            if(flag)
                            {
                                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "上传成功,自动进入下次上传..." });
                                Next.Invoke(context);
                            }
                            else
                            {
                                while(!flag)
                                {
                                    Thread.Sleep(2000);//休息俩秒继续查询
                                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "上传中,继续查询是否上传成功..." });
                                    flag = SearchNotifyStatus(context.UploadResponse.UploadStatusId);
                                    if (flag)
                                    {
                                        flag = false;
                                        OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "上传成功,自动进入下次上传..." });  
                                    }
                                }
                                Next.Invoke(context);
                            }
                           
                        }

                        index++;
                    }
                }
                #endregion
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Exception, Msg = "QunarIncrementalDispatcherMiddleware:" + ex.Message, Exception = ex, PurchaserType = Enums.PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ResultType.Failed, ErrMsg = "QunarIncrementalDispatcherMiddleware:" + ex.Message, Excption = ex };
                return;
            }
        }

        private bool SearchNotifyStatus(string uploadStatusId)
        {
            SearchNotifyRequest request = new SearchNotifyRequest() { UploadStatusId = uploadStatusId };
            string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["PolicyNotifyUrl"].ToString(), request);

            SearchNotifyResponse rep = JsonConvert.DeserializeObject<SearchNotifyResponse>(responseContent);
            if (rep.ErrCode == ResultType.Failed)
            {
                return true;
            }
            else
            {
                if (rep.NotifyList[0].NotifyResult == 1 || rep.NotifyList[0].NotifyResult == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
