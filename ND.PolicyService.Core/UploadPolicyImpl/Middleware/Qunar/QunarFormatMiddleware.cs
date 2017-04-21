using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.dtoEntity.Qunar;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyService.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using ND.PolicyUploadService.DtoModel.SeatDiscount;

namespace ND.PolicyUploadService.Core.impl.Middleware.Qunar
{
    /// <summary>
    /// 去哪儿格式化数据
    /// </summary>
    public class QunarFormatMiddleware : HandlerMiddleware
    {
        ConcurrentBag<QunarDeletePolicyBase> qunarDelPolicy = new ConcurrentBag<QunarDeletePolicyBase>();
        ConcurrentBag<QunarPolicyListBase> qunarAddPolicy = new ConcurrentBag<QunarPolicyListBase>();
         /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarFormatMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }
        public QunarFormatMiddleware()
        { }
        public override void Invoke(IHandlerContext context)
        {
            try
            {

                QunarUploadPolicyRequest qunarRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);

                Dictionary<UploadTypeDetail, PolicyRecord> dicRec = new Dictionary<UploadTypeDetail, PolicyRecord>();

                List<SeatDiscountDto> seatDiscountList = new List<SeatDiscountDto>();
                if (qunarRequest.PolicyType != QunarPolicyType.COMMON)
                {
                    SeatDiscountListResponse seatRep = JsonConvert.DeserializeObject<SeatDiscountListResponse>(CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SeatDiscountListUrl"], ""));//获取舱位折扣列表
                    seatDiscountList = seatRep.SeatDiscountList;
                }
                Dictionary<string, string> lstUploadedId = new Dictionary<string,string>();
                #region 选择要封装哪个节点
                foreach (KeyValuePair<UploadTypeDetail, List<Policies>> item in qunarRequest.PolicyData)
                {
                    if (item.Value.Count > 0)
                    {
                        Policies ps = item.Value.LastOrDefault();
                        //if (item.Key != UploadTypeDetail.IncrementalDelete)
                        //{
                           
                            item.Value.ForEach(x =>
                            {
                                if (!lstUploadedId.ContainsKey(x.Id.ToString()))
                                {
                                    
                                    lstUploadedId.Add(x.Id.ToString(),x.PartnerPolicyId);
                                }
                            });
                       // }
                        dicRec.Add(item.Key, new PolicyRecord { LastPolicyId = ps.Id, LastUpdateTime = ps.UpdateTime });
                        DivideTask(item.Key, item.Value, qunarRequest,seatDiscountList, ChangeToQunarAddPolicy, ChangeToQunarDelPolicy);//开始多任务处理
                    }
                }
                #endregion
                string logPath = qunarRequest.FormatFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + DateTime.Now.Hour + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml";
                string zipPath = qunarRequest.FormatZipFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + DateTime.Now.Hour + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".zip";
               
                if(qunarAddPolicy.ToList().Count <= 0 && qunarDelPolicy.ToList().Count <= 0)
                {
                    OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "经过去哪儿格式化组件筛选完后没有要上传的政策" });
                    return;
                }

                #region 保存上传记录
                PolicyNotifyRequest notifyRequest = new PolicyNotifyRequest();
                SaveNotifyResponse notifyRes = new SaveNotifyResponse();

