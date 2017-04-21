using ND.PolicyReceiveService.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyReceiveService.Helper;
using Newtonsoft.Json;
using ND.PolicyService.Core.SplitCore;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    public class QunarFlightDateMiddleware : SplitHandlerMiddleware
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public QunarFlightDateMiddleware(SplitHandlerMiddleware next)
           : base(next)
       {

       }


        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                Policies policy = context.RequestPolicy;
                string effctDate = policy.FlightEffectDate.EnsureDateRight();//销售开始日期
                string expireDate = policy.FlightExpireDate.EnsureDateRight();//销售结束日期
                string forbidEffectDate = policy.FlightForbidEffectDate.EnsureDateRight();//销售禁止开始日期
                string forbidExpireDate = policy.FlightForbidExpireDate.EnsureDateRight(); //销售禁止结束日期

                List<Policies> lst = new List<Policies>();
                if (string.IsNullOrEmpty(forbidEffectDate) && string.IsNullOrEmpty(forbidExpireDate))
                {
                    //lst.Add(policy);
                    //lst.ToList().ForEach(x =>
                    //{
                    //    context = context.SetRequestPolicy(x);
                    //    Next.Invoke(context);
                    //});

                    //if (lst.Count > 0)
                    //{
                        lst.Add(policy);
                        lst.ToList().ForEach(x =>
                        {
                            context = context.SetRequestPolicy(x);
                            Next.Invoke(context);
                        });
                    //}
                    //else
                    //{
                    //    context = context.SetRequestPolicy(policy);
                    //    Next.Invoke(context);
                    //}
                    return;
                }
                if (!string.IsNullOrEmpty(forbidEffectDate))//不为空
                {
                    #region 判断禁止开始时间
                    if (Convert.ToDateTime(forbidEffectDate) >= Convert.ToDateTime(effctDate) && Convert.ToDateTime(forbidEffectDate) < Convert.ToDateTime(expireDate))
                    {
                        Policies pl = policy.DeepClone();
                        pl.FlightEffectDate = Convert.ToDateTime(effctDate);
                        pl.FlightExpireDate = Convert.ToDateTime(forbidEffectDate);
                        pl.FlightForbidEffectDate = Convert.ToDateTime("2099-12-30");
                        pl.FlightForbidExpireDate = Convert.ToDateTime("2099-12-30");
                        lst.Add(pl);

                    }
                    #endregion
                }
                if (!string.IsNullOrEmpty(forbidExpireDate))
                {
                    #region 判断禁止过期时间
                    if (Convert.ToDateTime(forbidExpireDate) > Convert.ToDateTime(effctDate) && Convert.ToDateTime(forbidExpireDate) <= Convert.ToDateTime(expireDate))
                    {
                        Policies pl = policy.DeepClone();
                        pl.FlightEffectDate = Convert.ToDateTime(forbidExpireDate);
                        pl.FlightExpireDate = Convert.ToDateTime(expireDate);
                        pl.FlightForbidEffectDate = Convert.ToDateTime("2099-12-30");
                        pl.FlightForbidExpireDate = Convert.ToDateTime("2099-12-30");
                        lst.Add(pl);

                    }
                    #endregion
                }
                lst.ForEach(y =>
                {
                    context = context.SetRequestPolicy(y);
                    this.Invoke(context);

                });
            }
            catch(Exception ex)
            {
                LogContext logContext = new LogContext();
                string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicySplitService\\ErrSplitPolicyRec\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                logContext.AddLogInfo(logPath, JsonConvert.SerializeObject(context.RequestPolicy) + "\r\n错误信息:" + JsonConvert.SerializeObject(ex), true);
               // this.Invoke(context);//当前急需循环拆分
                return;
            }
           
        }
    }
}
