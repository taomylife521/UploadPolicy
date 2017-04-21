using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using ND.PolicyService.Core.SplitCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    public class QunarFlightOutMiddleware : SplitHandlerMiddleware
    {
        public QunarFlightOutMiddleware(SplitHandlerMiddleware next)
           : base(next)
       {

       }

        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                Policies policy = context.RequestPolicy;
                string[] flightOut = policy.FlightOut.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToArray();//最多为25个 //适用航班

                List<Policies> lst = new List<Policies>();
                if (flightOut.Length <= request.MaxFlightInCount)
                {
                    lst.Add(policy);
                    lst.ToList().ForEach(x =>
                    {
                        context = context.SetRequestPolicy(x);
                        Next.Invoke(context);
                    });
                    return;
                }
                int yuShu = flightOut.Length % request.MaxFlightInCount;//余数
                int shang = flightOut.Length / request.MaxFlightInCount;//商
                shang = yuShu != 0 ? shang + 1 : shang;
                for (int i = 0; i < shang; i++)
                {
                    string[] groupArrCity = flightOut.ToList().Skip(i * request.MaxFlightInCount).Take(request.MaxFlightInCount).ToArray();
                    Policies pl = policy.DeepClone();
                    pl.FlightOut = string.Join(",", groupArrCity);
                    lst.Add(pl);
                }
                lst.ForEach(y =>
                {
                    context = context.SetRequestPolicy(y);
                    this.Invoke(context);
                });
            }
            catch (Exception ex)
            {
                LogContext logContext = new LogContext();
                string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicySplitService\\ErrSplitPolicyRec\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                logContext.AddLogInfo(logPath, JsonConvert.SerializeObject(context.RequestPolicy) + "\r\n错误信息:" + JsonConvert.SerializeObject(ex), true);
                //this.Invoke(context);//当前急需循环拆分
                return;
            }

        }

    }
}
