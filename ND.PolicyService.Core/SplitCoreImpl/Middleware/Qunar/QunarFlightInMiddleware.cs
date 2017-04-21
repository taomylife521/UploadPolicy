using ND.PolicyReceiveService.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyReceiveService.Helper;
using Newtonsoft.Json;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyUploadService.DtoModel.SplitPolicy;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    /// <summary>
    /// 拆分适用航班处理中间件
    /// </summary>
    public class QunarFlightInMiddleware : SplitHandlerMiddleware
    {
        public QunarFlightInMiddleware(SplitHandlerMiddleware next)
           : base(next)
       {

       }

        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                Policies policy = context.RequestPolicy;
                string[] flightIn = policy.FlightIn.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToArray();//最多为25个 //适用航班

                List<Policies> lst = new List<Policies>();
                if (flightIn.Length <= request.MaxFlightInCount)
                {
                    lst.Add(policy);
                    lst.ToList().ForEach(x =>
                    {
                        context = context.SetRequestPolicy(x);
                        Next.Invoke(context);
                    });
                    return;
                }
                int yuShu = flightIn.Length % request.MaxFlightInCount;//余数
                int shang = flightIn.Length / request.MaxFlightInCount;//商
                shang = yuShu != 0 ? shang + 1 : shang;
                for (int i = 0; i < shang; i++)
                {
                    string[] groupArrCity = flightIn.ToList().Skip(i * request.MaxFlightInCount).Take(request.MaxFlightInCount).ToArray();
                    Policies pl = policy.DeepClone();
                    pl.FlightIn = string.Join(",", groupArrCity);
                    lst.Add(pl);
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
