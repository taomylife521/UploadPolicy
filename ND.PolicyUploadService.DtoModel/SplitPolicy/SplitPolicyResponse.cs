using ND.PolicyReceiveService.DbEntity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    /// <summary>
    /// 拆分政策响应
    /// </summary>
    public class SplitPolicyResponse : ResponseBase
    {
        /// <summary>
        /// 拆分好的政策数据
        /// </summary>
        public List<Policies> PoliciesData { get; set; }
    }
}
