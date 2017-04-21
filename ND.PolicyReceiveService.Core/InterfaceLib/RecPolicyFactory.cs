using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.InterfaceLib
{
   public abstract class RecPolicyFactory
    {
       public abstract IRecPolicy Create(Config19e config);
    }
}
