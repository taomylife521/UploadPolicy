using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
   public class SearchNotifyRequest
    {
        private bool _isSearchSystem = false;
        /// <summary>
        /// 查询上传政策状态的id
        /// </summary>
        [DataMember]
        public bool IsSearchSystem { get { return _isSearchSystem; } set { _isSearchSystem=value;} }

        /// <summary>
        /// 查询上传政策状态的id
        /// </summary>
        [DataMember]
        public string UploadStatusId { get; set; }
    }
}
