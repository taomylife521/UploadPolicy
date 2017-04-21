using ND.PolicyReceiveService.Factory.ReceiveFac;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Core.HandlerPolicy
{
    public class HandlerFor19e : IHandlerForPolicy
    {
       private CancellationTokenSource importCts = new CancellationTokenSource();//是否取消

       public event EventHandler<Model.EventMessage> onWorklingMsg;
        public IRecPolicy Recp
        {
            get;
            set;
        }
      
        public HandlerFor19e(GlobalConfig cf)
        {
           config = cf;
         
           Config19e cf19e = new Config19e();
           if (cf is Config19e)
           {
               cf19e = cf as Config19e;
           }
             this.Recp = new RecPolicy19eFactory().Create(cf19e);
            this.Recp.onWorklingMsg += Recp_onWorklingMsg;
        }

        void Recp_onWorklingMsg(object sender, EventMessage e)
        {
            if (onWorklingMsg != null)
            {
                onWorklingMsg(sender, e);
            }
        }
        public void StartHanlerWork()
        {
            Config19e cf19e = new Config19e();
            if (config is Config19e)
            {
                cf19e = config as Config19e;
            }
            if (cf19e.IsCloseSync)//关闭同步，全取
            {
                this.Recp.ReceiveAllPolicy();
            }
            else//同步
            {
                //Task.Factory.StartNew(() =>
                //{
                    //while (true)
                   // {

                this.Recp.SyncPolicy();
                        //if (importCts.Token.IsCancellationRequested)
                        //{
                        //    LogContext log = new LogContext();
                        //    string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\19eFinished\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        //    log.AddLogInfo(logPath, "线程终止！时间：" + DateTime.Now, false);
                        //}
                       // Thread.Sleep(Convert.ToInt32(timeSpan) * 1000);
                   // }
               // }, importCts.Token);
            }
          
        }

        public void StopHanlderWork()
        {
            importCts.Cancel();
        }






        public GlobalConfig config
        {
            get;
            set;
        }
    }
}
