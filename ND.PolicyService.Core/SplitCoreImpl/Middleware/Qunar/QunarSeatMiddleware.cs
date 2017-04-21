using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using Newtonsoft.Json;
//**********************************************************************
//
// 文件名称(File Name)：QunarSeatMiddleware.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/1/5 9:49:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/1/5 9:49:27          
//             修改理由：         
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar
{
    public class QunarSeatMiddleware : SplitHandlerMiddleware
    {
         //List<Policies> lstArr = new List<Policies>();
       // ConcurrentBag<Policies> lstArr = new ConcurrentBag<Policies>();
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarSeatMiddleware(SplitHandlerMiddleware next)
            : base(next)
        {
        }

        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                Policies policy = context.RequestPolicy;
                List<string> seatList = policy.Seat.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();//抵达城市,最多为25个
                List<Policies> lstArr = new List<Policies>();

                if (seatList.Count <= request.MaxSeatCount)//已经不可拆分，添加到最终处理的集合并交给下一项条件拆分
                {

                    lstArr.Add(policy);
                    lstArr.ToList().ForEach(x =>
                    {
                        context = context.SetRequestPolicy(x);
                        Next.Invoke(context);
                    });
                    return;

                }
                string[] seatArr = policy.Seat.Split('/');
                for (int i = 0; i < seatArr.Length; i++)
                {
                    
                    Policies pl = policy.DeepClone();
                    pl.Seat = seatArr[i];
                    lstArr.Add(pl);
                }
                lstArr.ForEach(y =>//循环遍历自己
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
               // this.Invoke(context);
                return;
            }


        }
    }
}
