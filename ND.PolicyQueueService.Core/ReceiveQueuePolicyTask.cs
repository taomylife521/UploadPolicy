using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskPlatform.TaskInterface;

namespace ND.PolicyQueueService.Core
{
    public class ReceiveQueuePolicyTask:AbstractTask
    {
       //public List<Policies> ListenQueuePolicy(string queueHost, string queueName)
       //{
       //    MessageQueue x = new MessageQueue( ".\\" + queueHost + "\\" + queueName);
       //    System.Messaging.Message myMessage = null;
       //    try
       //    {
       //        myMessage = x.Receive(MessageQueueTransactionType.Single);
       //        myMessage.Formatter = new BinaryMessageFormatter();
       //        List<Policies> lstPolicies = myMessage.Body as List<Policies>;
       //        return lstPolicies;
              
       //    }
       //    catch (Exception ex)
       //    {

       //        return new List<Policies>();
       //    }
       //}

        public override RunTaskResult RunTask()
        {
            RunTaskResult taskResult = new RunTaskResult() { Success = true, Result = "执行完毕" };
            List<Policies> lstPolicies = new List<Policies>();
            string queueName = CustomConfig["policyUpdateQueueName"];
            string warningQueueName = CustomConfig["warningPolicyUpdateQueueName"];//预警队列名称
            string warningQueueHost = CustomConfig["warningPolicyUpdateQueueHost"];//预警队列主机
            string queueHost = CustomConfig["policyUpdateQueueHost"];
            while (true)
            {
                try
                {

                    #region 监听队列
                    try
                    {
                        ShowRunningLog(DateTime.Now+":队列监听中...");
                        MessageQueue x = new MessageQueue(".\\" + queueHost + "\\" + queueName);
                        System.Messaging.Message myMessage = x.Receive(MessageQueueTransactionType.Single);
                        myMessage.Formatter = new BinaryMessageFormatter();
                        lstPolicies = myMessage.Body as List<Policies>;
                        if (lstPolicies == null || lstPolicies.Count <= 0)
                        {
                            ShowRunningLog(DateTime.Now + ":未从队列中收到任何政策更新包!");
                            continue;
                          //  return taskResult;
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowRunningLog(DateTime.Now + ":监听队列异常:" + JsonConvert.SerializeObject(ex));
                        taskResult = new RunTaskResult() { Success = false, Result = ex.Message };
                       // return taskResult;
                    }
                    #endregion

                    #region 过滤掉失败次数大于2次的
                    ShowRunningLog(DateTime.Now + ":从队列中收到政策:" + lstPolicies.Count + "条！");
                    List<Policies> lstFailedPolicies = lstPolicies.Where(x => x.Id >= 2).ToList();//找出失败大于等于俩次的条数
                    if (lstFailedPolicies.Count > 0)
                    {
                        ShowRunningLog(DateTime.Now + ":收到更新政策失败次数超过2次的政策条数:" + lstFailedPolicies.Count + "条,开始发送到预警队列");
                        bool r = new MessageQueueHelper().SendMsgToQueue(warningQueueName, lstPolicies, MessagePriority.Normal, warningQueueHost);
                        if (r)
                        {
                            ShowRunningLog(DateTime.Now + ":发送到预警队列成功");
                            continue;
                        }
                       
                         new MessageQueueHelper().SendMsgToQueue(queueName, lstPolicies, MessagePriority.Normal, queueHost);
                         ShowRunningLog(DateTime.Now + ":发送到预警队列失败！重新发送回队列成功！");
                        
                    }
                    lstPolicies = lstPolicies.Where(x => x.Id < 2).ToList();
                    if (lstPolicies.Count <= 0)
                    {
                        ShowRunningLog(DateTime.Now + ":失败次数小于2次的政策数为0，不用更新");
                        continue;
                        //return taskResult;
                    }
                   
                        UploadPolicy(lstPolicies, queueName, queueHost);
                    #endregion
                   
                }
                catch (Exception ex)
                {
                    lstPolicies.ForEach(x =>
                    {
                        x.Id += 1;
                    });
                    ShowRunningLog(DateTime.Now + ":抛出异常：" + JsonConvert.SerializeObject(ex));
                    new MessageQueueHelper().SendMsgToQueue(queueName, lstPolicies, MessagePriority.Normal, queueHost);
                    
                    //taskResult = new RunTaskResult { Success = false, Result = ex.Message };
                }
               // Thread.Sleep(10 * 1000);
                
            }
            return taskResult;
        }


        #region 开启线程上传政策
        public void UploadPolicy(List<Policies> lstPolicies, string queueName, string queueHost)
        {
           // Task.Factory.StartNew(() =>
            //{
                List<string> lstQunarCodes = CoreHelper.ReadQunarCodes();
                QunarUploadConfigResponse config = CoreHelper.LoadQunarUploadConfig();
                IUploadPolicy uplload = new QunarUpLoadPolicy();
                uplload.OnWoking += uplload_OnWoking;
                QunarUploadPolicyRequest qunarRequest = new QunarUploadPolicyRequest()
                {
                    FormatFilePath = ConfigurationManager.AppSettings["FormatQunarFilePath"].ToString(),//xml文件路径
                    FormatZipFilePath = ConfigurationManager.AppSettings["FormatQunarZipFilePath"].ToString(),//压缩包文件路径
                    MaxTaskCount = int.Parse(ConfigurationManager.AppSettings["MaxTaskCount"].ToString()),
                    PerTaskCount = int.Parse(ConfigurationManager.AppSettings["PerTaskCount"].ToString()),
                    PolicyType = QunarPolicyType.COMMON,
                    SqlWhere = "",
                    UploadType = UploadType.Incremental,
                    //QunarUpLoadUrl = ConfigurationManager.AppSettings["QunarUpLoadUrl"].ToString(),
                    CommisionMoney = 0,
                    CommsionPoint = 0,
                    OperName = "system",
                    LstQunarCodes = lstQunarCodes,
                    PageSize = 99999,
                    PolicyDataOrgin = lstPolicies,
                    DefaultUploadConfig = config

                };

                UploadPolicyResponse rep = uplload.UpLoadIncrementPolicy(qunarRequest, false);
                if (rep.ErrCode == ResultType.Failed)
                {
                   lstPolicies.ForEach(x =>//统一加1
                    {
                        x.Id += 1;
                    });
                    new MessageQueueHelper().SendMsgToQueue(queueName, lstPolicies, MessagePriority.Normal, queueHost);
                    ShowRunningLog(DateTime.Now + ":上传失败,更新失败次数并重新发回队列成功！");
                }
               

           // });

        } 
        #endregion

        public void uplload_OnWoking(object sender,EventMsg msg)
        {
            ShowRunningLog(DateTime.Now + ":上传平台:" + msg.PurchaserType.ToString() + ",上传状态:" + msg.Status.ToString() + ",描述:" + msg.Msg);
        }


        public override string TaskDescription()
        {
            return "实时更新去哪儿政策(从待更新的队列里面读出要更新的政策数据) ";
        }

        public override string TaskName()
        {
            return "实时更新去哪儿政策 ";
        }
        public override Dictionary<string, string> UploadConfig()
        {

            if (!base.CustomConfig.ContainsKey("warningPolicyUpdateQueueName"))
            {
                base.CustomConfig["warningPolicyUpdateQueueName"] = "warningpolicyupdatequeue";
            }
            if (!base.CustomConfig.ContainsKey("warningPolicyUpdateQueueHost"))
            {
                base.CustomConfig["warningPolicyUpdateQueueHost"] = "private$";
            }
            if (!base.CustomConfig.ContainsKey("policyUpdateQueueName"))
            {
                base.CustomConfig["policyUpdateQueueName"] = "policyupdatequeue";
            }
            if (!base.CustomConfig.ContainsKey("policyUpdateQueueHost"))
            {
                base.CustomConfig["policyUpdateQueueHost"] = "private$";
            }
            if (!base.CustomConfig.ContainsKey("FormatQunarFilePath"))
            {
                base.CustomConfig["FormatQunarFilePath"] = "C:\\ND\\ND.File\\Qunar\\XmlFile";
            }
            if (!base.CustomConfig.ContainsKey("FormatQunarZipFilePath"))
            {
                base.CustomConfig["FormatQunarZipFilePath"] = "C:\\ND\\ND.File\\Qunar\\ZipFile";
            }
            if (!base.CustomConfig.ContainsKey("PerTaskCount"))
            {
                base.CustomConfig["PerTaskCount"] = "100";
            }
            if (!base.CustomConfig.ContainsKey("MaxTaskCount"))
            {
                base.CustomConfig["MaxTaskCount"] = "10";
            }
            if (!base.CustomConfig.ContainsKey("LoadDefautQunarConfigUrl"))
            {
                base.CustomConfig["LoadDefautQunarConfigUrl"] = "http://118.26.73.75:2233/api/QunarUploadConfigService/LoadDefautConfig";
            }
            if (!base.CustomConfig.ContainsKey("SearchQunarCodeUrl"))
            {
                base.CustomConfig["SearchQunarCodeUrl"] = "http://118.26.73.75:2233/api/QunarCodeService/GetList";
            }
            
            return base.UploadConfig();
        }
    }
}
