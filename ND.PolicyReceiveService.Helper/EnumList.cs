using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ND.PolicyReceiveService.Helper
{
    /// <summary>
    /// 行程类型
    /// </summary>
    public enum RouteType
    {
        /// <summary>
        /// 单程
        /// </summary>
        Single=1,

        /// <summary>
        /// 返程
        /// </summary>
        Round,

        /// <summary>
        /// 单程和返程
        /// </summary>
        SingleAndRound,

        /// <summary>
        /// 联程
        /// </summary>
        Connecting
    }

   

    ///// <summary>
    ///// 出票类型
    ///// </summary>
    //public enum TicketType
    //{
    //    B2B = 1, BSP
    //}
}
