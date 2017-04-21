using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.dtoEntity
{
    public class PolicySegmentDO
    {
       /// <summary>
       /// 
       /// </summary>
        public string segmentNum {get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string includeFlightNos { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string excludeFlightNos { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string operationTime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string travelStartDate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string travelEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string depTimeRanges { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cabinList { get; set; }

        /// <summary>
        /// 可售库存数 取值返回1-300仅对私有库存有意义
        /// </summary>
        public int seatNum { get; set; }
    }
}
