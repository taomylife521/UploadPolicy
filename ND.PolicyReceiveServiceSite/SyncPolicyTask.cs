using ND.PolicyReceiveService.Core;
using ND.PolicyReceiveService.Factory.HandlerFac;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using ND.WebService.LogIISHost.autoMapperConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlatform.TaskInterface;

namespace ND.PolicyReceiveServiceSite
{
    public class SyncPolicyTask:AbstractTask
    {
        public override RunTaskResult RunTask()
        {
            RunTaskResult taskResult = new RunTaskResult() { Success = true, Result = "执行完毕" };
            try
            {
                AutoMapperConfiguration.Configure();
                Config19e config = new Config19e
                {
                    AppCode19e = CustomConfig["appCode_19e"],
                    IsCloseSync = Convert.ToBoolean(CustomConfig["IsCloseSync19e"]),
                    PerPageSize = CustomConfig["PerPageSize19e"],
                    SafeCode19e = CustomConfig["safeCode_19e"],
                    TimeSpan19e = CustomConfig["timespan_19e"],
                    Username19e = CustomConfig["username_19e"],
                    QueueHost = CustomConfig["queueHost"],
                    QueueName = CustomConfig["queueName"],
                    IsSendPolicyQueue = Boolean.Parse(CustomConfig["isSendPolicyQueue"])
                };
                IHandlerForPolicy handler = new HandlerFor19eFactory().Create(config);
                handler.onWorklingMsg += handler_onWorklingMsg;
                handler.StartHanlerWork();
               
            }
            catch(Exception ex)
            {
                ShowRunningLog("抛出异常：" + ex.Message.ToString());
                taskResult = new RunTaskResult { Success = false, Result = ex.Message };
            }
            return taskResult;
        }

        void handler_onWorklingMsg(object sender, EventMessage e)
        {
            
            ShowRunningLog(e.Msg);
        }

        public override string TaskDescription()
        {
            return "同步或全取19e的政策 ";
        }

        public override string TaskName()
        {
            return "同步或全取19e的政策 ";
        }
        public override Dictionary<string, string> UploadConfig()
        {
            if (!base.CustomConfig.ContainsKey("username_19e"))
            {
                base.CustomConfig["username_19e"] = "18618001265";
            }
            if (!base.CustomConfig.ContainsKey("appCode_19e"))
            {
                base.CustomConfig["appCode_19e"] = "19";
            }
            if (!base.CustomConfig.ContainsKey("timespan_19e"))
            {
                base.CustomConfig["timespan_19e"] = "30";
            }
            if (!base.CustomConfig.ContainsKey("PerPageSize19e"))
            {
                base.CustomConfig["PerPageSize19e"] = "500";
            }
            if (!base.CustomConfig.ContainsKey("IsCloseSync19e"))
            {
                base.CustomConfig["IsCloseSync19e"] = "true"; 
            }
            if (!base.CustomConfig.ContainsKey("safeCode_19e"))
            {
                base.CustomConfig["safeCode_19e"] = "[66USljYj[3S#lkk3T#930Yj#93*j93&"; 
            }
            if (!base.CustomConfig.ContainsKey("queueHost"))
            {
                base.CustomConfig["queueHost"] = "private$";
            }
            if (!base.CustomConfig.ContainsKey("queueName"))
            {
                base.CustomConfig["queueName"] = "policyQueue";
            }
            if (!base.CustomConfig.ContainsKey("isSendPolicyQueue"))
            {
                base.CustomConfig["isSendPolicyQueue"] = "false";
            }
            return base.UploadConfig();
        }
    }
}
