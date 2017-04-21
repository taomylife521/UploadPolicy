using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.UploadPolicyImpl.Middleware
{
    /// <summary>
    /// 压缩zip文件响应
    /// </summary>
   public class PackageZipFileMiddleware:HandlerMiddleware
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public PackageZipFileMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }
        public PackageZipFileMiddleware()
        {
        } 
        #endregion
        public override void Invoke(IHandlerContext context)
        {
             
            try
            {
                Exception ex = new Exception();
                bool r = ZipHelper.ZipFile(context.UploadResponse.FormatPolicyFilePath, context.UploadResponse.FormatPolicyZipFilePath, ZipEnum.GZIP, ref ex);
                if (!r)
                {
                    OnMiddlewareWorking(new EventMsg { Msg = "压缩失败" + JsonConvert.SerializeObject(ex) });
                    context.UploadResponse = new ND.PolicyUploadService.DtoModel.UploadPolicyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "压缩文件失败,压缩路径:" + context.UploadResponse.FormatPolicyZipFilePath, Excption = ex };
                    return;
                }
                OnMiddlewareWorking(new EventMsg { Msg = "压缩成功,压缩路径:" + context.UploadResponse.FormatPolicyZipFilePath });
                Next.Invoke(context);
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Exception, Msg = "PackageZipFileMiddleware:" + ex.Message, Exception = ex, PurchaserType = PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ResultType.Failed, ErrMsg = "PackageZipFileMiddleware:" + ex.Message, Excption = ex };
                return;
            }
        }
    }
}
