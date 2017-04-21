using ND.PolicyService.Core.SplitCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    public class EmptySplitHandlerMiddleware : SplitHandlerMiddleware
    {

        /// <summary>
        /// 实例。
        /// </summary>
        public static readonly SplitHandlerMiddleware Instance = new EmptySplitHandlerMiddleware(null);
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public EmptySplitHandlerMiddleware(SplitHandlerMiddleware next)
            : base(next)
        {
        }
        public override void Invoke(ISplitHandlerContext context)
        {
           
        }
    }
}
