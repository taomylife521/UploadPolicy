using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.InterfaceLib
{
    /// <summary>
    /// 接收政策接口
    /// </summary>
   public interface IRecPolicy
    {
        event EventHandler<EventMessage> onWorklingMsg;
        Config19e config { get; set; }
       /// <summary>
       /// 全取
       /// </summary>
       void ReceiveAllPolicy();

       /// <summary>
       /// 同步政策
       /// </summary>
       void SyncPolicy();
    }
}
