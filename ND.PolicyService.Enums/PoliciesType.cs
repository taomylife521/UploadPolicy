using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Enums
{
    public enum PoliciesType
    {
        Other = 0,
        /// <summary>
        /// 表示单程普通政策
        /// </summary>
        COMMON=1,
        /// <summary>
        /// 表示单程预付政策
        /// </summary>
        PREPAY=2,

        /// <summary>
        /// 表示申请政策
        /// </summary>
        APPLY=3,

        /// <summary>
        /// 表示包机切位政策
        /// </summary>
        CUSTOMER=4,

        /// <summary>
        /// 表示特价政策
        /// </summary>
        LOWPRICE=5,

        /// <summary>
        /// 往返预付政策
        /// </summary>
        ROUNDPREPAY=6,

        /// <summary>
        /// 往返特价政策
        /// </summary>
        ROUNDSPECIAL=7,

        /// <summary>
        /// 单程所有类型政策
        /// </summary>
        SINGLEALL=8,

        /// <summary>
        /// 往返所有类型政策
        /// </summary>
        ROUNDALL=9 
    }
}
