using ND.PolicyReceiveService.Core.PolicyServiceBy19e;
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ND.PolicyReceiveService.Core.ReceivePolicy.helper
{
    public class RecPolicy19eHelper
    {
        public event EventHandler<EventMessage> onWorklingMsg;
        private static Config19e Config{get;set;}
        public RecPolicy19eHelper(Config19e cf19e)
        {
            Config = cf19e;
        }
       
        #region //政策同步（添加）
        public void SyncForAdd()
        {
            try
            {

                PolicyServiceBy19e.syncRateRequest req = new PolicyServiceBy19e.syncRateRequest();
                PolicyServiceBy19e.SyncRateServiceGHImplService c = new PolicyServiceBy19e.SyncRateServiceGHImplService();

                //while (true)
                //{
                string[] arryTimeAndId = GetLastUpTimeAndId("ND.ReceivePolicyService\\19e\\PolicyAddLog19e").Split('|');

                req.aircomp2c = ""; //航空公司, 如：MU, 为空取所有
                req.rateType = "3"; //政策类型[1:普通/2:特殊3:全部]
                req.psgType = "1";  //乘客类型[1:成人/2:儿童/3:全部]
                req.strategyId = arryTimeAndId[1];
                req.updateTime = arryTimeAndId[0];
                req.minDiscount = "0";

                req.pageSize = Config.PerPageSize;
                req.username = Config.Username19e;//"18618001265";
                req.appcode = Config.AppCode19e;// "19";
                req.sign = GetSignSync(req);
                PolicyServiceBy19e.syncRateResponse res = new syncRateResponse();
                try
                {
                    // ShowMsgToForm("接收19e政策请求参数:" + JsonConvert.SerializeObject(req));
                    res = c.GetSyncRate(req);
                }
                catch (Exception ex)
                {
                    ShowMsgToForm("接收19e政策异常:" + JsonConvert.SerializeObject(ex));
                    return;
                }


                if (res.code.Trim() == "F")
                {
                    ShowMsgToForm("19e政策接收失败,错误信息:" + res.message);
                    return;
                }
                else if (res.code.Trim() == "S")
                {
                    ShowMsgToForm("19e政策接口连接成功，接收政策...", "19e", false, false);

                    PolicyServiceBy19e.rate[] arry = res.rateList;

                    if (arry == null || arry.Length == 0)
                    {
                        ShowMsgToForm("19e政策 - 尚无新政策", "19e", false, false);
                        return;
                    }
                    List<Policies> list = new List<Policies>();

                    Regex regV = new Regex(@"[^\d\/]");

                    foreach (var item in arry)
                    {

                        #region 新代码


                        Policies policyInfo = new Policies();
                        // policyInfo.PolicyId = System.Guid.NewGuid().ToString();
                        policyInfo.AirlineCode = item.aircomp2c;//航空公司
                        policyInfo.DptCity = item.fromport3c;//出发城市
                        policyInfo.ArrCity = item.toport3c;//抵达城市
                        policyInfo.FlightIn = item.flightnoFit == null ? "" : item.flightnoFit == "***" ? "" : regV.Replace(item.flightnoFit, "").Trim();//适用航班
                        policyInfo.FlightOut = item.flightnoNotFit == null ? "" : item.flightnoNotFit == "***" ? "" : regV.Replace(item.flightnoNotFit, "").Trim();//不适用航班
                        policyInfo.Seat = item.flightclass != "" ? item.flightclass : "";//舱位
                        policyInfo.SrcType = (int)item.routetype;//航程类型航程类型[1:单程/2:往返/3:单程及往返]
                        policyInfo.SaleEffectDate = item.sdate;//政策有效期(开始时间)
                        policyInfo.SaleExpireDate = item.edate;//政策有效期(截止时间)
                        policyInfo.FlightEffectDate = item.sdate;
                        policyInfo.FlightExpireDate = item.edate;
                        policyInfo.CommisionPoint = item.backrate;//返点
                        policyInfo.CommisionMoney = 0;//返现
                        policyInfo.NeedSwitchPNR = 0;//是否需要换pnr
                        policyInfo.TicketSpeed = item.thespeed;//出票速度
                        policyInfo.IssueWorkTime = item.worktime == null ? "" : item.worktime;//出票时间段
                        policyInfo.VtWorkTime = item.voidtime == null ? "" : item.voidtime;//废票时间段
                        policyInfo.PartnerId = (int)SupplierType._19E;
                        policyInfo.PartnerName = SupplierType._19E.ToString();
                        policyInfo.Comment = item.chngretmemo ?? "";//政策备注
                        policyInfo.FlightCycle = item.daysFit;//适用班期
                        policyInfo.DelDegree = 1;
                        policyInfo.CommisionType = int.Parse(item.ratetype);//政策类型 1-普通
                        policyInfo.PartnerPolicyId = item.strategyId.ToString();//政策代号
                        policyInfo.PolicyStatus = 1;
                        policyInfo.CreateTime = DateTime.Now;
                        policyInfo.PolicyType = ((PolicyType)item.strategyType).ToString();
                        policyInfo.PsgType = item.psgtype;//乘客类型[1:成人]
                        //policyInfo.UserType = item.usertype;//客户类型[1:散客]
                        policyInfo.Param1 = "";
                        policyInfo.Param2 = "";
                        policyInfo.Param3 = "";
                        policyInfo.Param4 = "";
                        list.Add(policyInfo);
                        #endregion


                    }

                    ShowMsgToForm("19e政策接收成功，共接收 " + list.Count + " 条政策，更新数据库...", "19e", false);
                    if (Config.IsSendPolicyQueue)
                    {
                        MessageQueueHelper helper = new MessageQueueHelper();
                        bool r = helper.SendMsgToQueue(Config.QueueName, list, MessagePriority.Normal, Config.QueueHost);
                        if (r)
                        {
                            ShowMsgToForm("发送去哪儿更新队列成功", "19e", false);
                        }
                    }
                    List<PolicySyncRec> errList = PolicyManage19e.SynchronizePolicy(list);
                    if (errList.Count <= 0)
                    {
                        SaveLastUpTimeAndId(res.updateTime + "|" + arry[arry.Length - 1].strategyId, "ND.ReceivePolicyService\\19e\\PolicyAddLog19e");
                        ShowMsgToForm("19e政策更新数据库成功！", "19e", false);
                    }
                    //else
                    // ShowMsgToForm("19e政策更新数据库失败！更新不成功的数据:" + JsonConvert.SerializeObject(errList),"19eErrAddRec");

                    if (res.pageCount < 1) return;
                    else
                        ShowMsgToForm("19e政策 PageCount(" + res.pageCount + ") 大于 1 继续调用同步接口...", "19e", false);

                }
            }
            catch(Exception ex)
            {
                var str = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    str += ex.ToString();   // ex.Message;
                }
                ShowMsgToForm("添加19e政策异常:" + str);
            }
            
           
        }
        #endregion

        #region //政策同步（删除）
        public void SyncForDel()
        {
            try
            {
                PolicyServiceBy19e.syncRateDelRequest reqDel = new PolicyServiceBy19e.syncRateDelRequest();
                PolicyServiceBy19e.SyncRateServiceGHImplService c = new PolicyServiceBy19e.SyncRateServiceGHImplService();


                string[] arryTimeAndId = GetLastUpTimeAndId("ND.ReceivePolicyService\\19e\\PolicyDelLog19e").Split('|');
                reqDel.updateTime = arryTimeAndId[0];
                reqDel.deleteStrategyId = arryTimeAndId[1];
                reqDel.pageSize = Config.PerPageSize;
                reqDel.username = Config.Username19e;//"18618001265";
                reqDel.appcode = Config.AppCode19e;// "19";
                reqDel.sign = GetSignSyncDel(reqDel);

                PolicyServiceBy19e.syncRateDelResponse res = c.GetSyncRateDel(reqDel);
                //SaveLog("19e政策(删除)响应数据:" + JsonConvert.SerializeObject(res), "19eDelResponse");
                if (res.code.Trim() == "F")
                {
                    ShowMsgToForm("19e政策(删除)接收失败,错误信息" + res.message);
                }
                else if (res.code.Trim() == "S")
                {
                    ShowMsgToForm("19e政策(删除)接口连接成功，接收政策...", "19e", false, false);

                    PolicyServiceBy19e.rateDel[] arry = res.rateList;
                    if (arry == null || arry.Length == 0)
                    {
                        ShowMsgToForm("19e政策 - 尚无删除的政策", "19e", false, false);
                        return;
                    }
                    List<Policies> listDelPolicy = new List<Policies>();
                    List<string> list = new List<string>();
                    foreach (var item in arry)
                    {
                        Policies pl = new Policies();
                        pl.DelDegree = 0;
                        pl.PartnerPolicyId = item.strategyId.ToString();
                        list.Add(item.strategyId.ToString());
                        listDelPolicy.Add(pl);
                    }
                    if (Config.IsSendPolicyQueue)
                    {
                        MessageQueueHelper helper = new MessageQueueHelper();
                        bool r = helper.SendMsgToQueue(Config.QueueName, listDelPolicy, MessagePriority.Normal, Config.QueueHost);
                        if (r)
                        {
                            ShowMsgToForm("发送去哪儿删除更新队列成功,共发送" + listDelPolicy.Count + "条", "19e", false);
                        }
                    }
                    ShowMsgToForm("19e政策(删除)接收成功，共接收 " + list.Count + " 条政策，更新数据库...", "19e", false);

                    List<string> errSql = PolicyManage19e.SynchronizePolicyDel(list);
                    SaveLastUpTimeAndId(res.updateTime + "|" + res.deleteStrategyId, "ND.ReceivePolicyService\\19e\\PolicyDelLog19e");
                    if (errSql.Count <= 0)
                    {
                        //SaveLastUpTimeAndId(res.updateTime + "|" + arry[arry.Length - 1].strategyId, "PolicyDelLog");
                        ShowMsgToForm("19e政策(删除)更新数据库成功！", "19e", false);
                    }
                    else
                    {
                        ShowMsgToForm("19e政策(删除)更新数据库失败！", "19e", false);//执行的sql为:"+JsonConvert.SerializeObject(errSql)\
                        //SaveLog("19e政策(删除)更新数据库失败!执行的sql为:" + JsonConvert.SerializeObject(errSql), "19eErrDelRec");
                    }

                    if (res.rateList.Length < 1) return;
                    else
                        ShowMsgToForm("19e政策(删除) rateList(" + res.rateList.Length + ") 大于 1 继续调用同步(删除)接口...", "19e", false);
                }
            }
            catch(Exception ex)
            {
                var str = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    str += ex.ToString();   // ex.Message;
                }
                ShowMsgToForm("删除19e政策异常:" + str);
            }
            
        }
        #endregion

        #region 全取政策
        public void ReceiveAll()
        {
             PolicyServiceBy19e.syncRateRequest req = new PolicyServiceBy19e.syncRateRequest();
            PolicyServiceBy19e.SyncRateServiceGHImplService c = new PolicyServiceBy19e.SyncRateServiceGHImplService();

            while (true)
            {
                string[] arryTimeAndId = GetLastUpTimeAndId("ND.ReceivePolicyService\\19e\\PolicyAddLog19e").Split('|');

                req.aircomp2c = ""; //航空公司, 如：MU, 为空取所有
                req.rateType = "3"; //政策类型[1:普通/2:特殊3:全部]
                req.psgType = "1";  //乘客类型[1:成人]
                req.strategyId = arryTimeAndId[1];
                req.updateTime = arryTimeAndId[0];
                req.minDiscount = "0";
                req.pageSize = Config.PerPageSize;
                req.username = Config.Username19e;//"18618001265";
                req.appcode = Config.AppCode19e;// "19";
                req.sign = GetSignSync(req);
               // ShowMsgToForm("同步新增请求参数:" + JsonConvert.SerializeObject(req));
                PolicyServiceBy19e.syncRateResponse res = c.GetSyncRate(req);
                if (res.code.Trim() == "F")
                {
                    ShowMsgToForm("19e政策接收失败,错误信息:" + res.message);
                    continue;
                }
                else if (res.code.Trim() == "S")
                {
                    ShowMsgToForm("19e政策接口连接成功，接收政策...","19e",false,false);

                    PolicyServiceBy19e.rate[] arry = res.rateList;

                    if (arry == null || arry.Length == 0)
                    {
                        ShowMsgToForm("19e政策 - 尚无新政策", "19e", false,false);
                        continue;
                    }
                    List<Policies> list = new List<Policies>();

                    Regex regV = new Regex(@"[^\d\/]");

                    foreach (var item in arry)
                    {

                        #region 新代码

                        Policies policyInfo = new Policies();
                        //policyInfo.PolicyId = System.Guid.NewGuid().ToString();
                        policyInfo.AirlineCode = item.aircomp2c;//航空公司
                        policyInfo.DptCity = item.fromport3c;//出发城市
                        policyInfo.ArrCity = item.toport3c;//抵达城市
                        policyInfo.FlightIn = item.flightnoFit == null ? "" : item.flightnoFit == "***" ? "" : regV.Replace(item.flightnoFit, "").Trim();//适用航班
                        policyInfo.FlightOut = item.flightnoNotFit == null ? "" : item.flightnoNotFit == "***" ? "" : regV.Replace(item.flightnoNotFit, "").Trim();//不适用航班
                        policyInfo.Seat = item.flightclass != "" ? item.flightclass : "";//舱位
                        policyInfo.SrcType = (int)item.routetype;//航程类型航程类型[1:单程/2:往返/3:单程及往返]
                        policyInfo.SaleEffectDate = item.sdate;//政策有效期(开始时间)
                        policyInfo.SaleExpireDate = item.edate;//政策有效期(截止时间)
                       // policyInfo.FlightEffectDate = item.sdate;
                        //policyInfo.FlightExpireDate = item.edate;
                        policyInfo.CommisionPoint = item.backrate;//返点
                        policyInfo.CommisionMoney = 0;//返现
                        policyInfo.NeedSwitchPNR = 0;//是否需要换pnr
                        policyInfo.TicketSpeed = item.thespeed;//出票速度
                        policyInfo.IssueWorkTime = item.worktime == null ? "" : item.worktime;//出票时间段
                        policyInfo.VtWorkTime = item.voidtime == null ? "" : item.voidtime;//废票时间段
                        policyInfo.PartnerId = (int)SupplierType._19E;
                        policyInfo.PartnerName = SupplierType._19E.ToString();
                        policyInfo.Comment = item.chngretmemo ?? "";//政策备注
                        policyInfo.FlightCycle = item.daysFit;//适用班期
                        policyInfo.DelDegree = 1;
                        policyInfo.CommisionType = int.Parse(item.ratetype);//政策类型 1-普通
                        policyInfo.PartnerPolicyId = item.strategyId.ToString();//政策代号
                        policyInfo.PolicyStatus = 1;
                        policyInfo.CreateTime = DateTime.Now;
                        policyInfo.PolicyType = ((PolicyType)item.strategyType).ToString();
                        policyInfo.PsgType = item.psgtype;//乘客类型[1:成人]
                       // policyInfo.UserType = item.usertype;//客户类型[1:散客]
                        policyInfo.Param1 = "";
                        policyInfo.Param2 = "";
                        policyInfo.Param3 = "";
                        policyInfo.Param4 = "";
                        list.Add(policyInfo);
                        #endregion
                    }

                    ShowMsgToForm("19e政策接收成功，共接收 " + list.Count + " 条政策，更新数据库...", "19e", false);

                    List<PolicySyncRec> errList = PolicyManage19e.SynchronizePolicy(list);
                    if (errList.Count <= 0)
                    {
                        SaveLastUpTimeAndId(res.updateTime + "|" + arry[arry.Length - 1].strategyId, "ND.ReceivePolicyService\\19e\\PolicyAddLog19e");
                        ShowMsgToForm("19e政策更新数据库成功！", "19e", false);
                    }
                   // else
                       // ShowMsgToForm("19e政策更新数据库失败！更新不成功的数据:" + JsonConvert.SerializeObject(errList), "19eErrAddRec");

                    if (res.pageCount < 1) break;
                    else
                        ShowMsgToForm("19e政策 PageCount(" + res.pageCount + ") 大于 1 继续调用同步接口...", "19e",false);
                }
            }
           
        }
        #endregion

        private string GetLastUpTimeAndId(string _name)
        {
            LogContext log = new LogContext();
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\" + _name + ".txt";
            string lastUpTime = log.ReadDataLog(logPath).TrimEnd((char[])"\r\n".ToCharArray());
            if (lastUpTime.Trim() == "")
                lastUpTime = "2013-01-01 08:00:00" + "|" + "0";

            return lastUpTime;
        }

        /// <summary>
        /// 保存最新政策更新时间和ID
        /// </summary>
        /// <param name="_timeAndId">时间|ID</param>
        private void SaveLastUpTimeAndId(string _timeAndId, string _name)
        {
            LogContext log = new LogContext();
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\" + _name + ".txt";
            log.AddLogInfo(logPath, _timeAndId, false);
        }

        private void SaveLog(string _msg,string folderName,bool isAppend=true)
        {
            LogContext log = new LogContext();
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\" + folderName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            log.AddLogInfo(logPath, DateTime.Now + ":" + _msg + "\r\n", isAppend);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="_msg">消息内容</param>
        public void ShowMsgToForm(string _msg,string folderName = "19e",bool isWriteToLog=true,bool isShowForm=true)
        {
            if (isWriteToLog)
            {
                bool flag = folderName == "19e" ? false : true;
                SaveLog(_msg, folderName, flag);
            }
            if (isShowForm)
            {
                if (onWorklingMsg != null)
                {
                    onWorklingMsg(this, new EventMessage { Msg = DateTime.Now.ToString() + ":" + _msg });
                }
            }
        }

        private string GetSignSync(PolicyServiceBy19e.syncRateRequest req)
        {
            //string sign = "GetSyncRate" + req.aircomp2c + "" + req.rateType + "" + req.psgType + "19" + "#U[jZ&6#U9#l3TZ#U0Ylj033j96j9#Yl";
            string safecode = Config.SafeCode19e;// TriDES.Decrypt(ConfigurationManager.AppSettings["safeCode_19e"]);
            StringBuilder sbSign = new StringBuilder();
            sbSign.Append("GetSyncRate");
            sbSign.Append(req.aircomp2c);
            sbSign.Append(req.rateType);
            sbSign.Append(req.psgType);
            sbSign.Append(req.strategyId);
            sbSign.Append(req.updateTime);
            sbSign.Append(req.minDiscount);
            sbSign.Append(req.pageSize);
            sbSign.Append(req.username);
            sbSign.Append(req.appcode);
            //sbSign.Append("[66USljYj[3S#lkk3T#930Yj#93*j93&");
            sbSign.Append(safecode);
            return EncryptWithoutKey(sbSign.ToString(), Encoding.GetEncoding("GB2312"));
        }

        private string GetSignSyncDel(PolicyServiceBy19e.syncRateDelRequest req)
        {
            //string sign = "GetSyncRate" + req.aircomp2c + "" + req.rateType + "" + req.psgType + "19" + "#U[jZ&6#U9#l3TZ#U0Ylj033j96j9#Yl";
            string safecode = Config.SafeCode19e;// TriDES.Decrypt(ConfigurationManager.AppSettings["safeCode_19e"]);
            StringBuilder sbSign = new StringBuilder();
            sbSign.Append("GetSyncRateDel");
            sbSign.Append(req.updateTime);
            sbSign.Append(req.deleteStrategyId);
            sbSign.Append(req.pageSize);
            sbSign.Append(req.username);
            sbSign.Append(req.appcode);
            //sbSign.Append("[66USljYj[3S#lkk3T#930Yj#93*j93&");
            sbSign.Append(safecode);
            return EncryptWithoutKey(sbSign.ToString(), Encoding.GetEncoding("GB2312"));
        }

        /// <summary>
        /// 用MD5算法加密字符串（不带密钥）
        /// </summary>
        /// <param name="srcString">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        private string EncryptWithoutKey(string srcString, Encoding _encoding)
        {
            byte[] b = Encoding.UTF8.GetBytes(srcString);
            byte[] a = Encoding.Convert(Encoding.UTF8, _encoding, b);
            b = new MD5CryptoServiceProvider().ComputeHash(a);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x2").ToLower();
            }
            return ret;
        }
    }
}
