using ND.PolicyUploadService.Core.inter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.UploadPolicyImpl.Middleware
{
   public class EmptyHandlerMiddleware:HandlerMiddleware
    {
        /// <summary>
        /// 实例。
        /// </summary>
        public static readonly HandlerMiddleware Instance = new EmptyHandlerMiddleware(null);

        /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public EmptyHandlerMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }

        public EmptyHandlerMiddleware()
        {
        }

        #region Overrides of HandlerMiddleware

        /// <summary>
        /// 调用。
        /// </summary>
        /// <param name="context">处理上下文。</param>
        /// <returns>任务。</returns>
        public override void Invoke(IHandlerContext context)
        {
            context.UploadResponse.ErrCode = Enums.ResultType.Sucess;
            
        }

        #endregion Overrides of HandlerMiddleware
    }
}
