using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Core.SplitCoreImpl;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using ND.PolicyUploadService.DtoModel.QunarCode;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using ND.PolicyUploadService.DtoModel.RealTimeUpload;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ND.PolicyService.Core
{
   public class CoreHelper
    {
        #region 获取上次更新时间和id
        /// <summary>
        /// 获取上次更新时间和id
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static PolicyRecord GetLastUpTimeAndId(string _name)
        {
            PolicyRecord rec = new PolicyRecord();
            LogContext log = new LogContext();

            string logPath = ConfigurationManager.AppSettings["LogRecPath"].ToString()+ "\\LogContext\\ND.PolicyUploadService\\" + _name + ".txt"; 
            string lastUpTime = log.ReadDataLog(logPath).TrimEnd((char[])"\r\n".ToCharArray());
            if (lastUpTime.Trim() == "")
            {
                rec.LastPolicyId = 0;
                rec.LastUpdateTime = Convert.ToDateTime("2015-10-10 08:00:00");
            }
            else
            {
                rec.LastUpdateTime = Convert.ToDateTime(lastUpTime.Trim().Split('|')[0]);
                rec.LastPolicyId = int.Parse(lastUpTime.Trim().Split('|')[1]);

            }


            return rec;
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
            string logPath = ConfigurationManager.AppSettings["LogRecPath"].ToString()+ "\\LogContext\\ND.PolicyUploadService\\" + _name + ".txt"; 
            log.AddLogInfo(logPath, _timeAndId, false);
        }
        #endregion

        #region 父类转子类
        public static Child ChangeToChild<Father, Child>(Father request) where Child : class
        {
            if (!(request is Child))
            {
                throw new ArgumentNullException(typeof(Father) + "不能转换为" + typeof(Child) + ",类型不兼容！");
            }
            Child childRequest = request as Child;
            if (childRequest == null)
            {
                throw new ArgumentNullException(typeof(Father) + "转换为" + typeof(Child) + "为空！");
            }
            return childRequest;
        }

        #endregion

        #region 创建文件
        public static bool CreateFile(string strPath, string txt)
        {
            try
            {
                string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
                if (!Directory.Exists(strDirecory))
                {
                    Directory.CreateDirectory(strDirecory);
                }
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }



                FileStream fs = File.Create(strPath);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(txt);
                sw.Flush();
                fs.Flush();
                fs.Close();
                fs.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        } 
        #endregion

       

        #region 封装post请求
        public static string DoPost(string url, object data)
        {

            string responseContent = "";
            try
            {
                //设置HttpClientHandler的AutomaticDecompression
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                //创建HttpClient（注意传入HttpClientHandler）
                using (var http = new HttpClient(handler))
                {
                    //使用FormUrlEncodedContent做HttpContent

                    var content = new StringContent(JsonConvert.SerializeObject(data));
                    content.Headers.ContentType.MediaType = "application/json";
                    //con.Headers.ContentType.MediaType = "text/xml";
                    content.Headers.ContentType.CharSet = "utf-8";
                    //await异步等待回应

                    var response = http.PostAsync(url, content).Result;
                    //确保HTTP成功状态值
                    response.EnsureSuccessStatusCode();
                    responseContent = response.Content.ReadAsStringAsync().Result;
                    //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip） 
                }
                return responseContent;
            }
           
            catch(TimeoutException ex)
            {
               // MessageBox.Show("请求数据超时:" + ex.Message);
                return "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("请求数据失败:" + ex.Message);
                return "";
            }
        }
        #endregion

        #region 拆分政策
        public static List<Policies> SplitPolicy(PurchaserType pType, List<Policies> lstPolicies,List<string> lstQunarCodes,PoliciesType policyType)
        {
            SplitPolicyResponse splitResponse = new SplitPolicyResponse();
            int maxArrCityCount = 25;
            int maxDptCityCount = 1;
            int maxFlightInCount = 25;
            int splitMaxTaskCount = 10;
            int splitPerTaskMaxCount = 50;
            int maxSeatCount = 29;
            switch(policyType)
            {
                case PoliciesType.COMMON://普通政策
                    break;
                case PoliciesType.CUSTOMER://包机切位政策
                    break;
                case PoliciesType.LOWPRICE://特价政策
                    break;
                case PoliciesType.PREPAY://预付政策
                    {
                        maxSeatCount = 1;
                        maxArrCityCount = 1;
                    }
                    break;
                case PoliciesType.ROUNDALL://往返所有类型政策
                    break;
                case PoliciesType.ROUNDPREPAY://往返预付政策
                    break;
                case PoliciesType.ROUNDSPECIAL://往返特价政策
                    break;
                case PoliciesType.SINGLEALL://单程所有类型政策
                    break;
                case PoliciesType.APPLY://申请政策
                    break;
                default:
                    break;
            }
            switch (pType)
            {
                case PurchaserType.Qunar:
                    {
                        QunarSplitPolicyRequest splitQunarRequst = new QunarSplitPolicyRequest()
                        {
                            MaxArrCityCount = maxArrCityCount,//最大支持出发城市个数
                            MaxDptCityCount = maxDptCityCount,//最大支持出发城市个数
                            MaxFlightInCount = maxFlightInCount,//包含航班最大个数
                            SplitMaxTaskCount = splitMaxTaskCount,//最大可开启任务数量
                            SplitPerTaskMaxCount = splitPerTaskMaxCount,//每个task处理的数量
                            StorageMaxTaskCount = 10,//最多可开启存储的线程
                            StoragePerTaskMaxCount = 1000,//每个存储线程最多可处理的数量
                            MaxSeatCount = maxSeatCount,
                            Purchaser = PurchaserType.Qunar,//供应商类型
                            Policies = lstPolicies,//政策数据
                            LstQunarCodes = lstQunarCodes,
                            PolicyType = policyType
                        };
                        IPolicySplit policySplit = new QunarPolicySplit();//去哪儿拆分政策
                        //拆分政策
                        splitResponse = policySplit.PolicySplit(splitQunarRequst);
                        if (splitResponse.ErrCode == ResultType.Failed)
                        {
                            //  MessageBox.Show("拆分政策失败:" + splitResponse.ErrMsg);
                            return new List<Policies>();
                        }
                    }
                    break;
                case PurchaserType.TaoBao:
                    break;
                default:
                    break;
            }
            return splitResponse.PoliciesData;

        }
        #endregion

        #region 过滤政策
       public static List<Policies> FilterPolicy(List<string> lstQunarCodes,List<Policies> lstPolicies,ref bool isAll)
        {
               List<Policies> lstNew = new List<Policies>();
               

               foreach (var x in lstPolicies)
	            {

                   // int day = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).Subtract(Convert.ToDateTime(x.FlightEffectDate.EnsureDateRight())).Days;
                   //// day > 0 ? DateTime.Now.ToString("yyyy-MM-dd") : x.FlightEffectDate.EnsureDateRight();//旅行开始日期
                   //if(day >= 0)
                   //{
                    if (x.DelDegree == 0)
                    {
                        lstNew.Add(x);
                    }
                    else
                    {
                        if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(2) < Convert.ToDateTime(x.FlightExpireDate.EnsureDateRight()))
                        {
                            x.FlightEffectDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(2);
                            if (x.DptCity.Split('/').ToList().Count > 218 && x.ArrCity.Split('/').ToList().Count > 218)
                            {
                                isAll = true;
                            }
                            List<string> lstDptCity = FilterQunarPolicy(x.DptCity.Split('/').ToList(), lstQunarCodes);
                            List<string> lstArrCity = FilterQunarPolicy(x.ArrCity.Split('/').ToList(), lstQunarCodes);
                            if (lstDptCity.Count > 0 && lstArrCity.Count > 0)
                            {
                                x.DptCity = string.Join("/", lstDptCity.ToArray());
                                x.ArrCity = string.Join("/", lstArrCity.ToArray());
                                lstNew.Add(x);
                            }

                        }
                    }
                  // }
                   
              }
                return lstNew;       
       }
        

        #region 筛选
        private static List<string> FilterQunarPolicy(List<string> lstDptCity,List<string> lstQunarCodes)
        {
            List<string> cityNew = new List<string>();
            lstDptCity.ForEach(x =>
            {
                if (lstQunarCodes.Contains(x))
                {
                    cityNew.Add(x);
                }
            });
            lstDptCity.Clear();
            return cityNew;
        }
        #endregion
        #endregion

        #region 去除没有上传的政策列表
        public static Dictionary<QunarPolicyType, List<Policies>> ReserveHaveUploadPolicy(List<Policies> lstPolicies, List<CompleteUploadPolicyDto> completeUploadPolicyCollection)
        {
            Dictionary<QunarPolicyType, List<Policies>> lstPoliciesNew = new Dictionary<QunarPolicyType, List<Policies>>();
            completeUploadPolicyCollection = completeUploadPolicyCollection.Distinct(new ModelComparer()).ToList();
                    completeUploadPolicyCollection.ForEach(x =>
                    {
                        QunarPolicyType type = (QunarPolicyType)Enum.Parse(typeof(QunarPolicyType), x.PolicyType);
                        lstPolicies.ForEach(y =>
                        {
                            if(x.PartenerPolicyId == y.PartnerPolicyId)
                            {
                                y.CommisionMoney += x.CommisionMoney;
                                y.CommisionPoint += x.CommsionPoint;
                                if (lstPoliciesNew.ContainsKey(type))
                                {
                                    if(lstPoliciesNew[type] == null )
                                    {
                                        lstPoliciesNew[type] = new List<Policies>() { y };
                                    }
                                    else
                                    {
                                        lstPoliciesNew[type].Add(y);
                                    }
                                }
                                else
                                {
                                    lstPoliciesNew.Add(type, new List<Policies>() { y});
                                }
                                
                            }
                        });
                    });
                    return lstPoliciesNew;
        }
        #endregion

        #region 保存上次实时上传的时间
       public static void SaveLastRealTimeUpload(int interval,string lockPerson)
       {
           SaveRealTimeUploadRequest saveRealTimeUploadRequst = new SaveRealTimeUploadRequest()
           {
               Interval = interval,
               LockPerson = lockPerson,
               Purchaser = PurchaserType.Qunar,
               Remark = "ip:" + IPAddressHelper.GetExternalIP()
           };
           CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SaveRealTimeUploadUrl"].ToString(), saveRealTimeUploadRequst);

       }
        #endregion

        #region 读取去哪儿三字码列表
       public static List<string> ReadQunarCodes(string code="")
        {
           // Task.Factory.StartNew(() =>
           // {
            string repContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchQunarCodeUrl"].ToString(), new QunarCodeRequest() { Code=code});
            if (string.IsNullOrEmpty(repContent))
            {
                return new List<string>();
            }
            QunarCodeListResponse codeList = JsonConvert.DeserializeObject<QunarCodeListResponse>(repContent);
            return codeList.Codes;
            //});
            //List<string> lstCodes = new List<string>();
            //XmlDocument doc = new XmlDocument();
            //string path = System.IO.Directory.GetCurrentDirectory();
            //string xmlCodes = File.ReadAllText(path + "\\QunarCode.xml");
            //QunarCodeList codeList = XmlHelper.Deserialize(typeof(QunarCodeList), xmlCodes) as QunarCodeList;
            //return codeList.Code;
        }
        #endregion

       #region 载入去哪儿默认配置
       public static QunarUploadConfigResponse LoadQunarUploadConfig()
       {
           try
           {
               string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["LoadDefautQunarConfigUrl"].ToString(), "");
               QunarUploadConfigResponse config = JsonConvert.DeserializeObject<QunarUploadConfigResponse>(responseContent);
               return config;
           }
           catch(Exception ex)
           {
               return new QunarUploadConfigResponse();
           }


       } 
       #endregion

      
       



    }

   public class ModelComparer : IEqualityComparer<CompleteUploadPolicyDto>
   {
       public bool Equals(CompleteUploadPolicyDto x, CompleteUploadPolicyDto y)
       {
           return x.PartenerPolicyId.ToUpper() == y.PartenerPolicyId.ToUpper();
       }
       public int GetHashCode(CompleteUploadPolicyDto obj)
       {
           return obj.PartenerPolicyId.ToUpper().GetHashCode();
       }
   }
    [XmlRoot]
   public class QunarCodeList
    {
        [XmlElement]
       public List<string> Code { get; set; }
    }
}