                notifyRequest = new PolicyNotifyRequest()
                      {
                          // DicRec = dicRec,
                          UploadType = qunarRequest.UploadType,
                          PolicyRec = context.UploadResponse.PolicyRec[qunarRequest.UploadType],//最后一条政策记录
                          Purchaser = PurchaserType.Qunar,
                          NotifyResult = 0,
                          FileNamePath = zipPath,
                          ResponseParams = "",
                          RequestParams = qunarRequest.SqlWhere,
                          Remark = IPAddressHelper.GetInternalIP(),
                          OperName = qunarRequest.OperName,
                          UploadCount = qunarRequest.UploadCount,
                          BeforePolicyRec = context.UploadResponse.BeforePolicyRecord == null ? new PolicyRecord() : context.UploadResponse.BeforePolicyRecord,
                          CommisionMoney = qunarRequest.CommisionMoney,
                          CommisionPoint = qunarRequest.CommsionPoint,
                          UploadPolicyIds = lstUploadedId,
                          PolicyType = (PoliciesType)Enum.Parse(typeof(PoliciesType), qunarRequest.PolicyType.ToString())

                      };
                string notifyContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SavePolicyNotifyUrl"].ToString(), notifyRequest);
                notifyRes = JsonConvert.DeserializeObject<SaveNotifyResponse>(notifyContent);//先查询一遍，获取要上传的政策
                if (notifyRes.ErrCode == ResultType.Failed)
                {
                    context.UploadResponse = new UploadPolicyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "保存上传记录失败" };
                    return;
                }
                #endregion

                #region 封装去哪儿政策类
                    QunarPolicy policyBase = new QunarPolicy()
                    {
                        username = ConfigurationManager.AppSettings["QunarUsername"].ToString(),
                        password = ConfigurationManager.AppSettings["QunarPassword"].ToString(),
                        type = qunarRequest.PolicyType,
                        execType = qunarRequest.UploadType == UploadType.FullUpload ? ExecType.FULL : ExecType.ADD,
                        addPolicy = qunarAddPolicy.ToList(),//添加政策节点
                        deletePolicy = qunarDelPolicy.ToList(),//删除政策节点
                        ext = JsonConvert.SerializeObject(notifyRes.UploadStatusId)//保存xml文件路径
                    };
                    #endregion

                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "封装去哪儿政策类成功,扩展信息:" + JsonConvert.SerializeObject(notifyRes.UploadStatusId) + ",开始序列化成功xml..." });
                string xmlContent = XmlHelper.Serializer(typeof(QunarPolicy), policyBase);//序列化成xml
                CoreHelper.CreateFile(logPath, xmlContent);//创建文件
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "序列化xml并创建文件成功,生成的xml文件路径:" + logPath + ",开始压缩并上传..." });
                context.UploadResponse.FormatPolicyFilePath = logPath;//保存xml文件路径
                context.UploadResponse.FormatPolicyZipFilePath = zipPath;
                context.UploadResponse.UploadStatusId = notifyRes.UploadStatusId;
                Next.Invoke(context);
            }catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Exception, Msg = "格式化去哪儿数据失败!" + JsonConvert.SerializeObject(ex) });
                context.UploadResponse = new UploadPolicyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "格式化去哪儿数据失败!", Excption = ex };
                return;
            }
        }


        #region 划分任务转换去哪儿格式数据
        private void DivideTask(UploadTypeDetail uploadTypeDetail, List<Policies> lstPolicies, QunarUploadPolicyRequest qunarRequest,List<SeatDiscountDto> seatDiscountList, Func<Policies,QunarUploadConfigResponse, QunarPolicyType,List<SeatDiscountDto>, QunarPolicyListBase> funcAdd, Func<Policies, QunarPolicyType, QunarDeletePolicyBase> funcDel)
        {
            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();
          Task mangerTask= Task.Factory.StartNew(() =>//开启管理线程分配任务
            {
                int currentTaskCount = 0;
                while (lstPolicies.Count > 0)
                {
                    while (currentTaskCount >= qunarRequest.MaxTaskCount)
                    {
                        int index = Task.WaitAny(tasks.ToArray());//等待任何一个线程完成
                        currentTaskCount--;
                    }
                    List<Policies> childCollection = lstPolicies.Take(qunarRequest.PerTaskCount).ToList();
                    if (childCollection.Count > 0)
                    {
                        lstPolicies.RemoveRange(0, childCollection.Count);
                        Task subTask = Task.Factory.StartNew(() =>//开启子线程处理
                        {
                            #region 子线程执行的方法
                            foreach (Policies item in childCollection)
                            {

                                switch (uploadTypeDetail)
                                {
                                    case UploadTypeDetail.FullUpload://全量
                                    case UploadTypeDetail.IncrementalAdd://增量添加
                                        {
                                            QunarPolicyListBase baseList = funcAdd(item, qunarRequest.DefaultUploadConfig, qunarRequest.PolicyType, seatDiscountList);
                                            if (baseList != null)
                                            {
                                                qunarAddPolicy.Add(baseList);
                                            }
                                        }
                                        break;
                                    case UploadTypeDetail.IncrementalDelete:
                                        {
                                            qunarDelPolicy.Add(funcDel(item, qunarRequest.PolicyType));
                                        }
                                        break;
                                    default:
                                        break;
                                }

                            } 
                            #endregion
                        });
                        currentTaskCount++;
                        tasks.Add(subTask);
                    }
                }
            });
          Task.WaitAll(mangerTask);
          Task.WaitAll(tasks.ToArray());
        }
        #endregion


        #region 转换成去哪删除政策格式
        /// <summary>
        /// 转换成去哪删除政策格式
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        private QunarDeletePolicyBase ChangeToQunarDelPolicy(Policies policy, QunarPolicyType policyType)
        {
            QunarDeletePolicyBase policyList = new QunarDeletePolicyBase();
            #region 根据上传类型封装不同的政策节点
            switch (policyType)
            {
                case QunarPolicyType.APPLY://单程申请
                    break;
                case QunarPolicyType.COMMON://单程普通
                    {
                        policyList = ChangeToQunarCommonDelPolicy(policy,policyType);
                    }
                    break;
                case QunarPolicyType.CUSTOMER://包机切位
                    break;
                case QunarPolicyType.LOWPRICE://特价政策
                    break;
                case QunarPolicyType.PREPAY://单程预付
                    {
                        policyList = ChangeToQunarCommonDelPolicy(policy, policyType);
                    }
                    break;
                case QunarPolicyType.ROUNDALL://往返所有类型
                    break;
                case QunarPolicyType.ROUNDPREPAY://往返预付
                    break;
                case QunarPolicyType.ROUNDSPECIAL://往返特价
                    break;
                case QunarPolicyType.SINGLEALL://单程所有类型
                    break;
                default:
                    break;
            }
            #endregion
            return policyList;
        }
        #endregion

        #region 转换成去哪儿新增政策格式
        /// <summary>
        /// 转换成去哪儿新增政策格式
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        private QunarPolicyListBase ChangeToQunarAddPolicy(Policies policy,QunarUploadConfigResponse defaultUploadConfig, QunarPolicyType policyType,List<SeatDiscountDto> seatDiscountList)
        {
            QunarPolicyListBase policyList = new QunarPolicyListBase();
            #region 根据上传类型封装不同的政策节点
            switch (policyType)
            {
                case QunarPolicyType.APPLY://单程申请
                    break;
                case QunarPolicyType.COMMON://单程普通
                    {
                        policyList = ChangeToQunarCommonPolicy(policy, defaultUploadConfig);
                    }
                    break;
                case QunarPolicyType.CUSTOMER://包机切位
                    break;
                case QunarPolicyType.LOWPRICE://特价政策
                    break;
                case QunarPolicyType.PREPAY://单程预付
                    {
                        policyList = ChangeToQunarPrepayPolicy(policy, defaultUploadConfig, seatDiscountList);
                    }
                    break;
                case QunarPolicyType.ROUNDALL://往返所有类型
                    break;
                case QunarPolicyType.ROUNDPREPAY://往返预付
                    break;
                case QunarPolicyType.ROUNDSPECIAL://往返特价
                    break;
                case QunarPolicyType.SINGLEALL://单程所有类型
                    break;
                default:
                    break;
            }
            #endregion
            return policyList;
        }
        #endregion 


        #region 政策删除节点

        #region 普通政策删除节点
        /// <summary>
        /// 转换成去哪删除政策格式
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        private QunarDeletePolicyBase ChangeToQunarCommonDelPolicy(Policies policy, QunarPolicyType policyType)
        {
            QunarDeletePolicy qunarDelPolicy = new QunarDeletePolicy();
            //qunarDelPolicy.arr = policy.ArrCity;//到达机场三字码
            //qunarDelPolicy.cabin = policy.Seat;//舱位
            //qunarDelPolicy.dpt = policy.DptCity;//出发机场三字码
            //qunarDelPolicy.flightcode = policy.AirlineCode.ToString().ToUpper();//航空公司代码
            qunarDelPolicy.policyCode = policy.PartnerPolicyId.ToString();//自有政策标识
            //qunarDelPolicy.startdate = policy.FlightEffectDate.ToString("yyyy-MM-dd");//旅行开始日期
            //qunarDelPolicy.enddate = policy.FlightExpireDate.ToString("yyyy-MM-dd");//旅行结束日期
            qunarDelPolicy.type = policyType;

            return qunarDelPolicy;
        }
        #endregion

        #endregion

        #region 政策添加节点

        #region 转换成去哪儿普通政策
        private QunarCommonPolicyList ChangeToQunarCommonPolicy(Policies policy,QunarUploadConfigResponse defaultUploadConfig)
        {
            QunarCommonPolicyList qunarPolicy = new QunarCommonPolicyList();
            List<string> lstArr = policy.ArrCity.Replace("/", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ArrayList arrArrCity = new ArrayList();
            int index = 0;
            lstArr.ForEach(x =>
            {
                if (index < 25)
                {
                    arrArrCity.Add(x);
                }
                index++;
            });
            qunarPolicy.arr = lstArr.Count > 25 ? string.Join(",", arrArrCity.ToArray()).ToUpper() : policy.ArrCity.Replace("/", ",").ToUpper();//抵达机场??
            qunarPolicy.autoTicket = policy.IsAutoIssue == 1 ? "是" : "否";//是否自动出票
            qunarPolicy.backnote = string.IsNullOrEmpty(policy.TuiGaiRule) ? null : policy.TuiGaiRule;//退改签规则
            qunarPolicy.beforeValidDay = policy.EarliestIssueDays.ToString();//提前预定天数
            qunarPolicy.cabin = string.Join(",", policy.Seat.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToUpper();//舱位
            qunarPolicy.cabinnote = policy.Comment == "" ? "无" : policy.Comment;//舱位说明
            qunarPolicy.cardType = defaultUploadConfig.CardType.ToString();//证件类型，默认支持所有
            qunarPolicy.daycondition = policy.FlightCycle.Replace("/", "");
            qunarPolicy.discountType = "指定票面价";//折扣类型??
            qunarPolicy.discountValue = "0";//折扣信息??
            qunarPolicy.dpt = policy.DptCity.Substring(0, 3).Replace("/", ",").ToUpper();//出发机场??
            qunarPolicy.enddate = policy.FlightExpireDate.EnsureDateRight();//旅行结束日期
            qunarPolicy.enfdata_ticket = policy.SaleExpireDate.EnsureDateRight();//销售结束日期
            qunarPolicy.flightcode = policy.AirlineCode.ToUpper();//航空公司二字码
            if (policy.FlightIn == "" && policy.FlightOut == "")
            {
                qunarPolicy.flightcondition = "";//航班限制所有
                qunarPolicy.flightNumLimit = "所有";//
            }
            else if (policy.FlightIn != "")
            {
                List<string> lstFlightIn = policy.FlightIn.Replace("/", ",").Split(',').ToList();
                ArrayList arrFlightIn = new ArrayList();
                int n = 0;
                lstFlightIn.ForEach(x =>
                {
                    if (n < 25)
                    {
                        arrFlightIn.Add(qunarPolicy.flightcode + x);
                    }
                    n++;
                });
                qunarPolicy.flightcondition = arrFlightIn.Count <= 0 ? "" : string.Join(",", arrFlightIn.ToArray());
                qunarPolicy.flightNumLimit = "适用";//
            }
            else if (policy.FlightOut != "")
            {
                List<string> lstFlightOut = policy.FlightOut.Replace("/", ",").Split(',').ToList();
                ArrayList arrFlightOut = new ArrayList();
                int m = 0;
                lstFlightOut.ForEach(x =>
                {
                    if (m < 25)
                    {
                        arrFlightOut.Add(qunarPolicy.flightcode + x);
                    }
                    m++;
                });
                qunarPolicy.flightcondition = arrFlightOut.Count <= 0 ? "" : string.Join(",", arrFlightOut.ToArray());
                qunarPolicy.flightNumLimit = "不适用";//
            }
            qunarPolicy.flyerpoints = policy.IsProviderScore == 0 ? "否" : "是";//是否提供常旅客积分
            qunarPolicy.policyCode = policy.PartnerPolicyId.ToString();
            qunarPolicy.returnpoint = Convert.ToDouble(policy.CommisionPoint).ToString("0.00");// Convert.ToDouble(policy.CommisionPoint).ToString("0.00");//返点
            qunarPolicy.returnprice = decimal.Floor(policy.CommisionMoney).ToString();//decimal.Floor(policy.CommisionMoney).ToString();//留钱
            qunarPolicy.officeno = policy.OfficeNo.ToString();//授权officeNo
            qunarPolicy.stop = defaultUploadConfig.IsStopFlight;
            //qunarPolicy.cpaPutInPriceType = "下浮比例";
            //qunarPolicy.cpaPutInNormalPrice = 
            qunarPolicy.cpcReturnPoint = defaultUploadConfig.CPCReturnPoint;//返点
            qunarPolicy.cpcReturnPrice = decimal.Floor(Convert.ToDecimal(defaultUploadConfig.CPCReturnPrice)).ToString();//留钱
            qunarPolicy.sharedNew = defaultUploadConfig.IsShareFlight;
            qunarPolicy.startdate = policy.FlightEffectDate.ToString("yyyy-MM-dd");
            qunarPolicy.startdate_ticket = policy.SaleEffectDate.EnsureDateRight();//销售开始日期
            qunarPolicy.xcd = policy.InvoiceType.ToString();//是否提供行程单
            return qunarPolicy;
        }
        #endregion

        #region 转换成去哪儿单程预付政策
        private QunarPrepayPolicyList ChangeToQunarPrepayPolicy(Policies policy, QunarUploadConfigResponse defaultUploadConfig, List<SeatDiscountDto> seatDiscountList)
        {
            QunarPrepayPolicyList qunarPolicy = new QunarPrepayPolicyList();
            qunarPolicy.flightcode = policy.AirlineCode.ToUpper();//航空公司二字码
            qunarPolicy.policyCode = policy.PartnerPolicyId.ToString();
            qunarPolicy.dpt = policy.DptCity.Substring(0, 3).Replace("/", ",").ToUpper();//出发机场??
            //List<string> lstArr = policy.ArrCity.Replace("/", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //ArrayList arrArrCity = new ArrayList();
            //int index = 0;
            //lstArr.ForEach(x =>
            //{
            //    if (index < 25)
            //    {
            //        arrArrCity.Add(x);
            //    }
            //    index++;
            //});
            qunarPolicy.arr = policy.ArrCity.Substring(0, 3).Replace("/", ",").ToUpper();
            #region 包含航班不包含航班
            if (policy.FlightIn == "" && policy.FlightOut == "")
            {
                qunarPolicy.flightcondition = "";//航班限制所有
                qunarPolicy.flightNumLimit = "所有";//
            }
            else if (policy.FlightIn != "")
            {
                List<string> lstFlightIn = policy.FlightIn.Replace("/", ",").Split(',').ToList();
                ArrayList arrFlightIn = new ArrayList();
                int n = 0;
                lstFlightIn.ForEach(x =>
                {
                    if (n < 25)
                    {
                        arrFlightIn.Add(qunarPolicy.flightcode + x);
                    }
                    n++;
                });
                qunarPolicy.flightcondition = arrFlightIn.Count <= 0 ? "" : string.Join(",", arrFlightIn.ToArray());
                qunarPolicy.flightNumLimit = "适用";//
            }
            else if (policy.FlightOut != "")
            {
                List<string> lstFlightOut = policy.FlightOut.Replace("/", ",").Split(',').ToList();
                ArrayList arrFlightOut = new ArrayList();
                int m = 0;
                lstFlightOut.ForEach(x =>
                {
                    if (m < 25)
                    {
                        arrFlightOut.Add(qunarPolicy.flightcode + x);
                    }
                    m++;
                });
                qunarPolicy.flightcondition = arrFlightOut.Count <= 0 ? "" : string.Join(",", arrFlightOut.ToArray());
                qunarPolicy.flightNumLimit = "不适用";//
            } 
            #endregion
            qunarPolicy.daycondition = policy.FlightCycle.Replace("/", "");
            string seat= string.Join(",", policy.Seat.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToArray())[0].ToString().ToUpper();//舱位
            qunarPolicy.cabin = seat;
            qunarPolicy.discountType = "Y舱折扣";//折扣类型
            SeatDiscountDto seatDto= seatDiscountList.Where(x=>x.AirlineCode==policy.AirlineCode && x.Seat==seat).FirstOrDefault();
            if(seatDto == null)
            {
                return null;
            }
            qunarPolicy.discountValue = seatDto.Discount;//折扣信息??
            qunarPolicy.returnpoint = Convert.ToDouble(policy.CommisionPoint).ToString("0.00");// Convert.ToDouble(policy.CommisionPoint).ToString("0.00");//返点
            qunarPolicy.returnprice = decimal.Floor(policy.CommisionMoney).ToString();//decimal.Floor(policy.CommisionMoney).ToString();//留钱
            qunarPolicy.startdate_ticket = policy.SaleEffectDate.EnsureDateRight();//销售开始日期
            qunarPolicy.enfdata_ticket = policy.SaleExpireDate.EnsureDateRight();//销售结束日期
            qunarPolicy.startdate = policy.FlightEffectDate.ToString("yyyy-MM-dd");
            qunarPolicy.enddate = policy.FlightExpireDate.EnsureDateRight();//旅行结束日期
            qunarPolicy.deptTimeSlot = "0000-2359";//航班起飞时间
            qunarPolicy.beforeValidDay = policy.EarliestIssueDays.ToString();//提前预定天数
            //qunarPolicy.earliestBeforeValidDay = "";//正整数，大于等于0。表示用户至多需要提前几天预订机票
            qunarPolicy.backnote =string.IsNullOrEmpty(policy.TuiGaiRule) ? "无" :policy.TuiGaiRule;//退改签规则
            qunarPolicy.cabinnote = policy.Comment == "" ? "无" : policy.Comment;//舱位说明
            qunarPolicy.autoTicket = policy.IsAutoIssue == 1 ? "是" : "否";//是否自动出票
            qunarPolicy.canPay = "是";
            qunarPolicy.needPnr = "是";
            qunarPolicy.pata = "是";
            qunarPolicy.officeno = policy.OfficeNo.ToString();//授权officeNo
            qunarPolicy.xcd = "2";//是否提供行程单
            qunarPolicy.flyerpoints = policy.IsProviderScore == 0 ? "否" : "是";//是否提供常旅客积分
            qunarPolicy.cardType = defaultUploadConfig.CardType.ToString();//证件类型，默认支持所有
            qunarPolicy.maxAge = defaultUploadConfig.MaxAge.ToString();
            qunarPolicy.minAge = defaultUploadConfig.MinAge.ToString();
            qunarPolicy.returnRule = defaultUploadConfig.SpecialConfig.CPAReturn;
            qunarPolicy.changeRule = defaultUploadConfig.SpecialConfig.CPAChange;
            qunarPolicy.endorsement = defaultUploadConfig.SpecialConfig.CPAIsEnrosement;
            qunarPolicy.specialRule = defaultUploadConfig.SpecialConfig.SpecialTicketRemark;
            qunarPolicy.sharedNew = defaultUploadConfig.IsShareFlight;
            qunarPolicy.stop = defaultUploadConfig.IsStopFlight;
            qunarPolicy.cpcReturnPoint = defaultUploadConfig.CPCReturnPoint;//返点
            qunarPolicy.cpcReturnPrice = decimal.Floor(Convert.ToDecimal(defaultUploadConfig.CPCReturnPrice)).ToString();//留钱
            qunarPolicy.cpcReturnRule = defaultUploadConfig.SpecialConfig.CPCReturn;
            qunarPolicy.cpcChangeRule = defaultUploadConfig.SpecialConfig.CPCChange;
            qunarPolicy.cpcEndorsement = defaultUploadConfig.SpecialConfig.CPCIsEnrosement;
            qunarPolicy.specialRule = defaultUploadConfig.SpecialConfig.SpecialTicketRemark;
            return qunarPolicy;
        }
        #endregion 

        #endregion
    }
}
