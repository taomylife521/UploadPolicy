using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.CompleteUploadPolicy
{
     [DataContract]
  public class CompleteUploadPolicyResponse:ResponseBase
    {
         public CompleteUploadPolicyResponse()
         {
             CompleteUploadPolicyCollection = new List<CompleteUploadPolicyDto>();
         }
        /// <summary>
        /// 已经上传政策的id
        /// </summary>
      [DataMember]
      public List<CompleteUploadPolicyDto> CompleteUploadPolicyCollection{ get; set; }
    }
}
