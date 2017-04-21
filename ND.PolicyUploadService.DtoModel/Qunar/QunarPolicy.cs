using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.DtoModel.dtoEntity.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ND.PolicyUploadService.DtoModel.Qunar
{
     [XmlRoot("PolicyList")]
   public class QunarPolicy
    {
        [XmlIgnore]
        public string encoding { get { return "UTF-8"; } }

        /// <summary>
        /// 政策上传的用户名
        /// </summary>
        [XmlAttribute("username")]
        public string username { get; set; }

        /// <summary>
        /// 政策上传的密码
        /// </summary>
        [XmlAttribute("password")]
        public string password { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [XmlAttribute("ext")]
        public string ext { get; set; }

        /// <summary>
        /// 上传类型
        /// </summary>
        [XmlAttribute("execType")]
        public ExecType execType { get; set; }

        /// <summary>
        /// 去哪儿政策类型
        /// </summary>
        [XmlAttribute("type")]
        public QunarPolicyType type { get; set; }

        public QunarPolicy()
        {
            deletePolicy = new List<QunarDeletePolicyBase>();
            addPolicy = new List<QunarPolicyListBase>();
        }

        /// <summary>
        /// 删除政策节点
        /// </summary>
       [XmlElement("DeletePolicy")]
        public List<QunarDeletePolicyBase> deletePolicy { get; set; }

        /// <summary>
        /// 新增政策节点
        /// </summary>
         [XmlElement("Policy")]
       public List<QunarPolicyListBase> addPolicy { get; set; }
    }
}
