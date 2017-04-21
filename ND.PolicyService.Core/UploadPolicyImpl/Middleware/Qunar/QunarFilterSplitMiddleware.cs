using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.UploadPolicyImpl.Middleware.Qunar
{
    public class QunarFilterSplitMiddleware : HandlerMiddleware
    {
          /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarFilterSplitMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }

        public QunarFilterSplitMiddleware() 
        {
        }


        public override void Invoke(IHandlerContext context)
        {
            try
            {
                #region 过滤政策
                QunarUploadPolicyRequest qunarIncrementRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);

                bool isHaveAll = false;
                List<Policies> lstFiltedPolicy = CoreHelper.FilterPolicy(qunarIncrementRequest.LstQunarCodes, qunarIncrementRequest.PolicyDataOrgin, ref isHaveAll);
                if (lstFiltedPolicy.Count <= 0)
                {
                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "经过去哪儿三子码过滤后未筛选到要上传的政策" });
                    context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Sucess };
                    return;
                }
                #endregion

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "过滤完成,政策数量:" + lstFiltedPolicy.Count + "条,开始拆分..." });

                #region 拆分政策
                PoliciesType policyType =(PoliciesType)Enum.Parse(typeof(PoliciesType),qunarIncrementRequest.PolicyType.ToString());
                List<Policies> lstIncrementalPolicies = new List<Policies>();
                List<Policies> lstValidPolicies = lstFiltedPolicy.Where(x => x.DelDegree == 1).ToList();
                List<Policies> lstNoValidPolicies = lstFiltedPolicy.Where(x => x.DelDegree == 0).ToList();
                lstIncrementalPolicies.AddRange(lstNoValidPolicies);
                if (lstValidPolicies.Count > 0)
                {
                    lstValidPolicies = CoreHelper.SplitPolicy(PurchaserType.Qunar, lstValidPolicies, qunarIncrementRequest.LstQunarCodes, policyType);
                    lstIncrementalPolicies.AddRange(lstValidPolicies);
                }
                qunarIncrementRequest.UploadCount = lstIncrementalPolicies.Count;
                #endregion

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "收到拆分政策" + lstIncrementalPolicies.Count.ToString() + "条政策包！开始上传..." });
                Policies policy = lstIncrementalPolicies.LastOrDefault();
                qunarIncrementRequest.PolicyDataOrgin = lstIncrementalPolicies;
                context.SetRequest(qunarIncrementRequest);
                Next.Invoke(context);
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Exception, Msg = "QunarFilterSplitMiddleware:" + ex.Message, Exception = ex, PurchaserType = Enums.PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ResultType.Failed, ErrMsg = "QunarFilterSplitMiddleware:"+ex.Message,Excption=ex };
                return;
            }
           
        }
    }
}
