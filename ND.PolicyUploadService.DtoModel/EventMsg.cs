using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    public class EventMsg:EventArgs
    {
        private RunStatus status = RunStatus.Normal;
        private string msg = "";
        private Exception exception = null;

        private PurchaserType purchaserType = new PurchaserType();
        /// <summary>
        /// 运行状态
        /// </summary>
        public RunStatus Status { get{return status;} set{status=value;} }

        /// <summary>
        /// 采购商
        /// </summary>
        public PurchaserType PurchaserType { get { return purchaserType; } set { purchaserType = value; } }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Msg { get{return msg;} set{msg=value;} }

        /// <summary>
        /// 异常信息
        /// </summary>

        public Exception Exception { get{return exception;} set{exception=value;} }
    }
}
