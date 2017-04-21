using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore
{

   public interface IPolicyNotify
   {

       SaveNotifyResponse SaveNotify(PolicyNotifyRequest request);


       EmptyResponse UpdateNotify(UpdateNotifyRequest request);
       

       SearchNotifyResponse SearchNotifyList(SearchNotifyRequest request);
    }
}
