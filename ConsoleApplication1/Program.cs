using ND.PolicyQueueService.Core;
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Factory.HandlerFac;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using ND.PolicyReceiveServiceSite;
using ND.PolicyService.Core;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.DbEntity;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using ND.PolicyUploadService.WinformClient;
using ND.WebService.LogIISHost.autoMapperConfiguration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            //new ReceiveKGTask().RunTask();
           // new SyncPolicyTask().RunTask();

            #region 接收政策
            AutoMapperConfiguration.Configure();
            Config19e config = new Config19e
            {
                AppCode19e = "19",
                IsCloseSync = false,
                PerPageSize = "300",
                SafeCode19e = "[66USljYj[3S#lkk3T#930Yj#93*j93&",
                TimeSpan19e = "2000",
                Username19e = "18618001265",
                QueueHost ="private$",
                QueueName = "policyQueue",
                IsSendPolicyQueue = false
            };
            IHandlerForPolicy handler = new HandlerFor19eFactory().Create(config);
           // handler.onWorklingMsg += handler_onWorklingMsg;
            handler.StartHanlerWork();
            #endregion

            #region 监听队列
            //Console.WriteLine("队列监听中...");
            //MessageQueue x = new MessageQueue(".\\private$\\warningpolicyupdatequeue");
            //System.Messaging.Message myMessage = x.Receive(MessageQueueTransactionType.Single);
            //myMessage.Formatter = new BinaryMessageFormatter();
            //List<Policies> lstPolicies = myMessage.Body as List<Policies>;
            //try
            //{
            //    lstPolicies.ForEach(x1 =>
            //    {
            //        x1.Id = 0;
            //    });
            //    if (lstPolicies == null || lstPolicies.Count <= 0)
            //    {
            //        Console.WriteLine("未从队列中收到任何政策更新包!");
            //        return;
            //    }
            //    File.WriteAllText("d://1.txt", JsonConvert.SerializeObject(lstPolicies));
            //    new MessageQueueHelper().SendMsgToQueue("policyupdatequeue", lstPolicies, MessagePriority.Normal, "private$");
            //    UploadPolicy(lstPolicies, "policyupdatequeue", "private$");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("监听队列异常:" + JsonConvert.SerializeObject(ex));
            //    new MessageQueueHelper().SendMsgToQueue("policyupdatequeue", lstPolicies, MessagePriority.Normal, "private$");
            //}
            #endregion


            #region 上传
            //HttpClient client = new HttpClient();

            ////client.DefaultRequestHeaders.enctype

            //byte[] ct = File.ReadAllBytes("E://20160106044440.zip");////@"D:\ND.Application\File\Qunar\ZipFile\2015\12\2\15\20151202030916.zip"
            //HttpContent con = new ByteArrayContent(ct, 0, ct.Length);//, Encoding.UTF8, "multipart/form-data"
            //con.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");

            //var res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["QunarUpLoadUrl"].ToString(), con).Result;
            //res.EnsureSuccessStatusCode();
            //string backContent = res.Content.ReadAsStringAsync().Result;
            
            #endregion
           
            Console.ReadKey();
        }

        public static void uplload_OnWoking(object sender, EventMsg msg)
        {
            Console.WriteLine("上传平台:" + msg.PurchaserType.ToString() + ",上传状态:" + msg.Status.ToString() + ",描述:" + msg.Msg);
        }

        public static void UploadPolicy(List<Policies> lstPolicies, string queueName, string queueHost)
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
                Console.WriteLine(DateTime.Now + ":上传失败,更新失败次数并重新发回队列成功！");
            }


            // });

        } 
    }
}
