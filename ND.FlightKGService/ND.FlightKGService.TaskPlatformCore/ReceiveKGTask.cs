
using Maticsoft.DAL;
using ND.FlightKGService.TaskPlatformCore.DAL;
using ND.FlightKGService.TaskPlatformCore.w_51book_getModifyAndRefundStipulates;
using ND.PolicyReceiveService.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaskPlatform.TaskInterface;

namespace ND.PolicyQueueService.Core
{
    public class ReceiveKGTask : AbstractTask
    {
        public override RunTaskResult RunTask()
        {
            RunTaskResult taskResult = new RunTaskResult() { Success = true, Result = "执行完毕" };
            FlightAirRuleDAL ruleDal = new FlightAirRuleDAL();
            try
            {
                ruleDal.deleteAllData();//清空所有数据
                bool flag = true;
                int index = 0;
                while (flag)
                {
                    getModifyAndRefundStipulatesRequest req = new getModifyAndRefundStipulatesRequest();
                    req.rowPerPage = 1500;
                    req.rowPerPageSpecified = true;
                    string lastTimeAndId = GetLastUpTimeAndId("AirKGLog");
                    req.lastSeatId = Convert.ToInt32(lastTimeAndId.Split('|')[1]);
                    req.lastSeatIdSpecified = true;
                    req.lastModifiedAt = lastTimeAndId.Split('|')[0];
                    getModifyAndRefundStipulatesReply rep = _51bookHelper.getModifyAndRefundStipulates(req) as getModifyAndRefundStipulatesReply;
                    if(rep.returnCode.ToLower() != "s")
                    {
                        ShowRunningLog(rep.returnMessage+","+rep.returnStackTrace);
                        flag = false;
                        continue;
                    }
                    ShowRunningLog("收到退改签规定包数量:" + rep.modifyAndRefundStipulateList.Length + ",剩余页数:" + rep.leftPages);
                    if (index > 0)
                    {
                        if (rep.leftPages == 0)
                        {
                            ShowRunningLog("剩余页数为0，已经全部取完！");
                            flag = false;
                        }
                        else
                        {
                            
                                addDb(rep);
                           
                        }
                    }
                    else
                    {
                        addDb(rep);
                    }
                    index++;
                }
                SaveLastUpTimeAndId("2000-01-01 00:00:00|0", "AirKGLog");
                ruleDal.deleteAllDataForever();//清空所有数据

            }
            catch (Exception ex)
            {
              ShowRunningLog("添加航空公司客规失败：" + ex.Message);
              ruleDal.recoveryAllData();
            }
            return taskResult;
        }


        #region 获取上次更新时间和id
        /// <summary>
        /// 获取上次更新时间和id
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static string GetLastUpTimeAndId(string _name)
        {
        
            LogContext log = new LogContext();

            string logPath = System.IO.Directory.GetCurrentDirectory()+ "\\LogContext\\ND.FlightKGService\\" + _name + ".txt";
            string lastUpTime = log.ReadDataLog(logPath).TrimEnd((char[])"\r\n".ToCharArray());
            if (lastUpTime.Trim() == "")
            {
                return "2015-10-10 08:00:00|0";
                
            }
            else
            {
                
                string lastUpdateTime = lastUpTime.Trim().Split('|')[0];
               string lastId =lastUpTime.Trim().Split('|')[1];
               return lastUpdateTime + "|" + lastId;

            }  
        }
        #endregion

        #region 保存上次更新时间和id
        /// <summary>
        /// 保存最新政策更新时间和ID
        /// </summary>
        /// <param name="_timeAndId">时间|ID</param>
        public static void SaveLastUpTimeAndId(string _timeAndId, string _name)
        {
            LogContext log = new LogContext();
            //string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicyUploadService\\" + _name + ".txt";
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.FlightKGService\\" + _name + ".txt";
            log.AddLogInfo(logPath, _timeAndId, false);
        }
        #endregion

        #region 添加到数据库
        public void addDb(getModifyAndRefundStipulatesReply rep)
        {
            try
            {
                string seatId = "0";
                string lastModifiedAt = "2000-01-01 00:00:00";
                int i = 0;
                FlightAirRuleDAL airRuleBll = new FlightAirRuleDAL();
                ShowRunningLog(DateTime.Now + ":开始添加退改签规定包：" + rep.modifyAndRefundStipulateList.Length);
                //获取记录并添加到数据库中
                for (i = 0; i < rep.modifyAndRefundStipulateList.Length; i++)
                {
                    FlightAirRule airRule = new FlightAirRule();
                    airRule.id = System.Guid.NewGuid().ToString();
                    airRule.returnn = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].refundStipulate, System.Text.Encoding.UTF8);
                    airRule.endorsement = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].modifyStipulate, System.Text.Encoding.UTF8);
                    lastModifiedAt = rep.modifyAndRefundStipulateList[i].modifiedAt;
                    seatId = rep.modifyAndRefundStipulateList[i].seatId.ToString();
                    airRule.seatclass = rep.modifyAndRefundStipulateList[i].seatCode;
                    airRule.change = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].changeStipulate, System.Text.Encoding.UTF8);
                    airRule.airline = rep.modifyAndRefundStipulateList[i].airlineCode;
                    try
                    {
                        int r = airRuleBll.Add(airRule);
                        if (r == 0)
                        {
                            ShowRunningLog("添加航空公司客规失败：" + rep.modifyAndRefundStipulateList[i].airlineCode + rep.modifyAndRefundStipulateList[i].seatCode + rep.modifyAndRefundStipulateList[i].seatId);
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                if (i >= rep.modifyAndRefundStipulateList.Length)
                {
                    ShowRunningLog(DateTime.Now + ":添加完成：" + rep.modifyAndRefundStipulateList.Length);
                    SaveLastUpTimeAndId(lastModifiedAt + "|" + seatId, "AirKGLog");
                }
            }
            catch(Exception ex)
            { }
        } 
        #endregion
       


        public override string TaskDescription()
        {
            return "定时获取客规 ";
        }

        public override string TaskName()
        {
            return "定时获取客规 ";
        }
        public override Dictionary<string, string> UploadConfig()
        {
            return base.UploadConfig();
        }
    }
}
