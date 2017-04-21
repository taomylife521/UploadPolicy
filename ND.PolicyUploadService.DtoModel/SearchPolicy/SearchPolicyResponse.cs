using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
    public class SearchPolicyResponse 
    {
        [DataMember]
        public PolicyRecord LastPolicyRecord { get; set; }

        [DataMember]
       public int TotalCount { get; set; }

        [DataMember]
       public List<Policies> lstPolicies { get; set; }
    }
}
