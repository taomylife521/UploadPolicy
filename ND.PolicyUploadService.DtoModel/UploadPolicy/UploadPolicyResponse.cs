
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    /// <summary>
    /// 上传政策响应
    /// </summary>
    public class UploadPolicyResponse:ResponseBase
    {
        public UploadPolicyResponse()
        {
            PolicyRec = new Dictionary<UploadType, PolicyRecord>() { 
                {UploadType.FullUpload,new PolicyRecord()},
                {UploadType.Incremental,new PolicyRecord()},
            };
        }
        /// <summary>
        /// xml政策路径
        /// </summary>
        public string FormatPolicyFilePath { get; set; }

        /// <summary>
        /// 生成zip包的路径
        /// </summary>
        public string FormatPolicyZipFilePath { get; set; }

        /// <summary>
        /// 最后一条记录
        /// </summary>
        public Dictionary<UploadType, PolicyRecord> PolicyRec { get; set; }

        /// <summary>
        /// 最后一条记录
        /// </summary>
        public string UploadStatusId { get; set; }

        /// <summary>
        /// 上次的更新记录
        /// </summary>
        public PolicyRecord BeforePolicyRecord { get; set; }
        
    }
}
