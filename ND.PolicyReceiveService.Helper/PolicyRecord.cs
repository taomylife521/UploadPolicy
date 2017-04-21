using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Helper
{
    /// <summary>
    /// 政策记录
    /// </summary>
    [DataContract]
   public class PolicyRecord
    {
       /// <summary>
       /// 上次更新时间
       /// </summary>
       [DataMember]
       public DateTime LastUpdateTime { get; set; }

       /// <summary>
       /// 最后一次更新政策id
       /// </summary>
         [DataMember]
       public long LastPolicyId { get; set; }
    }
}
