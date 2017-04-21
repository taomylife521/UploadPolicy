using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
   public class SaveNotifyResponse:ResponseBase
    {
        [DataMember]
        public string UploadStatusId { get; set; }
    }
}
