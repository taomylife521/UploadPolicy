using ND.PolicyUploadService.Core.impl.Middleware;
using ND.PolicyUploadService.Core.impl.Middleware.Qunar;
using ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    /// <summary>
    /// 一个抽象的处理程序。
    /// </summary>
    public interface ISplitHandler
    {
        /// <summary>
        /// 执行。
        /// </summary>
        /// <param name="context">处理上下文。</param>
        /// <returns>任务。</returns>
        void Execute(ISplitHandlerContext context);
    }
    public class SplitHandler : ISplitHandler
    {
        private readonly ISplitHandlerBuilder _builder;
        public SplitHandler(ISplitHandlerBuilder builder)
        {
            _builder = builder;
        }
        public void Execute(ISplitHandlerContext context)
        {
            var middlewareItems = (ICollection<KeyValuePair<object, object[]>>)_builder.Properties["NDPolicySplit.Middlewares"];

            var fristMiddleware = GetFirstMiddleware(middlewareItems);

             fristMiddleware.Invoke(context);
        }

        private static SplitHandlerMiddleware GetFirstMiddleware(ICollection<KeyValuePair<object, object[]>> middlewareItems)
        {
            if (middlewareItems.Count == 0)
                return EmptySplitHandlerMiddleware.Instance;

            var middlewares = new List<SplitHandlerMiddleware>();
            foreach (var item in middlewareItems.Reverse())
            {
                var lastMiddleware = middlewares.LastOrDefault() ?? EmptySplitHandlerMiddleware.Instance;

                IEnumerable<object> args = new object[] { lastMiddleware };
                if (item.Value != null && item.Value.Any())
                {
                    args = args.Concat(item.Value);
                }

                middlewares.Add(GetHandlerMiddleware(item.Key, args.ToArray()));
            }
            middlewares.Reverse();
            return middlewares.FirstOrDefault();
        }

        private static SplitHandlerMiddleware GetHandlerMiddleware(object middleware, object[] args)
        {
            if (middleware is SplitHandlerMiddleware)
            {
                return middleware as SplitHandlerMiddleware;
            }

            if (middleware is Type)
            {
                var type = middleware as Type;
                if (!typeof(SplitHandlerMiddleware).IsAssignableFrom(type))
                    throw new NotSupportedException("无法将类型：" + type.FullName + "，注册为处理中间件。");

                var constructor = type.GetConstructors().First();
                return constructor.Invoke(args) as SplitHandlerMiddleware;
            }

            throw new NotSupportedException("无法将类型：" + middleware.GetType().FullName + "，注册为处理中间件。");
        }
    }
}
