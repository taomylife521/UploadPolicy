using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.QunarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore
{
   public interface IQunarCode
    {
       EmptyResponse AddCode(QunarCodeRequest code);

       EmptyResponse DeleteCode(QunarCodeRequest code);

       QunarCodeListResponse GetList(QunarCodeRequest code);
    }
}
