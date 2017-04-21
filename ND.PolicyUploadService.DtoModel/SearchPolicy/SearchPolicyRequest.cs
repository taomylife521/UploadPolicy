using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
   public class SearchPolicyRequest
    {
       // private bool isRealTimeUpload = false;
        //private PolicyRecord realTimePolicyRec = new PolicyRecord { LastPolicyId = 0, LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) };
       /// <summary>
       /// 上传数量
       /// </summary>
       [DataMember]
       public int PageSize { get; set; }

       /// <summary>
       /// 上传筛选条件
       /// </summary>
       [DataMember]
       public string SqlWhere { get; set; }

       /// <summary>
       /// 上传类型
       /// </summary>
       [DataMember]
       public UploadType UType { get; set; }

       /// <summary>
       /// 平台类型
       /// </summary>
       [DataMember]
       public PurchaserType pType { get; set; }

       /// <summary>
       /// 政策类型
       /// </summary>
       [DataMember]
       public QunarPolicyType PolicyType { get; set; }

       /// <summary>
       /// 贴钱,在原有基础上进行加减
       /// </summary>
       [DataMember]
       public decimal CommisionMoney { get; set; }

       /// <summary>
       /// 贴点,在原有基础上进行加减
       /// </summary>
       [DataMember]
       public decimal CommsionPoint { get; set; }

       /// <summary>
       /// 操作人名称
       /// </summary>
       [DataMember]
       public string OperName { get; set; }

       /// <summary>
       /// 是否上传
       /// </summary>
       [DataMember]
       public bool IsUpload { get; set; }

       ///// <summary>
       ///// 是否实时上传
       ///// </summary>
       //[DataMember]
       //public bool IsRealTimeUpload { get { return isRealTimeUpload; } set { isRealTimeUpload = value == null ? isRealTimeUpload : value; } }

       

       /// <summary>
       /// 是否查询总数
       /// </summary>
       [DataMember]
       public bool IsSearchTotalCount { get; set; }
    }
}
