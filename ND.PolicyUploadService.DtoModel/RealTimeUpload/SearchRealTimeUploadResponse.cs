using ND.PolicyReceiveService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.RealTimeUpload
{
    [DataContract]
   public class SearchRealTimeUploadResponse:ResponseBase
    {
       [DataMember]
       public PolicyRecord PolicyRec { get; set; }
    }
}
