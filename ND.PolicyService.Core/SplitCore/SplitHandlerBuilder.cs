using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    /// <summary>
    /// 一个抽象的拆分处理构造者。
    /// </summary>
    public interface ISplitHandlerBuilder
    {
        /// <summary>
        /// 属性字典。
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// 使用一个处理中间件。
        /// </summary>
        /// <param name="middleware">处理中间件实例。</param>
        /// <param name="args">参数。</param>
        /// <returns>处理构造者。</returns>
        ISplitHandlerBuilder Use(object middleware, params object[] args);
    }
    public class SplitHandlerBuilder : ISplitHandlerBuilder
    {
        private readonly IList<KeyValuePair<object, object[]>> _middlewares;
        public SplitHandlerBuilder()
        {
            Properties = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Properties["NDPolicySplit.Middlewares"] = _middlewares = new List<KeyValuePair<object, object[]>>();
        }
        /// <summary>
        /// 属性字典。
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }

        public ISplitHandlerBuilder Use(object middleware, params object[] args)
        {
            _middlewares.Add(new KeyValuePair<object, object[]>(middleware, args));

            return this;
        }
    }

    /// <summary>
    /// 处理构造者扩展方法。
    /// </summary>
    public static class SplitHandlerBuilderExtensions
    {
        /// <summary>
        /// 使用一个处理中间件。
        /// </summary>
        /// <typeparam name="T">处理中间件类型。</typeparam>
        /// <param name="builder">处理构造者。</param>
        /// <param name="args">参数。</param>
        /// <returns>处理构造者。</returns>
        public static ISplitHandlerBuilder Use<T>(this ISplitHandlerBuilder builder, params object[] args) where T : SplitHandlerMiddleware
        {
            return builder.Use(typeof(T), args);
        }
    }
}
