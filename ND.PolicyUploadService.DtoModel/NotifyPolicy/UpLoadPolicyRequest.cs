using ND.PolicyService.Enums.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    /// <summary>
    /// 上传政策请求类
    /// </summary>
    public class UpLoadPolicyRequest 
    {
        public UploadType UploadType { get; set; }

        /// <summary>
        /// 要保存的格式化文件的路径
        /// </summary>
        public string FormatFilePath { get; set; }

        /// <summary>
        /// 要保存的格式化文件Zip的路径
        /// </summary>
        public string FormatZipFilePath { get; set; }
        /// <summary>
        /// 每次取最多数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 最大任务数量
        /// </summary>
        public int MaxTaskCount { get; set; }

        /// <summary>
        /// 每个任务最多处理数量
        /// </summary>
        public int PerTaskCount { get; set; }

        /// <summary>
        /// 是否打印sql
        /// </summary>
        public bool IsPrintSql { get; set; }

        /// <summary>
        ///请求条件
        /// </summary>
        public string SqlWhere { get; set; }

        /// <summary>
        /// 贴钱
        /// </summary>
        public decimal CommisionMoney { get; set; }

        /// <summary>
        /// 贴点
        /// </summary>
        public decimal CommsionPoint { get; set; }
               
              

       
    }
}
