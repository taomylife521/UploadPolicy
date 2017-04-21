using ND.PolicyService.Enums;
using ND.PolicyUploadService.DtoModel.dtoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
   public class SearchNotifyResponse:ResponseBase
    {
        public SearchNotifyResponse()
        {
            NotifyList = new List<UpLoadRecordDto>();
        }
        /// <summary>
        /// 上传通知列表
        /// </summary>
        [DataMember]
        public List<UpLoadRecordDto> NotifyList { get; set; }
       


    }
}
