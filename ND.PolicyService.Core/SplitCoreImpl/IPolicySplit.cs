using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    /// <summary>
    /// 政策拆分
    /// </summary>
    public interface IPolicySplit
    {
        event EventHandler<EventMsg> OnWoking;

        /// <summary>
        /// 开始拆分政策
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SplitPolicyResponse PolicySplit(SplitPolicyRequest request);
    }
}
