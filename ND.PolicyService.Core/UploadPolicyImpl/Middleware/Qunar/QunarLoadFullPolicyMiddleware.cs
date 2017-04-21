using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using ND.PolicyService.Core;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Core.SplitCoreImpl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.Core.impl.Middleware.Qunar
{
    /// <summary>
    /// 载入全量政策处理中间件
    /// </summary>
   public class QunarLoadFullPolicyMiddleware:HandlerMiddleware
    {
       public QunarLoadFullPolicyMiddleware(HandlerMiddleware next):base(next)
       {

       }
       public QunarLoadFullPolicyMiddleware()
        { }
        public override void Invoke(IHandlerContext context)
        {
            try
            {
                QunarUploadPolicyRequest qunarRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "开始获取去哪儿全量更新包..." });
                //PolicySyncRecLib syncLib = new PolicySyncRecLib();
                //PolicyRecord policyRec = CoreHelper.GetLastUpTimeAndId("Qunar\\QunarFullPolicyRecLog");//去哪儿全量选择日志
                #region 查询政策
                SearchPolicyRequest request = new SearchPolicyRequest()
                   {
                       CommisionMoney = qunarRequest.CommisionMoney,
                       CommsionPoint = qunarRequest.CommsionPoint,
                       IsUpload = true,
                       OperName = qunarRequest.OperName,
                       PageSize = qunarRequest.PageSize,
                       PolicyType = qunarRequest.PolicyType,
                       pType = PurchaserType.Qunar,
                       SqlWhere = qunarRequest.SqlWhere,
                       UType = qunarRequest.UploadType,
                       IsSearchTotalCount = false
                   };
                string searchContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchPolicyUrl"].ToString(), request);
                SearchPolicyResponse rep = JsonConvert.DeserializeObject<SearchPolicyResponse>(searchContent);//先查询一遍，获取要上传的政策
                PolicyRecord policyRec = rep.LastPolicyRecord;
                context.UploadResponse.BeforePolicyRecord = policyRec;
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "载入上次更新记录,更新时间:" + policyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "，上次最后一条更新id：" + policyRec.LastPolicyId.ToString() + ",收到全量政策包:" + rep.lstPolicies.Count + "条" });
                qunarRequest.PolicyDataOrgin = rep.lstPolicies;
                context.SetRequest(qunarRequest);
                Next.Invoke(context);
                #endregion
                #region 注释
                //bool isHaveAll = false;
                ////过滤政策
                //#region 过滤政策
                //int count = rep.lstPolicies == null ? 0 : rep.lstPolicies.Count;
                //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "查询到政策数量:" + count + " 条,开始过滤..." });
                //List<Policies> lstFiltedPolicy = CoreHelper.FilterPolicy(qunarRequest.LstQunarCodes, rep.lstPolicies, ref isHaveAll);
                //if (lstFiltedPolicy.Count <= 0)
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "经过去哪儿三子码过滤后未筛选到要上传的政策" });
                //    return;
                //}
                ////if (isHaveAll)
                ////{
                ////  return;
                ////DialogResult dr = MessageBox.Show("当前上传的政策有全国对全国的政策,是否继续上传", "提示", MessageBoxButtons.OKCancel);
                ////if (dr == DialogResult.Cancel)
                ////{
                ////    return;
                ////}
                ////} 
                //#endregion
                //List<Policies> lstAddPolicies = CoreHelper.SplitPolicy(request.pType, lstFiltedPolicy, qunarRequest.LstQunarCodes);//拆分政策
                //qunarRequest.UploadCount = lstAddPolicies.Count;
                //context.UploadResponse.BeforePolicyRecord = policyRec;
                //// List<Policies> lstAddPolicies = syncLib.LoadPolicy(context.Request.PageSize, policyRec, context.Request.SqlWhere, PurchaserType.Qunar, ref selectSql,ref totalCount, UploadType.FullUpload, context.Request.CommisionMoney, context.Request.CommsionPoint);//获取要上传的政策
                //// List<Policies> lstAddPolicies = syncLib.LoadPolicy(policyRec, context.Request.CommisionMoney, context.Request.CommsionPoint, context.Request.PageSize, context.Request.SqlWhere, ref selectSql, ref totalCount, UploadType.FullUpload);//获取要上传的政策


                ////if(!context.Request.IsPrintSql)
                ////{
                ////    selectSql = "";
                ////}
                ////selectSql = context.Request.IsPrintSql ? "\r\n筛选sql为:\r\n" + selectSql+"\r\n" : "";
                //if (lstAddPolicies.Count <= 0)
                //{
                //    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "未筛选到要上传的政策，或者筛选的政策已经上传！" });
                //    context.UploadResponse = new UploadPolicyResponse() { ErrCode = ResultType.Failed, ErrMsg = "未筛选到要上传的政策，或者筛选的政策已经上传！" };
                //    return;
                //} 
                #endregion
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = ND.PolicyService.Enums.RunStatus.Exception, Msg = "QunarLoadFullPolicyMiddleware:" + ex.Message, Exception = ex, PurchaserType = ND.PolicyService.Enums.PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ND.PolicyService.Enums.ResultType.Failed, ErrMsg = ex.Message, Excption = ex };
                return;
            }
            
            
            
        }

       

     
    }
}
