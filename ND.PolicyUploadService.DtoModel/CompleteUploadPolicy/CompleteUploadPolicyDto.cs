using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.CompleteUploadPolicy
{
   public class CompleteUploadPolicyDto
    {
        /// <summary>
        /// 政策id
        /// </summary>
       public string PartenerPolicyId { get; set; }

       /// <summary>
       /// 政策id
       /// </summary>
       public string PolicyId { get; set; }

       /// <summary>
       /// 留钱
       /// </summary>
       public decimal CommisionMoney { get; set; }

       /// <summary>
       /// 留点
       /// </summary>
       public decimal CommsionPoint { get; set; }

       /// <summary>
       /// 留点
       /// </summary>
       public string PolicyType { get; set; }
    }
}
