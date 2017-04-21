
using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyUploadService.DtoModel.RealTimeUpload;
using ND.PolicyService.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;

namespace ND.PolicyUploadService.Core.impl.Middleware.Qunar
{
    /// <summary>
    /// 获取去哪儿增量政策
    /// </summary>
    public class QunarLoadIncrementalPolicyMiddleware : HandlerMiddleware
    {
        public QunarLoadIncrementalPolicyMiddleware(HandlerMiddleware next)
            : base(next)
       {

       }
        public QunarLoadIncrementalPolicyMiddleware()
        { }
        public override void Invoke(IHandlerContext context)
        {
            try
            {

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "开始获取去哪儿增量更新包..." });
                QunarUploadPolicyRequest qunarIncrementRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);


                #region 查询增量政策
                SearchPolicyRequest request = new SearchPolicyRequest()
                    {
                        CommisionMoney = qunarIncrementRequest.CommisionMoney,
                        CommsionPoint = qunarIncrementRequest.CommsionPoint,
                        IsUpload = true,
                        OperName = qunarIncrementRequest.OperName,
                        PageSize = qunarIncrementRequest.PageSize,
                        PolicyType = qunarIncrementRequest.PolicyType,
                        pType = PurchaserType.Qunar,
                        SqlWhere = qunarIncrementRequest.SqlWhere,
                        UType = qunarIncrementRequest.UploadType,
                        IsSearchTotalCount = false,

                    };
                #region 如果是实时上传则根据上传过的去哪儿记录过滤要上传的记录
                //if (qunarIncrementRequest.IsRealTimeUpload)
                //{
                //   // OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "开始获取实时更新时间..." });
                //    request.IsRealTimeUpload = true;//是否实时更新


