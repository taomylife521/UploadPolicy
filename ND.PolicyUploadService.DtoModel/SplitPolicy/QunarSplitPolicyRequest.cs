using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.SplitPolicy
{
   public class QunarSplitPolicyRequest:SplitPolicyRequest
    {

        /// <summary>
        /// 最大舱位个数
        /// </summary>
        public int MaxSeatCount { get; set; }

        /// <summary>
        /// 最大出发机场个数
        /// </summary>
        public int MaxDptCityCount { get; set; }

        /// <summary>
        /// 最大出发机场个数
        /// </summary>
        public int MaxArrCityCount { get; set; }

        /// <summary>
        /// 最大包含航班个数
        /// </summary>
        public int MaxFlightInCount { get; set; }

        /// <summary>
        /// 去哪儿的三字码
        /// </summary>
        public List<string> LstQunarCodes { get; set; }
    }
}
