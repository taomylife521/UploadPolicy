using ND.PolicyReceiveService.Core.HandlerPolicy;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Factory.HandlerFac
{
   public class HandlerFor19eFactory:HandlerForPolicyFactory
    {
       public override InterfaceLib.IHandlerForPolicy Create(GlobalConfig config)
        {
            return new HandlerFor19e(config);
        }
    }
}
