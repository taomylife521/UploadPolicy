using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.dtoEntity
{
   public class PolicyPriceDO
    {
       /// <summary>
       /// 价格（单位：元，保留2位小数） 普通库存，非必填，如果不传，淘宝根据旅行有效期，航空公司，舱位匹配FD价格，私有库存必填
       /// </summary>
       public string price { get; set; }

       /// <summary>
       /// 返点，只保留俩位小数
       /// </summary>
       public float retentionPoint { get; set; }


       /// <summary>
       /// 返现，可以是正负值，正值表示给客人返现，负值表示留钱，把一代加上金额，买家支付手续费，买家服务费，都合起来算在这个字段里
       /// </summary>
       public double retentionMoney { get; set; }


       /// <summary>
       /// 卖家id
       /// </summary>
       public string supportAgents { get; set; }


      
    }
}
