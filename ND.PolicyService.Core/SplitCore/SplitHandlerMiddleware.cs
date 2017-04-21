
using ND.PolicyUploadService.Core.inter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    /// <summary>
    /// 抽象的拆分中间件
    /// </summary>
    public abstract class SplitHandlerMiddleware 
    {
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        protected SplitHandlerMiddleware(SplitHandlerMiddleware next)
        {
            Next = next;
        }

        /// <summary>
        /// 下一个处理中间件。
        /// </summary>
        protected SplitHandlerMiddleware Next { get; private set; }

        /// <summary>
        /// 调用。
        /// </summary>
        /// <param name="context">处理上下文。</param>
        /// <returns>任务。</returns>
        public abstract void Invoke(ISplitHandlerContext context);

       
    }
}
