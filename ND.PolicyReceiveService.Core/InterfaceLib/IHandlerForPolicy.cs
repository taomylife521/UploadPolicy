using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.InterfaceLib
{
    /// <summary>
    /// 处理政策接口
    /// </summary>
   public interface IHandlerForPolicy
    {
        event EventHandler<EventMessage> onWorklingMsg;
        /// <summary>
        /// 接收政策对象
        /// </summary>
        IRecPolicy Recp
        {
            get;
            set;
        }
        GlobalConfig config { get; set; }
       void StartHanlerWork();

       void StopHanlderWork();
    }
}
