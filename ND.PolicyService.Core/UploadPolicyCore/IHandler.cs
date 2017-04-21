using ND.PolicyUploadService.Core.impl.Middleware;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyService.Core.UploadPolicyImpl.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.Core.inter
{
   public interface IHandler
    {
        /// <summary>
        /// 执行。
        /// </summary>
        /// <param name="context">处理上下文。</param>
        /// <returns>任务。</returns>
       void Execute(IHandlerContext context);
    }

   /// <summary>
   /// 默认的处理程序。
   /// </summary>
   public sealed class DefaultHandler : IHandler
   {
       #region Field

       private readonly IHandlerBuilder _builder;
       private static Action<object,EventMsg> _registerMiddlewareEventCall;

       #endregion Field

       #region Constructor

       /// <summary>
       /// 初始化一个新的默认微信处理程序。
       /// </summary>
       /// <param name="builder">处理构造者。</param>
       public DefaultHandler(IHandlerBuilder builder, Action<object,EventMsg> registerMiddlewareEventCall=null)
       {
           _builder = builder;
           _registerMiddlewareEventCall = registerMiddlewareEventCall;
       }

       #endregion Constructor

       #region Implementation of IHandler

       /// <summary>
       /// 执行。
       /// </summary>
       /// <param name="context">处理上下文。</param>
       /// <returns>任务。</returns>
       public void Execute(IHandlerContext context)
       {
           var middlewareItems = (ICollection<KeyValuePair<HandlerMiddleware, object[]>>)_builder.Properties["ND.PolicyUploadService.Middlewares"];

           var fristMiddleware = GetFirstMiddleware(middlewareItems);

            fristMiddleware.Invoke(context);
       }

       #endregion Implementation of IHandler

       #region Private Method

       private static HandlerMiddleware GetFirstMiddleware(ICollection<KeyValuePair<HandlerMiddleware, object[]>> middlewareItems)
       {
           if (middlewareItems.Count == 0)
               return EmptyHandlerMiddleware.Instance;

           var middlewares = new List<HandlerMiddleware>();
           foreach (var item in middlewareItems.Reverse())
           {
               var lastMiddleware = middlewares.LastOrDefault() ?? EmptyHandlerMiddleware.Instance;

               IEnumerable<object> args = new object[] { lastMiddleware };
               if (item.Value != null && item.Value.Any())
               {
                   args = args.Concat(item.Value);
               }

               middlewares.Add(GetHandlerMiddleware(item.Key, args.ToArray()));
           }
          
               middlewares.ForEach(x =>
               {
                   x.MiddlewareWorking += x_MiddlewareWorking;
               });
           
           middlewares.Reverse();
           return middlewares.FirstOrDefault();
       }

       static void x_MiddlewareWorking(object sender, EventMsg e)
       {
           _registerMiddlewareEventCall(sender, e);
       }

     

       private static HandlerMiddleware GetHandlerMiddleware(object middleware, object[] args)
       {
           #region 旧代码
           //if (middleware is HandlerMiddleware)
           //{
           //    return middleware as HandlerMiddleware;
           //}

           //if (middleware is Type)
           //{
           //    var type = middleware as Type;
           //    if (!typeof(HandlerMiddleware).IsAssignableFrom(type))
           //        throw new NotSupportedException("无法将类型：" + type.FullName + "，注册为处理中间件。");
           //    Type[] types = new Type[1];
           //    types[0] = typeof(HandlerMiddleware);
           //    var constructor = type.GetConstructor(types);
           //    return constructor.Invoke(args) as HandlerMiddleware;
           //}

           //throw new NotSupportedException("无法将类型：" + middleware.GetType().FullName + "，注册为处理中间件。"); 
           #endregion

           if (middleware is HandlerMiddleware)
           {
               //var type = middleware as Type;
               //if (!typeof(HandlerMiddleware).IsAssignableFrom(type))
               //    throw new NotSupportedException("无法将类型：" + type.FullName + "，注册为处理中间件。");
               Type type=middleware.GetType();
               Type[] types = new Type[1];
               types[0] = typeof(HandlerMiddleware);
               var constructor = type.GetConstructor(types);
               
               return constructor.Invoke(args) as HandlerMiddleware;
           }

           throw new NotSupportedException("无法将类型：" + middleware.GetType().FullName + "，注册为处理中间件。"); 
       }

       #endregion Private Method
   }
}
