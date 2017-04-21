using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.RealTimeUpload
{
   public class SearchRealTimeUploadRequest
    {
       public PurchaserType Purchaser { get; set; }
       public string LockPerson { get; set; }
    }
}
