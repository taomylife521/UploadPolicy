
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web;

namespace ND.PolicyUploadService.Core.inter
{
    /// <summary>
    /// 一个抽象的处理上下文。
    /// </summary>
    public interface IHandlerContext
    {
        /// <summary>
        /// 一个请求。
        /// </summary>
        UpLoadPolicyRequest Request { get; set; }

        /// <summary>
        /// 最终响应给微信的Xml内容。
        /// </summary>
        UploadPolicyResponse UploadResponse { get; set; }

        /// <summary>
        /// 处理环境。
        /// </summary>
        IDictionary<string, object> Environment { get; }

        /// <summary>
        /// 从环境中得到一个值。
        /// </summary>
        /// <typeparam name="T">值类型。</typeparam>
        /// <param name="key">值的key。</param>
        /// <returns>值。</returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置一个环境值。
        /// </summary>
        /// <typeparam name="T">值类型。</typeparam>
        /// <param name="key">值的key。</param>
        /// <param name="value">具体值。</param>
        /// <returns>处理上下文。</returns>
        IHandlerContext Set<T>(string key, T value);
    }

    /// <summary>
    /// 一个默认的处理上下文。
    /// </summary>
    public sealed class HandlerContext : IHandlerContext
    {
       

        /// <summary>
        /// 初始化一个新的处理上下文。
        /// </summary>
        /// <param name="request">一个请求。</param>
        /// <exception cref="ArgumentNullException"><paramref name="request"/> 为null。</exception>
        public HandlerContext(UpLoadPolicyRequest request)
        {
            Request = request;
            Environment = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            UploadResponse = new UploadPolicyResponse();
            //设置默认的依赖解析器。
            //this.SetDependencyResolver(DefaultDependencyResolver.Instance);
        }

        public HandlerContext(UpLoadPolicyRequest request, ConcurrentDictionary<string, object> environment)
        {
            Request = request;
            Environment = environment;
            UploadResponse = new UploadPolicyResponse();
            //设置默认的依赖解析器。
            //this.SetDependencyResolver(DefaultDependencyResolver.Instance);
        }

        #region Implementation of IHandlerContext

        /// <summary>
        /// 一个请求。
        /// </summary>
        public UpLoadPolicyRequest Request { get; set; }

        /// <summary>
        /// 最终响应内容。
        /// </summary>
        public UploadPolicyResponse UploadResponse { get; set; }

        /// <summary>
        /// 处理环境。
        /// </summary>
        public IDictionary<string, object> Environment { get; private set; }

        /// <summary>
        /// 从环境中得到一个值。
        /// </summary>
        /// <typeparam name="T">值类型。</typeparam>
        /// <param name="key">值的key（不区分大小写）。</param>
        /// <returns>值。</returns>
        public T Get<T>(string key)
        {
            object value;
            if (Environment.TryGetValue(key, out value))
                return (T)value;

            return default(T);
        }

        /// <summary>
        /// 设置一个环境值。
        /// </summary>
        /// <typeparam name="T">值类型。</typeparam>
        /// <param name="key">值的key（不区分大小写）。</param>
        /// <param name="value">具体值。</param>
        /// <returns>处理上下文。</returns>
        public IHandlerContext Set<T>(string key, T value)
        {
            Environment[key] = value;

            return this;
        }

        #endregion Implementation of IHandlerContext
    }

    /// <summary>
    /// 处理上下文扩展方法。
    /// </summary>
    public static partial class HandlerContextExtensions
    {
       
        public static void SetRequest(this IHandlerContext context,UpLoadPolicyRequest request)
        {
            context.Request = request;
            
        }
        
        
    }
}