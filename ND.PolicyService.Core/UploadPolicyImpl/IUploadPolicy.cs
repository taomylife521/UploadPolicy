using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.OutPutAllPolicyZip.Model;
using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.SplitCore
{
    /// <summary>
    /// 生成政策Zip文件
    /// </summary>
    public interface IUploadPolicy 
    {
        event EventHandler<EventMsg> OnWoking;

       /// <summary>
       ///上传全量政策
       /// </summary>
        UploadPolicyResponse UploadFullPolicy(UpLoadPolicyRequest request);

        /// <summary>
        /// 上传增量政策
        /// </summary>
        UploadPolicyResponse UpLoadIncrementPolicy(UpLoadPolicyRequest request,bool isTakePolicy=true);

       
    }
}
