using ND.PolicyReceiveService.Core.ReceivePolicy;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Factory.ReceiveFac
{
   public  class RecPolicy19eFactory:RecPolicyFactory
    {
       public override IRecPolicy Create(Config19e config)
        {
            return new RecPolicy19e(config);
        }
    }
}
