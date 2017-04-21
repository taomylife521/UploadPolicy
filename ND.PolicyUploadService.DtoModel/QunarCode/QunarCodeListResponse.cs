using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.QunarCode
{
    [DataContract]
   public class QunarCodeListResponse:ResponseBase
    {
       public QunarCodeListResponse()
       {
           Codes = new List<string>();
       }
        [DataMember]
       public List<string> Codes { get; set; }
    }
}