                //    //SearchRealTimeUploadResponse realTimeRep = JsonConvert.DeserializeObject<SearchRealTimeUploadResponse>(CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchRealTimeUploadUrl"].ToString(), request));//获取实时上传的记录
                //    //if(realTimeRep.ErrCode == ResultType.Failed)
                //    //{
                //    //    return;
                //    //}
                //    //request.RealTimePolicyRec = realTimeRep.PolicyRec;
                //    //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "获取到实时更新时间：" + realTimeRep.PolicyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") });
                //}
                #endregion
                string searchContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchPolicyUrl"].ToString(), request);
                string selectSql = "";
                int totalCount = 0;
                SearchPolicyResponse rep = JsonConvert.DeserializeObject<SearchPolicyResponse>(searchContent);//先查询一遍，获取要上传的政策
                PolicyRecord policyRec = rep.LastPolicyRecord;
                context.UploadResponse.BeforePolicyRecord = policyRec;
                if (rep.lstPolicies.Count <= 0)
                {

                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "暂时没有收到增量更新包" });
                    return;
                }
                #endregion

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "收到增量更新包:" + rep.lstPolicies.Count + "条,开始过滤..." });
                List<Policies> lstPoliciesOrgin = rep.lstPolicies;
                #region 如果是实时上传则根据上传过的去哪儿记录过滤要上传的记录
                //if (qunarIncrementRequest.IsRealTimeUpload)
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "获取上次更新时间：" + rep.LastPolicyRecord.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") });
                //    //更新到的政策里面去筛选出已经上传的政策Id
                //  CompleteUploadPolicyResponse completePolicyRep = JsonConvert.DeserializeObject<CompleteUploadPolicyResponse>(CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchCompleteUploadUrl"].ToString(), request));
                //    //把没有上传的政策删除掉
                //  if (completePolicyRep.ErrCode == ResultType.Failed || completePolicyRep.CompleteUploadPolicyCollection.Count <= 0)
                //  {
                //      OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "该更新包不在上传去哪儿的上传列表中,不用更新" });
                //      return;
                //  }
                //  lstPoliciesOrgin = CoreHelper.ReserveHaveUploadPolicy(lstPoliciesOrgin, completePolicyRep.CompleteUploadPolicyCollection);  
                //    if(lstPoliciesOrgin.Count <= 0)
                //    {
                //        OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "该更新包不在上传去哪儿的上传列表中,不用更新" });
                //        return;
                //    }
                //}
                #endregion
                selectSql = context.Request.IsPrintSql ? "\r\n去哪儿获取增量数据sql:" + selectSql + "\r\n" : "";

                if (lstPoliciesOrgin.Count <= 0)
                {
                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = selectSql + "暂时没有增量政策更新包！" });
                    return;
                }
                qunarIncrementRequest.PolicyDataOrgin = lstPoliciesOrgin;
                context.UploadResponse.BeforePolicyRecord = rep.LastPolicyRecord;
                context.SetRequest(qunarIncrementRequest);
                Next.Invoke(context);
                #region 旧代码
                //#region 过滤政策
                //bool isHaveAll = false;
                //List<Policies> lstFiltedPolicy = CoreHelper.FilterPolicy(qunarIncrementRequest.LstQunarCodes, lstPoliciesOrgin, ref isHaveAll);
                //if (lstFiltedPolicy.Count <= 0)
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "经过去哪儿三子码过滤后未筛选到要上传的政策" });
                //    return;
                //}
                //#endregion

                //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "过滤完成,政策数量:" + lstFiltedPolicy.Count + "条,开始拆分..." });

                //#region 拆分政策
                //List<Policies> lstIncrementalPolicies = CoreHelper.SplitPolicy(request.pType, lstFiltedPolicy, qunarIncrementRequest.LstQunarCodes);
                //qunarIncrementRequest.UploadCount = lstIncrementalPolicies.Count;

                //selectSql = context.Request.IsPrintSql ? "\r\n去哪儿获取增量数据sql:" + selectSql + "\r\n" : "";

                //if (lstIncrementalPolicies.Count <= 0)
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = selectSql + "暂时没有增量政策更新包！" });
                //    return;
                //}
                //#endregion



                //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = selectSql + "收到拆分政策" + lstIncrementalPolicies.Count.ToString() + "条增量政策更新包！开始上传..." });
                //Policies policy = lstIncrementalPolicies.LastOrDefault();
                //PolicyRecord lastPolicyRec = new PolicyRecord() { LastPolicyId = 0, LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) };
                //context.UploadResponse.PolicyRec[UploadType.Incremental] = lastPolicyRec;//保存上次更新记录

                //#region 判断是否分批并交由下个中间件处理
                //int upLoadCount = System.Configuration.ConfigurationManager.AppSettings["MaxUploadCount"] == null ? 1000 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxUploadCount"].ToString());
                //if (lstIncrementalPolicies.Count < upLoadCount)//小于10000，自动上传
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "当前增量政策为" + lstIncrementalPolicies.Count + "条,小于最高限制条数:" + upLoadCount + "条,不用分批上传" });
                //    List<Policies> lstAddPolicies = lstIncrementalPolicies.Where(x => x.DelDegree == 1).ToList();
                //    List<Policies> lstDelPolicies = lstIncrementalPolicies.Where(x => x.DelDegree == 0).ToList();
                //    qunarIncrementRequest.PolicyData.Add(UploadTypeDetail.IncrementalAdd, lstAddPolicies);
                //    qunarIncrementRequest.PolicyData.Add(UploadTypeDetail.IncrementalDelete, lstDelPolicies);
                //    context.SetRequest(qunarIncrementRequest);
                //    Next.Invoke(context);

                //}
                //else
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "当前增量政策为" + lstIncrementalPolicies.Count + "条,大于最高限制条数:" + upLoadCount + "条,开始分批上传.." });
                //    int index = 1;
                //    while (lstIncrementalPolicies.Count > 0)
                //    {
                //        List<Policies> lstPolicies = new List<Policies>();
                //        lstPolicies = lstIncrementalPolicies.Take(upLoadCount).ToList();//取一万条先上传
                //        qunarIncrementRequest.UploadCount = lstPolicies.Count;
                //        if (lstPolicies.Count > 0)
                //        {
                //            lstIncrementalPolicies.RemoveRange(0, lstPolicies.Count);
                //        }
                //        OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "收到增量政策数量:" + lstPolicies.Count + "条,开始第" + index + "次分批上传" });
                //        List<Policies> lstAddPolicies = lstIncrementalPolicies.Where(x => x.DelDegree == 1).ToList();
                //        List<Policies> lstDelPolicies = lstIncrementalPolicies.Where(x => x.DelDegree == 0).ToList();
                //        PolicyRecord rec = new PolicyRecord() { LastPolicyId = lstPolicies.LastOrDefault().Id, LastUpdateTime = lstPolicies.LastOrDefault().UpdateTime };
                //        context.UploadResponse.BeforePolicyRecord = rec;//每次都保留上回更新的记录
                //        qunarIncrementRequest.PolicyData.Remove(UploadTypeDetail.IncrementalAdd);//先移除后添加，防止key冲突
                //        qunarIncrementRequest.PolicyData.Remove(UploadTypeDetail.IncrementalDelete);
                //        qunarIncrementRequest.PolicyData.Add(UploadTypeDetail.IncrementalAdd, lstAddPolicies);
                //        qunarIncrementRequest.PolicyData.Add(UploadTypeDetail.IncrementalDelete, lstDelPolicies);
                //        context.SetRequest(qunarIncrementRequest);
                //        Next.Invoke(context);
                //        index++;
                //    }
                //}
                //#endregion 
                #endregion
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = ND.PolicyService.Enums.RunStatus.Exception, Msg = "QunarLoadIncrementalPolicyMiddleware:" + ex.Message, Exception = ex, PurchaserType = ND.PolicyService.Enums.PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ND.PolicyService.Enums.ResultType.Failed, ErrMsg = ex.Message, Excption = ex };
                return;
            }

           
          
            
            
            
        }
    }
}
