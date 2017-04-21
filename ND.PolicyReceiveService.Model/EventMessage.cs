using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Model
{
   public class EventMessage:EventArgs
    {
       /// <summary>
       /// 消息
       /// </summary>
       public string Msg { get; set; }
    }
}
