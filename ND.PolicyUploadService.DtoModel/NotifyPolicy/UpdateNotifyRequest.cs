using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
   public class UpdateNotifyRequest
    {
       public bool IsSucess { get; set; }
       /// <summary>
       /// 保存更新状态记录id
       /// </summary>
       public string UpdateStatusId { get; set; }

       /// <summary>
       /// 响应参数
       /// </summary>
       public string ResponseParams { get; set; }

       /// <summary>
       /// 响应参数
       /// </summary>
       public SuccessStatus NotifyResult { get; set; }

    }
}
