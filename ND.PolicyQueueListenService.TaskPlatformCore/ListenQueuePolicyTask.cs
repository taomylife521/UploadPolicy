using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core;
using ND.PolicyService.Core.UploadPolicyImpl.Middleware.Qunar;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using TaskPlatform.TaskInterface;

namespace ND.PolicyQueueListenService.TaskPlatformCore
{
    public class ListenQueuePolicyTask : AbstractTask
    {
        public override RunTaskResult RunTask()
        {
            RunTaskResult taskResult = new RunTaskResult() { Success = true, Result = "执行完毕" };
            List<Policies> lstPolicies = new List<Policies>();
            string queueName = CustomConfig["policyListenQueueName"];
            string queueHost = CustomConfig["policyListenQueueHost"];
            string updateQueueName = CustomConfig["policyUpdateQueueName"];
            string updateQueueHost = CustomConfig["policyUpdateQueueHost"];
            while (true)
            {
                #region 监听队列
                try
                {

                    MessageQueue x = new MessageQueue(".\\" + queueHost + "\\" + queueName);
                    System.Messaging.Message myMessage = x.Receive(MessageQueueTransactionType.Single);
                    myMessage.Formatter = new BinaryMessageFormatter();
                    lstPolicies = myMessage.Body as List<Policies>;
                    if (lstPolicies == null || lstPolicies.Count <= 0)
                    {
                        ShowRunningLog("未从队列中收到任何政策更新包!");
                        continue;
                    }
                    ShowRunningLog("收到监听队列中政策更新包:" + lstPolicies.Count + "条!");
                }
                catch (Exception ex)
                {
                    ShowRunningLog("监听队列异常:" + JsonConvert.SerializeObject(ex));
                    taskResult = new RunTaskResult() { Success = false, Result = ex.Message };
                   // return taskResult;
                    continue;
                }
                #endregion
                try
                {
                    string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchCompleteUploadPolicyUrl"].ToString(), null);
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        ShowRunningLog("暂未上传过任何政策,不用更新!");
                       // return taskResult;
                        continue;
                    }

                    CompleteUploadPolicyResponse rep = JsonConvert.DeserializeObject<CompleteUploadPolicyResponse>(responseContent);
                    if (rep.ErrCode == ResultType.Failed || rep.CompleteUploadPolicyCollection == null || rep.CompleteUploadPolicyCollection.Count <= 0)
                    {
                        ShowRunningLog("暂未上传过任何政策,不用更新!");
                        //return taskResult;
                        continue;
                    }
                    Dictionary<QunarPolicyType, List<Policies>> dic = CoreHelper.ReserveHaveUploadPolicy(lstPolicies, rep.CompleteUploadPolicyCollection);
                    if (dic.Count <= 0)
                    {
                        ShowRunningLog("此次更新包不在上传政策列表中，不用更新!");
                       // return taskResult;
                        continue;
                    }
                    //在的话发送到更新队列中
                    List<Policies> lstUpdatePolicies = new List<Policies>();
                    foreach (KeyValuePair<QunarPolicyType, List<Policies>> item in dic)
                    {

                        ShowRunningLog("收到去哪儿" + item.Key.ToString() + "政策更新包:" + item.Value.Count + "条");
                        item.Value.ForEach(x =>
                        {
                            lstUpdatePolicies.Add(x);
                        });
                    }
                    new MessageQueueHelper().SendMsgToQueue(updateQueueName, lstUpdatePolicies, MessagePriority.Normal, updateQueueHost);
                    ShowRunningLog("发送更新队列成功!");
                    continue;
                }
                catch (Exception ex)
                {
                    ShowRunningLog(JsonConvert.SerializeObject(ex));
                    //return new RunTaskResult() { Success = false, Result = ex.Message };
                    continue;
                }
            }
        }

        public override string TaskDescription()
        {
            return "监听政策队列(供应商更新政策直接发送到此队列) ";
        }

        public override string TaskName()
        {
            return "监听政策队列 ";
        }
        public override Dictionary<string, string> UploadConfig()
        {

            
            if (!base.CustomConfig.ContainsKey("policyListenQueueName"))
            {
                base.CustomConfig["policyListenQueueName"] = "policylistenqueue";
            }
            if (!base.CustomConfig.ContainsKey("policyListenQueueHost"))
            {
                base.CustomConfig["policyListenQueueHost"] = "private$";
            }
            if (!base.CustomConfig.ContainsKey("policyUpdateQueueName"))
            {
                base.CustomConfig["policyUpdateQueueName"] = "policyupdatequeue";
            }
            if (!base.CustomConfig.ContainsKey("policyUpdateQueueHost"))
            {
                base.CustomConfig["policyUpdateQueueHost"] = "private$";
            }
            return base.UploadConfig();
        }
    }
}
