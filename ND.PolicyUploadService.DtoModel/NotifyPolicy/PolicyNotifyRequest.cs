using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
   public class PolicyNotifyRequest
    {
        /// <summary>
        /// 供应商
        /// </summary>
        [DataMember]
       public PurchaserType Purchaser { get; set; }

        


        /// <summary>
        /// 上传类型
        /// </summary>
        [DataMember]
        public UploadType UploadType { get; set; }

        /// <summary>
        /// 保存上次上传的记录
        /// </summary>
        [DataMember]
        public PolicyRecord BeforePolicyRec { get; set; }

        /// <summary>
        /// 保存最新上传的记录
        /// </summary>
        [DataMember]
       public  PolicyRecord PolicyRec { get; set; }

        /// <summary>
        /// 保存上次上传的文件路径
        /// </summary>
        [DataMember]
        public string FileNamePath { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public int NotifyResult { get; set; }

        /// <summary>
        /// 响应参数
        /// </summary>
        [DataMember]
        public string ResponseParams { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [DataMember]
        public string RequestParams { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string OperName { get; set; }

        /// <summary>
        /// s上传数量
        /// </summary>
        [DataMember]
        public int UploadCount { get; set; }

        /// <summary>
        ///留钱
        /// </summary>
        [DataMember]
        public decimal CommisionMoney { get; set; }

        /// <summary>
        ///留点
        /// </summary>
        [DataMember]
        public decimal CommisionPoint { get; set; }

        /// <summary>
        ///上传政策类型
        /// </summary>
        [DataMember]
        public PoliciesType PolicyType { get; set; }

        /// <summary>
        ///要上传的政策Id列表,key为自己库中id value 为第三方政策id
        /// </summary>
        [DataMember]
        public Dictionary<string,string> UploadPolicyIds { get; set; }
    }
}
