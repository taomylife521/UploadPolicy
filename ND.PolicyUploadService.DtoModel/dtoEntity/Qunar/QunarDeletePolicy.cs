

using ND.PolicyService.Enums.Upload.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ND.PolicyUploadService.DtoModel.dtoEntity.Qunar
{
      [XmlRoot("DeletePolicy")]
    public class QunarDeletePolicy : QunarDeletePolicyBase
    {
        /// <summary>
        /// 去哪儿政策类型
        /// </summary>
         [XmlAttribute("type")]
        public QunarPolicyType type { get; set; }

       /// <summary>
       /// 航空公司二字码，必须大写，只支持一个航空公司
       /// </summary>
        [XmlAttribute("flightcode")]
        public string flightcode { get; set; }

       /// <summary>
       /// 代理商内部政策代码，用于标识内部的政策。支持多个政策代码，多个政策代码以“,”分隔，最多支持1万个政策代码。支持模糊检索，模糊检索部分用“*”代替。例：输入“abcpolicy*”，匹配“abcpolicy%”格式的所有政策（只支持* 在最后）
       /// </summary>
       [XmlAttribute("policyCode")]
        public string policyCode { get; set; }

        /// <summary>
        /// 出发机场三字代码，大写且只支持单个出发地
        /// </summary>
         [XmlAttribute("dpt")]
        public string dpt { get; set; }

        /// <summary>
        /// 到达机场三字码，大写且只支持单个目的地
        /// </summary>
         [XmlAttribute("arr")]
        public string arr { get; set; }

        /// <summary>
        /// 舱位(往返政策对应去程舱位) 舱位代码，大写且只支持单个舱位的录入，不支持子舱位
        /// </summary>
        [XmlAttribute("cabin")]
        public string cabin { get; set; }

        /// <summary>
        ///旅行起始日期（往返政策对应去程旅行起始日期）  格式为yyyy-MM-dd，日期不能早于当天
        /// </summary>
        [XmlAttribute("startdate")]
        public string startdate { get; set; }

        /// <summary>
        ///旅行结束日期（往返政策对应去程旅行结束日期）格式为yyyy-MM-dd，日期不能早于旅行起始日期
        /// </summary>
      [XmlAttribute("enddate")]
        public string enddate { get; set; }
    }
}
