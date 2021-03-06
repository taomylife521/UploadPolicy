﻿using ND.PolicyUploadService.DtoModel.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ND.PolicyUploadService.DtoModel.dtoEntity.Qunar
{
    /// <summary>
    /// 去哪儿政策详情
    /// </summary>
     [XmlRoot("Policy")]
    public class QunarCommonPolicyList : QunarPolicyListBase
    {
        /// <summary>
        /// 航空公司二字码，必须大写，只支持一个航空公司
        /// </summary>
         [XmlAttribute("flightcode")]
        public string flightcode { get; set; }

        /// <summary>
        /// 政策标识 代理商内部政策代码，用于标识内部的政策
        /// </summary>
         [XmlAttribute("policyCode")]
        public string policyCode { get; set; }


        /// <summary>
        /// 出发地 出发机场三字代码，大写且只支持单个出发地
        /// </summary>
        [XmlAttribute("dpt")]
        public string dpt { get; set; }


        /// <summary>
        /// 到达地 到达机场三字码，大写，普通政策支持最多25个机场，多个用英文的“,”分隔
        /// </summary>
           [XmlAttribute("arr")]
        public string arr { get; set; }


        /// <summary>
        /// 航班限制 航班限制，取值如下：所有、适用、不适用
        /// </summary>
         [XmlAttribute("flightNumLimit")]
        public string flightNumLimit { get; set; }


        /// <summary>
        /// 航班号 1.航班限制为所有则不填2.适用则填写适用航班号，多个用英文的“,”分隔，最多不超过25个航班号3.不适用则填写不适用航班号
        /// </summary>
         [XmlAttribute("flightcondition")]
        public string flightcondition { get; set; }

        /// <summary>
        ///周中限制 1234567表示周一、周二、周三、周四、周五、周六、周日，不能为空
        /// </summary>
        [XmlAttribute("daycondition")]
        public string daycondition { get; set; }

        /// <summary>
        /// 舱位 只普通政策允许多个舱位，多个舱位间使用逗号分隔符，如：F,C,X1,M
        /// </summary>
         [XmlAttribute("cabin")]
        public string cabin { get; set; }


        /// <summary>
        /// 票面价类型 取值如下：1. 指定票面价2. Y舱折扣 普通政策，此处取“指定票面价”
        /// </summary>
         [XmlAttribute("discountType")]
        public string discountType { get; set; }


        /// <summary>
        /// 票面价/折扣 如果票面价类型为“指定票面价”此处填写票面价信息(普通政策填写0)，如果为“Y舱折扣”则填写折扣信息（如0.85表示85折）。
        /// </summary>
         [XmlAttribute("discountValue")]
        public string discountValue { get; set; }


        /// <summary>
        /// 返点 5.5表示返5.5个点 CPA返点
        /// </summary>
          [XmlAttribute("returnpoint")]
        public string returnpoint { get; set; }


        /// <summary>
        /// 留钱 支持正负整数 CPA留钱
        /// </summary>
        [XmlAttribute("returnprice")]
        public string returnprice { get; set; }

        /// <summary>
        /// 销售起始日期 格式为yyyy-MM-dd
        /// </summary>
         [XmlAttribute("startdate_ticket")]
        public string startdate_ticket { get; set; }

        /// <summary>
        /// 销售结束日期 格式为yyyy-MM-dd，日期不能早于销售起始日期注意：字段名为“enfdata_ticket”，不是“enddata_ticket”
        /// </summary>
          [XmlAttribute("enfdate_ticket")]
        public string enfdata_ticket { get; set; }


        /// <summary>
        /// 旅行起始日期 格式为yyyy-MM-dd
        /// </summary>
        [XmlAttribute("startdate")]
        public string startdate { get; set; }


        /// <summary>
        /// 旅行结束日期 格式为yyyy-MM-dd，日期不能早于旅行起始日期
        /// </summary>
         [XmlAttribute("enddate")]
        public string enddate { get; set; }


        /// <summary>
        ///提前出票时限 正整数，大于等于0
        /// </summary>
         [XmlAttribute("beforeValidDay")]
        public string beforeValidDay { get; set; }


        /// <summary>
        /// 退改签说明 最大不超过255个字符的文本
        /// </summary>
        [XmlAttribute("backnote")]
        public string backnote { get; set; }

        /// <summary>
        /// 舱位说明 最大不超过255个字符
        /// </summary>
        [XmlAttribute("cabinnote")]
        public string cabinnote { get; set; }

        /// <summary>
        /// 是否自动出票 取值“是”或者“否”
        /// </summary>
        [XmlAttribute("autoTicket")]
        public string autoTicket { get; set; }




       


        /// <summary>
        /// 0，不提供报销凭证1，提供行程单2，提供发票强制为1，必须提供行程单
        /// </summary>
         [XmlAttribute("xcd")]
        public string xcd { get; set; }


        /// <summary>
        /// 常旅客积分 否,不提供 是，提供默认为“是”
        /// </summary>
          [XmlAttribute("flyerpoints")]
        public string flyerpoints { get; set; }

        /// <summary>
        /// 证件类型 0，支持所有证件类型1，只支持身份证购买默认为0，支持所有证件类型
        /// </summary>
            [XmlAttribute("cardType")]
        public string cardType { get; set; }

        /// <summary>
        /// 授权搭桥的OFFICE号 授权其它OFFICE号也可以提取订单的PNR信息。可以为空，最多两个OFFICE号，两个OFFICE号时中间用英文的逗号隔开
        [XmlAttribute("officeno")]
            public string officeno { get; set; }

        /// <summary>
        /// 预定office号 创建PNR时所使用的OFFICE号，必须在代理商PID列表中存在。可以为空，系统使用代理商列表中有权的office
        [XmlAttribute("preOfficeNo")]
        public string preOfficeNo { get; set; }

        /// <summary>
        /// 取值“全部、非共享”，默认为“非共享”
        /// </summary>
        [XmlAttribute("sharedNew")]
        public string sharedNew { get; set; }

        /// <summary>
        /// 取值“全部、经停、非经停”，默认为"全部"，
        /// </summary>
        [XmlAttribute("stop")]
        public string stop { get; set; }

        /// <summary>
        /// CPA投放类型 取值“下浮比例”，默认为“下浮比例”
        /// </summary>
        [XmlAttribute("cpaPutInPriceType")]
        public string cpaPutInPriceType { get; set; }

        /// <summary>
        /// CPA投放指定金额/下浮比例如果为“下浮比例”则填写比例信息（如5表示票面价下浮5个返点为航司允许的最低销售价），支持数字输入，精确小数2位，可为空普通政策不支持“指定金额”
        /// </summary>
        [XmlAttribute("cpaPutInNormalPrice")]
        public string cpaPutInNormalPrice { get; set; }

        /// <summary>
        /// CPC返点 支持数字输入，精确到两位小数，如：15.25，代表返点15.25
        /// </summary>
        [XmlAttribute("cpcReturnPoint")]
        public string cpcReturnPoint { get; set; }

        /// <summary>
        /// CPC留钱 支持正负整数输入
        /// </summary>
        [XmlAttribute("cpcReturnPrice")]
        public string cpcReturnPrice { get; set; }

        /// <summary>
        ///出票速度 支持5位以内正整数输入
        /// </summary>
        [XmlAttribute("ticketTime")]
        public string ticketTime { get; set; }

        ///// <summary>
        ///// 最大购买年龄 0-100整数；允许购买机票的最大年龄；默认为空表示无年龄上限限制
        ///// </summary>
        // [XmlAttribute("maxAge")]
        //public string maxAge { get; set; }

        ///// <summary>
        ///// 最小购买年龄 0-100整数；允许购买机票的最小年龄；默认为空表示无年龄下限限制
        ///// </summary>
        //   [XmlAttribute("minAge")]
        //public string minAge { get; set; }


           
    }
}
