using ND.PolicyReceiveService.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    #region 拆分上下文ISplitHandlerContext接口
    public interface ISplitHandlerContext
    {
        /// <summary>
        /// 一个请求。
        /// </summary>
        Policies RequestPolicy { get; set; }

        /// <summary>
        /// 最终响应拆分好的政策列表
        /// </summary>
        List<Policies> ResponsePolicy { get; set; }

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
        ISplitHandlerContext Set<T>(string key, T value);
    } 
    #endregion

   public class SplitHandlerContext:ISplitHandlerContext
    {
        #region Property
        public Policies RequestPolicy
        {
            get;
            set;
        }

        public List<Policies> ResponsePolicy
        {
            get;
            set;
        }
        public IDictionary<string, object> Environment
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        public SplitHandlerContext()
        {
            RequestPolicy = new Policies();
            ResponsePolicy = new List<Policies>();
        }

        public SplitHandlerContext(Dictionary<string, object> ev)
            : this()
        {
            Environment = ev;

        }

        public SplitHandlerContext(Policies pl,Dictionary<string, object> ev)
            : this()
        {
            RequestPolicy = pl;
            Environment = ev;

        } 
        #endregion

        #region 方法
        public T Get<T>(string key)
        {
            object value;
            if (Environment.TryGetValue(key, out value))
                return (T)value;

            return default(T);
        }

        public ISplitHandlerContext Set<T>(string key, T value)
        {
            Environment[key] = value;

            return this;
        } 
        #endregion
    }

     /// <summary>
    /// 处理上下文扩展方法。
    /// </summary>
   public static partial  class SplitHandlerContextExtensions
   {
       /// <summary>
       /// 设置依赖解析器。
       /// </summary>
       /// <param name="context">处理上下文。</param>
       /// <param name="dependencyResolver">依赖解析器实例。</param>
       /// <exception cref="ArgumentNullException"><paramref name="context"/> 为 null。</exception>
       /// <exception cref="ArgumentNullException"><paramref name="dependencyResolver"/> 为 null。</exception>
       /// <returns>处理上下文。</returns>
       public static ISplitHandlerContext SetRequestPolicy(this ISplitHandlerContext context, Policies pl)
       {
           context.RequestPolicy = pl;
           return context;
       }

   }
}
