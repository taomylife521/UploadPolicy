﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.DbEntity
{
    [Serializable]
    [DataContract]
   public class Policies
    {
        [DataMember]
        public int IsUpload
        {
            set { isUpload = value; }
            get { return isUpload; }
        }

        public Policies DeepClone()
        {
           BinaryFormatter bFormatter = new BinaryFormatter();
           MemoryStream stream = new MemoryStream();
           bFormatter.Serialize(stream, this);
           stream.Seek(0, SeekOrigin.Begin);
           return (Policies)bFormatter.Deserialize(stream);
       }

        #region Model
        private long _id;
        private DateTime _updatetime = DateTime.Now;
        private int _partnerid = 0;
        private string _partnername = "";
        private string _partnerpolicyid = "";
        private int platformPolicyStatus = 1;
        /// <summary>
        /// 是否已经上传 0--未上传 1-已上传
        /// </summary>
        private int isUpload = 0;
        [DataMember]
        public int PlatformPolicyStatus
        {
            set { platformPolicyStatus = value; }
            get { return platformPolicyStatus; }
        }

        /// <summary>
        /// 政策id
        /// </summary>
        [DataMember]
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 同步时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 供应商id
        /// </summary>
        [DataMember]
        public int PartnerId
        {
            set { _partnerid = value; }
            get { return _partnerid; }
        }
        /// <summary>
        /// 合作伙伴名称
        /// </summary>
        [DataMember]
        public string PartnerName
        {
            set { _partnername = value; }
            get { return _partnername; }
        }
        /// <summary>
        /// 合作伙伴id
        /// </summary>
        [DataMember]
        public string PartnerPolicyId
        {
            set { _partnerpolicyid = value; }
            get { return _partnerpolicyid; }
        }
        #endregion Model

        #region Model
      
        private long _policyid = 0;
        private int _srctype = 0;
        private int _commisiontype = 0;
        private string _comment = "";
        private string _airlinecode = "";
        private string _dptcity = "";
        private string _arrcity = "";
        private string _flightin = "";
        private string _flightout = "";
        private string _seat = "";
        private DateTime _saleeffectdate = Convert.ToDateTime("2099-12-30");
        private DateTime _saleexpiredate = Convert.ToDateTime("2099-12-30");
        private DateTime _saleforbideffectdate = Convert.ToDateTime("2099-12-30");
        private DateTime _saleforbidexpiredate = Convert.ToDateTime("2099-12-30");
        private DateTime _flighteffectdate = Convert.ToDateTime("2099-12-30");
        private DateTime _flightexpiredate = Convert.ToDateTime("2099-12-30");
        private DateTime _flightforbideffectdate = Convert.ToDateTime("2099-12-30");
        private DateTime _flightforbidexpiredate = Convert.ToDateTime("2099-12-30");
        private int _earliestissuedays = 0;
        private int _isfitchild = 0;
        private decimal _commisionpoint = 0M;
        private decimal _commisionmoney = 0M;
        private decimal _platformcommisionpoint = 0M;
        private decimal _platformcommisionmoney = 0M;
        private int _issetprivate = 0;
        private int _privatecount = 0;
        private string _officeno = "";
        private int _needswitchpnr = 0;
        private int _isautoissue = 0;
        private int _ispata = 1;
        private string _bigclientcode = "";
        private int _minimumtraveller = 9;
        private int _isproviderscore = 0;
        private int _issharingflight = 0;
        private int _invoicetype = 1;
        private string _tuigairule = "";
        private string _changeworktime = "";
        private string _returnworktime = "";
        private string _vtworktime = "";
        private string _changeworktimeweekend = "";
        private string _returnworktimeweekend = "";
        private string _vtworktimeweekend = "";
        private string _issueworktime = "";
        private string _issueworktimeweekend = "";
        private string _ticketspeed = "";
        private string _flightcycle = "";
        private string _policytype = "";
        private int _psgtype = 0;
        private string _param1 = "";
        private string _param2 = "";
        private string _param3 = "";
        private string _param4 = "";
        private int _policystatus = 1;
        private DateTime _createtime = DateTime.Now;
        private int _deldegree = 1;

       
        /// <summary>
        /// 政策id
        /// </summary>
        [DataMember]
        public long PolicyId
        {
            set { _policyid = value; }
            get { return _policyid; }
        }
        /// <summary>
        /// 航程类型[1:单程/2:往返/3:单程及往返/4:联程/5:缺口程]
        /// </summary>
        [DataMember]
        public int SrcType
        {
            set { _srctype = value; }
            get { return _srctype; }
        }
        /// <summary>
        /// 产品类别 政策类型[1:普通/2:特殊/3:外放(特价舱)/其他为空代表全国]
        /// </summary>
        [DataMember]
        public int CommisionType
        {
            set { _commisiontype = value; }
            get { return _commisiontype; }
        }
        /// <summary>
        /// 政策备注
        /// </summary>
        [DataMember]
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 航空公司代码
        /// </summary>
        [DataMember]
        public string AirlineCode
        {
            set { _airlinecode = value; }
            get { return _airlinecode; }
        }
        /// <summary>
        /// 出发城市
        /// </summary>
        [DataMember]
        public string DptCity
        {
            set { _dptcity = value; }
            get { return _dptcity; }
        }
        /// <summary>
        /// 抵达城市
        /// </summary>
        [DataMember]
        public string ArrCity
        {
            set { _arrcity = value; }
            get { return _arrcity; }
        }
        /// <summary>
        /// 适用航班
        /// </summary>
        [DataMember]
        public string FlightIn
        {
            set { _flightin = value; }
            get { return _flightin; }
        }
        /// <summary>
        /// 不适用航班
        /// </summary>
        [DataMember]
        public string FlightOut
        {
            set { _flightout = value; }
            get { return _flightout; }
        }
        /// <summary>
        /// 舱位
        /// </summary>
        [DataMember]
        public string Seat
        {
            set { _seat = value; }
            get { return _seat; }
        }
        /// <summary>
        /// 出票有效期开始时间(销售出票开始日期)格式：2013-05-04
        /// </summary>
        [DataMember]
        public DateTime SaleEffectDate
        {
            set { _saleeffectdate = value; }
            get { return _saleeffectdate; }
        }
        /// <summary>
        /// 出票过期时间(销售出票结束日期)2015-02-03
        /// </summary>
        [DataMember]
        public DateTime SaleExpireDate
        {
            set { _saleexpiredate = value; }
            get { return _saleexpiredate; }
        }
        /// <summary>
        /// 禁止出票开始时间(禁止销售出票开始日期)
        /// </summary>
        [DataMember]
        public DateTime SaleForbidEffectDate
        {
            set { _saleforbideffectdate = value; }
            get { return _saleforbideffectdate; }
        }
        /// <summary>
        /// 禁止出票结束时间(禁止销售出票结束日期)
        /// </summary>
        [DataMember]
        public DateTime SaleForbidExpireDate
        {
            set { _saleforbidexpiredate = value; }
            get { return _saleforbidexpiredate; }
        }
        /// <summary>
        /// 航班有效起始日期(旅行起始日期)
        /// </summary>
        [DataMember]
        public DateTime FlightEffectDate
        {
            set { _flighteffectdate = value; }
            get { return _flighteffectdate; }
        }
        /// <summary>
        /// 航班过期时间(旅行结束日期)
        /// </summary>
        [DataMember]
        public DateTime FlightExpireDate
        {
            set { _flightexpiredate = value; }
            get { return _flightexpiredate; }
        }
        /// <summary>
        /// 禁止航班有效起始日期(禁止旅行起始日期)
        /// </summary>
        [DataMember]
        public DateTime FlightForbidEffectDate
        {
            set { _flightforbideffectdate = value; }
            get { return _flightforbideffectdate; }
        }
        /// <summary>
        /// 禁止航班有效结束日期(禁止旅行结束日期)
        /// </summary>
        [DataMember]
        public DateTime FlightForbidExpireDate
        {
            set { _flightforbidexpiredate = value; }
            get { return _flightforbidexpiredate; }
        }
        /// <summary>
        /// 提前预定天数
        /// </summary>
        [DataMember]
        public int EarliestIssueDays
        {
            set { _earliestissuedays = value; }
            get { return _earliestissuedays; }
        }
        /// <summary>
        /// 是否适合儿童 0-不适合 1-适合
        /// </summary>
        [DataMember]
        public int IsFitChild
        {
            set { _isfitchild = value; }
            get { return _isfitchild; }
        }
        /// <summary>
        /// 返点
        /// </summary>
        [DataMember]
        public decimal CommisionPoint
        {
            set { _commisionpoint = value; }
            get { return _commisionpoint; }
        }
        /// <summary>
        /// 返现(留钱)
        /// </summary>
        [DataMember]
        public decimal CommisionMoney
        {
            set { _commisionmoney = value; }
            get { return _commisionmoney; }
        }

        /// <summary>
        /// 对外暴漏返点
        /// </summary>
        [DataMember]
        public decimal PlatformCommisionPoint
        {
            set { _commisionpoint = value; }
            get { return _commisionpoint; }
        }
        /// <summary>
        /// 对外暴漏返现(留钱)
        /// </summary>
        [DataMember]
        public decimal PlatformCommisionMoney
        {
            set { _commisionmoney = value; }
            get { return _commisionmoney; }
        }
        /// <summary>
        /// 是否设置库存 0-不设置 1-设置
        /// </summary>
        [DataMember]
        public int isSetPrivate
        {
            set { _issetprivate = value; }
            get { return _issetprivate; }
        }
        /// <summary>
        /// 当isSetPrivate为1时该值必须填写
        /// </summary>
        [DataMember]
        public int PrivateCount
        {
            set { _privatecount = value; }
            get { return _privatecount; }
        }
        /// <summary>
        /// 授权office号
        /// </summary>
        [DataMember]
        public string OfficeNo
        {
            set { _officeno = value; }
            get { return _officeno; }
        }
        /// <summary>
        /// 是否需换pnr0-不需要 1- 需要
        /// </summary>
        [DataMember]
        public int NeedSwitchPNR
        {
            set { _needswitchpnr = value; }
            get { return _needswitchpnr; }
        }
        /// <summary>
        /// 0-不是自动出票 1-自动出票
        /// </summary>
        [DataMember]
        public int IsAutoIssue
        {
            set { _isautoissue = value; }
            get { return _isautoissue; }
        }
        /// <summary>
        /// 是否需要验价 1-需要 0-不需要
        /// </summary>
        [DataMember]
        public int IsPata
        {
            set { _ispata = value; }
            get { return _ispata; }
        }
        /// <summary>
        /// 大客户编码
        /// </summary>
        [DataMember]
        public string BigClientCode
        {
            set { _bigclientcode = value; }
            get { return _bigclientcode; }
        }
        /// <summary>
        /// 最少成行人数
        /// </summary>
        [DataMember]
        public int MinimumTraveller
        {
            set { _minimumtraveller = value; }
            get { return _minimumtraveller; }
        }
        /// <summary>
        /// 0-不提供常旅客积分 1- 提供常旅客积分
        /// </summary>
        [DataMember]
        public int IsProviderScore
        {
            set { _isproviderscore = value; }
            get { return _isproviderscore; }
        }
        /// <summary>
        /// 0-不是共享航班 1-共享航班
        /// </summary>
        [DataMember]
        public int IsSharingFlight
        {
            set { _issharingflight = value; }
            get { return _issharingflight; }
        }
        /// <summary>
        /// 0-不提供行程单 1-提供行程单 
        /// </summary>
        [DataMember]
        public int InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 退改规定
        /// </summary>
        [DataMember]
        public string TuiGaiRule
        {
            set { _tuigairule = value; }
            get { return _tuigairule; }
        }
        /// <summary>
        /// 改期工作日工作时间
        /// </summary>
        [DataMember]
        public string ChangeWorkTime
        {
            set { _changeworktime = value; }
            get { return _changeworktime; }
        }
        /// <summary>
        /// 退票工作日工作时间
        /// </summary>
        [DataMember]
        public string ReturnWorkTime
        {
            set { _returnworktime = value; }
            get { return _returnworktime; }
        }
        /// <summary>
        /// 废票工作日工作时间
        /// </summary>
        [DataMember]
        public string VtWorkTime
        {
            set { _vtworktime = value; }
            get { return _vtworktime; }
        }
        /// <summary>
        /// 改期周末工作时间
        /// </summary>
        [DataMember]
        public string ChangeWorkTimeWeekend
        {
            set { _changeworktimeweekend = value; }
            get { return _changeworktimeweekend; }
        }
        /// <summary>
        /// 退票周末工作时间
        /// </summary>
        [DataMember]
        public string ReturnWorkTimeWeekend
        {
            set { _returnworktimeweekend = value; }
            get { return _returnworktimeweekend; }
        }
        /// <summary>
        /// 废票周末工作时间
        /// </summary>
        [DataMember]
        public string VtWorkTimeWeekend
        {
            set { _vtworktimeweekend = value; }
            get { return _vtworktimeweekend; }
        }
        /// <summary>
        /// 工作时间（出票时间）
        /// </summary>
        [DataMember]
        public string IssueWorkTime
        {
            set { _issueworktime = value; }
            get { return _issueworktime; }
        }
        /// <summary>
        /// 出票周末工作时间
        /// </summary>
        [DataMember]
        public string IssueWorkTimeWeekend
        {
            set { _issueworktimeweekend = value; }
            get { return _issueworktimeweekend; }
        }
        /// <summary>
        /// 出票效率
        /// </summary>
        [DataMember]
        public string TicketSpeed
        {
            set { _ticketspeed = value; }
            get { return _ticketspeed; }
        }
        /// <summary>
        /// 如“1,2,3,4”表示周一、周二、周三、周四的航班可用。若为空，则表示都可用。
        /// </summary>
        [DataMember]
        public string FlightCycle
        {
            set { _flightcycle = value; }
            get { return _flightcycle; }
        }
        /// <summary>
        /// 政策类型（BSp or B2B）
        /// </summary>
        [DataMember]
        public string PolicyType
        {
            set { _policytype = value; }
            get { return _policytype; }
        }
        /// <summary>
        /// 乘客类型[1:成人/2:儿童]
        /// </summary>
        [DataMember]
        public int PsgType
        {
            set { _psgtype = value; }
            get { return _psgtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Param1
        {
            set { _param1 = value; }
            get { return _param1; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Param2
        {
            set { _param2 = value; }
            get { return _param2; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Param3
        {
            set { _param3 = value; }
            get { return _param3; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Param4
        {
            set { _param4 = value; }
            get { return _param4; }
        }
        /// <summary>
        /// 0-无效 1-有效
        /// </summary>
        [DataMember]
        public int PolicyStatus
        {
            set { _policystatus = value; }
            get { return _policystatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 0-已删除 1-未删除
        /// </summary>
        [DataMember]
        public int DelDegree
        {
            set { _deldegree = value; }
            get { return _deldegree; }
        }
        #endregion Model
    }
}
