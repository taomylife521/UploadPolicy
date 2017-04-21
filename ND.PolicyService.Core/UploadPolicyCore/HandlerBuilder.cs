﻿using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace ND.PolicyUploadService.Core.inter
{
    /// <summary>
    /// 一个抽象的处理构造者。
    /// </summary>
    public interface IHandlerBuilder
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
        IHandlerBuilder Use(HandlerMiddleware middleware, params object[] args);

      
    }

    /// <summary>
    /// 处理构造者实现。
    /// </summary>
    public sealed class HandlerBuilder : IHandlerBuilder
    {
        #region Field
        private  readonly IList<KeyValuePair<HandlerMiddleware, object[]>> _middlewares;
      

        #endregion Field

        #region Constructor

        /// <summary>
        /// 初始化一个新的处理构造者。
        /// </summary>
        public HandlerBuilder()
        {
            Properties = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Properties["ND.PolicyUploadService.Middlewares"] = _middlewares = new List<KeyValuePair<HandlerMiddleware, object[]>>();
        }

        #endregion Constructor

        #region Implementation of IHandlerBuilder

        /// <summary>
        /// 属性字典。
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }

        /// <summary>
        /// 使用一个处理中间件。
        /// </summary>
        /// <param name="middleware">处理中间件实例。</param>
        /// <param name="args">参数。</param>
        /// <returns>处理构造者。</returns>
        public IHandlerBuilder Use(HandlerMiddleware middleware, params object[] args)
        {
            _middlewares.Add(new KeyValuePair<HandlerMiddleware, object[]>(middleware, args));
          
            return this;
        }

        #endregion Implementation of IHandlerBuilder


}

    /// <summary>
    /// 处理构造者扩展方法。
    /// </summary>
    public static class HandlerBuilderExtensions
    {
        /// <summary>
        /// 使用一个处理中间件。
        /// </summary>
        /// <typeparam name="T">处理中间件类型。</typeparam>
        /// <param name="builder">处理构造者。</param>
        /// <param name="args">参数。</param>
        /// <returns>处理构造者。</returns>
        public static IHandlerBuilder Use<T>(this IHandlerBuilder builder, params object[] args) where T : HandlerMiddleware
        {
            return builder.Use((HandlerMiddleware)Activator.CreateInstance(typeof(T)), args);
        }
    }
}