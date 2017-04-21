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
   public class QunarDivideTaskMiddleware:SplitHandlerMiddleware
    {
       public QunarDivideTaskMiddleware(SplitHandlerMiddleware next)
           : base(next)
       {

       }
        public override void Invoke(ISplitHandlerContext context)
        {
            try
            {
                ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();
                QunarSplitPolicyRequest request= context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                List<Policies> lstPolicies = request.Policies;
                Task.Factory.StartNew(() =>//开启管理线程分配任务
                {
                    int currentTaskCount = 0;
                    while (lstPolicies.Count > 0)
                    {
                        while (currentTaskCount >= request.SplitMaxTaskCount)
                        {
                            int index = Task.WaitAny(tasks.ToArray());//等待任何一个线程完成
                            currentTaskCount--;
                        }
                        List<Policies> childCollection = lstPolicies.Take(request.SplitPerTaskMaxCount).ToList();
                        if (childCollection.Count > 0)
                        {
                            lstPolicies.RemoveRange(0, childCollection.Count);
                            Task subTask = Task.Factory.StartNew(() =>//开启子线程处理
                            {
                                foreach (Policies item in childCollection)
                                {
                                    context.RequestPolicy = item;
                                    Next.Invoke(context);
                                }
                            });
                            currentTaskCount++;
                            tasks.Add(subTask);
                        }
                    }
                });
                //.ContinueWith(task =>
               // {
                    Task.WhenAll(tasks.ToArray());
               // });
               
            }
            catch (Exception ex)
            {
                LogContext logContext = new LogContext();
                string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicySplitService\\ErrSplitPolicyRec\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                logContext.AddLogInfo(logPath, JsonConvert.SerializeObject(context.RequestPolicy) + "\r\n错误信息:" + JsonConvert.SerializeObject(ex), true);
            }
        }

     
    }
}
