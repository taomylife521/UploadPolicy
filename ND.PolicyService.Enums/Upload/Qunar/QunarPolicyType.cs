using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Enums.Upload.Qunar
{
    /// <summary>
    /// 去哪儿政策类型
    /// </summary>
   public enum QunarPolicyType
    {
           /// <summary>
           /// 表示单程普通政策
           /// </summary>
           COMMON,
           /// <summary>
           /// 表示单程预付政策
           /// </summary>
          PREPAY,

           /// <summary>
           /// 表示申请政策
           /// </summary>
          APPLY,

           /// <summary>
           /// 表示包机切位政策
           /// </summary>
          CUSTOMER,

           /// <summary>
          /// 表示特价政策
           /// </summary>
         LOWPRICE,

        /// <summary>
        /// 往返预付政策
        /// </summary>
        ROUNDPREPAY,

           /// <summary>
           /// 往返特价政策
           /// </summary>
        ROUNDSPECIAL,

           /// <summary>
           /// 单程所有类型政策
           /// </summary>
        SINGLEALL,
 
           /// <summary>
        /// 往返所有类型政策
           /// </summary>
        ROUNDALL 
    }
}
