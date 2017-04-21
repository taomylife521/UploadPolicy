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
    public class QunarFullDispatcherMiddleware : HandlerMiddleware
    {
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarFullDispatcherMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }

        public QunarFullDispatcherMiddleware()
        {
        }

        public override void Invoke(IHandlerContext context)
        {
            try
            {
                #region 上传全量政策
                QunarUploadPolicyRequest qunarRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);
                Policies policy = qunarRequest.PolicyDataOrgin.LastOrDefault();
                PolicyRecord LastPolicyRec = new PolicyRecord { LastPolicyId = 0, LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) };
                context.UploadResponse.PolicyRec[UploadType.FullUpload] = LastPolicyRec;
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "收到全量政策" + qunarRequest.PolicyDataOrgin.Count + "条,开始分批上传" });
                context.SetRequest(qunarRequest);
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "当前全量政策为" + qunarRequest.PolicyDataOrgin.Count + "条,不用分批上传" });
                qunarRequest.PolicyData.Add(UploadTypeDetail.FullUpload, qunarRequest.PolicyDataOrgin);
                Next.Invoke(context);

                #endregion
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Exception, Msg = "QunarFullDispatcherMiddleware:" + ex.Message, Exception = ex, PurchaserType = Enums.PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Failed, ErrMsg = ex.Message, Excption = ex };
                return;
            }
        }
    }
}
