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
    /// 去哪抵达城市处理中间件
    /// </summary>
    public class QunarArrCityMiddleware : SplitHandlerMiddleware
    {

        //List<Policies> lstArr = new List<Policies>();
       // ConcurrentBag<Policies> lstArr = new ConcurrentBag<Policies>();
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
         public QunarArrCityMiddleware(SplitHandlerMiddleware next)
            : base(next)
        {
        }
        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                Policies policy = context.RequestPolicy;
                List<string> arrCity = policy.ArrCity.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();//抵达城市,最多为25个
                //List<string> arrCityNew = new List<string>();
                //arrCity.ForEach(x =>
                //{
                //    if(request.LstQunarCodes.Contains(x))
                //    {
                //        arrCityNew.Add(x);
                //    }
                //});
                //arrCity.Clear();
                //arrCity = arrCityNew;
                //if(arrCity.Count <= 0)
                //{
                //    return;
                //}
              List<Policies> lstArr = new List<Policies>();
              if (policy.AirlineCode.ToUpper() == "G5")
              {
                  lstArr.Add(policy);
                  lstArr.ToList().ForEach(x =>
                  {
                      context = context.SetRequestPolicy(x);//重新设置请求政策
                      Next.Invoke(context);//交给下个拆分程序处理
                  });
                  return;
              }
                if (arrCity.Count <= request.MaxArrCityCount)//已经不可拆分，添加到最终处理的集合并交给下一项条件拆分
                {
                   
                        lstArr.Add(policy);
                        lstArr.ToList().ForEach(x =>
                        {
                            context = context.SetRequestPolicy(x);
                            Next.Invoke(context);
                        });
                   
                    return;

                }
                int yuShu = arrCity.Count % request.MaxArrCityCount;//余数
                int shang = arrCity.Count / request.MaxArrCityCount;//商
                shang = yuShu != 0 ? shang + 1 : shang;
                for (int i = 0; i < shang; i++)
                {
                    string[] groupArrCity = arrCity.ToList().Skip(i * request.MaxArrCityCount).Take(request.MaxArrCityCount).ToArray();
                    Policies pl = policy.DeepClone();
                    pl.ArrCity = string.Join(",", groupArrCity);
                    lstArr.Add(pl);
                }
                lstArr.ForEach(y =>//循环遍历自己
                {
                    context = context.SetRequestPolicy(y);
                    this.Invoke(context);
                });
            }
            catch(Exception ex)
            {
                LogContext logContext = new LogContext();
                 string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicySplitService\\ErrSplitPolicyRec\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                 logContext.AddLogInfo(logPath, JsonConvert.SerializeObject(context.RequestPolicy)+"\r\n错误信息:"+JsonConvert.SerializeObject(ex), true);
                 return;
            }

           
        }
    }
}
