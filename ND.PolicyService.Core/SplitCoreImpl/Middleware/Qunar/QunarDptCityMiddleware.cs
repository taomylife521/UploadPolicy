using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    /// <summary>
    /// 去哪儿拆分出发城市
    /// </summary>
    public class QunarDptCityMiddleware : SplitHandlerMiddleware
    {
        private ConcurrentBag<Policies> lstSplitTotal = new ConcurrentBag<Policies>();
       // ConcurrentBag<Policies> lst = new ConcurrentBag<Policies>();
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarDptCityMiddleware(SplitHandlerMiddleware next)
            : base(next)
        {
        }
        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                Policies policy = context.RequestPolicy;
               
                List<string> dptCity = policy.DptCity.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();//出发城市，限制只能为一个
                //List<string> dptCityNew = new List<string>();
                //dptCity.ForEach(x =>
                //{
                //    if(request.LstQunarCodes.Contains(x))
                //    {
                //        dptCityNew.Add(x);
                //    }
                //});
                //dptCity.Clear();
                //dptCity = dptCityNew;
                //if(dptCity.Count <= 0)
                //{
                //    return;
                //}
                List<Policies> lst = new List<Policies>();
                 if(policy.AirlineCode.ToUpper() == "G5")
                {
                    lst.Add(policy);
                    lst.ToList().ForEach(x =>
                    {
                        context = context.SetRequestPolicy(x);//重新设置请求政策
                        Next.Invoke(context);//交给下个拆分程序处理
                    });
                    return;
                }
                if (dptCity.Count <= request.MaxDptCityCount)
                {
                    //context.ResponsePolicy.Add(policy);
                    //lst.Add(policy);
                   
                        lst.Add(policy);
                        lst.ToList().ForEach(x =>
                        {
                            context = context.SetRequestPolicy(x);//重新设置请求政策
                            Next.Invoke(context);//交给下个拆分程序处理
                        });
                    return;
                }

                foreach (var item in dptCity)
                {
                    Policies pl = policy.DeepClone();
                    pl.DptCity = item.Replace("/", "").ToUpper();
                    lst.Add(pl);
                }
                lst.ForEach(y =>
                {
                    context = context.SetRequestPolicy(y);//重新设置请求政策
                    this.Invoke(context);//当前急需循环拆分

                });
            }
            catch(Exception ex)
            {
                LogContext logContext = new LogContext();
                string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicySplitService\\ErrSplitPolicyRec\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                logContext.AddLogInfo(logPath, JsonConvert.SerializeObject(context.RequestPolicy) + "\r\n错误信息:" + JsonConvert.SerializeObject(ex), true);
                return;
               // this.Invoke(context);//当前急需循环拆分
                
            }
            
        }
    }
}
