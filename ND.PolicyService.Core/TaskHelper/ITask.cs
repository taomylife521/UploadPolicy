using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.TaskHelper
{
   public interface ITask
    {
        event EventHandler<EventMsg> OnWorking;

       /// <summary>
       /// 开始任务
       /// </summary>
        EmptyResponse StartWork(object request);

        /// <summary>
        /// 开始任务
        /// </summary>
        bool CancelWork();
    }
}
