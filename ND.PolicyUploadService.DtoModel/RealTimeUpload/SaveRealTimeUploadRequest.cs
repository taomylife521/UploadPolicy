using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.RealTimeUpload
{
  public  class SaveRealTimeUploadRequest
    {
      public PurchaserType Purchaser { get; set; }
      public PolicyRecord Record { get; set; }
      public string LockPerson { get; set; }
      public string Remark { get; set; }

      public int Interval { get; set; }

    }
}
