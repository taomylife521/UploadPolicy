using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Enums.Upload
{
    /// <summary>
    /// 同步类型
    /// </summary>
    public enum UploadType
    {

        /// <summary>
        /// 全量上传
        /// </summary>
        FullUpload,

        /// <summary>
        /// 增量上传
        /// </summary>
        Incremental

    }

    public enum UploadTypeDetail
    {
        
        /// <summary>
        /// 全量上传
        /// </summary>
        FullUpload,

        /// <summary>
        /// 增量添加上传
        /// </summary>
        IncrementalAdd,

          /// <summary>
        /// 增量删除上传
        /// </summary>
        IncrementalDelete
    }
}
