using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ND.PolicyUploadService.DtoModel.QunarUploadConfig
{
     [XmlRoot("QunarUploadPolicyConfig")]
    public class QunarUploadConfigResponse
    {
         public QunarUploadConfigResponse()
         {
             SpecialConfig = new SpecialPolicyConfigDto();
         }
        [XmlAttribute("CardType")]
        public string CardType { get; set; }

        [XmlAttribute("MaxAge")]
        public string MaxAge { get; set; }

        [XmlAttribute("MinAge")]
        public string MinAge { get; set; }

        [XmlAttribute("CPCReturnPoint")]
        public string CPCReturnPoint { get; set; }

        [XmlAttribute("CPCReturnPrice")]
        public string CPCReturnPrice { get; set; }

        [XmlAttribute("IsShareFlight")]
        public string IsShareFlight { get; set; }

        [XmlAttribute("IsStopFlight")]
        public string IsStopFlight { get; set; }

        [XmlElement("SpecialConfig")]
        public SpecialPolicyConfigDto SpecialConfig { get; set; }
    }

     [XmlRoot("QunarSpecialPolicyConfig")]
    public class SpecialPolicyConfigDto
    {
        [XmlAttribute("CPAReturn")]
        public string CPAReturn { get; set; }

        [XmlAttribute("CPAChange")]      
        public string CPAChange { get; set; }

        [XmlAttribute("CPAIsEnrosement")]
        public string CPAIsEnrosement { get; set; }

        [XmlAttribute("CPCReturn")]
        public string CPCReturn { get; set; }

        [XmlAttribute("CPCChange")]
        public string CPCChange { get; set; }

        [XmlAttribute("CPCIsEnrosement")]
        public string CPCIsEnrosement { get; set; }

        [XmlAttribute("SpecialTicketRemark")]
        public string SpecialTicketRemark { get; set; }
    }
}
