using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.dtoEntity
{
   public class policyDO
    {
       /// <summary>
       /// 外部id，用来标识B2B的一条政策，如果此ID重复淘宝采用覆盖机制
       /// </summary>
       public string outerId { get; set; }

       /// <summary>
       /// 航空公司代码,CA,CZ,MU等航空公司
       /// </summary>
       public string airline { get; set; }

       /// <summary>
       /// 出发机场，PEK,SHA,CSX等机场三字码，全国为999，以，号分割，最多支持250个三字码，注意：私有库存时唯一，不可传多个
       /// </summary>
       public string depAirport { get; set; }

       /// <summary>
       /// 出发机场，PEK,SHA,CSX等机场三字码，全国为999，以，号分割，最多支持250个三字码，注意：私有库存时唯一，不可传多个
       /// </summary>
       public string arrAirport { get; set; }

       /// <summary>
       /// 航程类型 0-单程 1-往返
       /// </summary>
       public string tripType { get; set; }

       /// <summary>
       /// 销售有效期开始日期 指从此日起，政策才可销售下单。格式 “yyyy-MM-dd"
       /// </summary>
       public string saleStartDate { get; set; }

       /// <summary>
       /// 销售有效期结束日期 格式 “yyyy-MM-dd"
       /// </summary>
       public string saleEndDate { get; set; }

       /// <summary>
       /// 包含此政策的所有价格信息
       /// </summary>
       public List<PolicyPriceDO> prices { get; set; }

       /// <summary>
       /// 批发商officeId （即：office号），创建PNR时，会给此office做授权，只支持一个office号
       /// </summary>
       public string supplierOfficeId { get; set; }

       /// <summary>
       /// 代理商Id，卖家Id，此字段代理商不需要填写
       /// </summary>
       public string supplierId { get; set; }

       /// <summary>
       /// 是否更换PNR,true是 false否，主要针对平台的参数
       /// </summary>
       public bool needSwitchPNR { get; set; }

       /// <summary>
       /// 出票工作时间,最多支持7天（每天最多支持5个时间段）
       /// </summary>
       public string workTime { get; set; }

       /// <summary>
       /// 退票工作时间，最多支持7天（每天最多支持5个时间段）
       /// </summary>
       public string refundTime { get; set; }

       /// <summary>
       /// 出发不适用,当出发机场为999全国时使用；PEK，SHA，CSX等机场三字码表，以，号分割，最多支持250个三字码
       /// </summary>
       public string excludeDepAirports { get; set; }


       /// <summary>
       /// 到达不适用,当出发机场为999全国时使用；PEK，SHA，CSX等机场三字码表，以，号分割，最多支持250个三字码
       /// </summary>
       public string excludeArrAirports { get; set; }

       /// <summary>
       /// 行程单发票类型 1-等额行程单 2-不提供发票 5-等额行程单加发票 6-等额发票
       /// </summary>
       public int invoiceType { get; set; }

       /// <summary>
       /// Ei内容 支持最长32个字符
       /// </summary>
       public string contentEI { get; set; }

       /// <summary>
       /// 最早提前出票天数，没有限制-1
       /// </summary>
       public int earliestIssueDays { get; set; }

       /// <summary>
       /// 最晚提前出票天数 没有限制-1
       /// </summary>
       public int lastestIssueDays { get; set; }

     /// <summary>
       ///最小停留天数 没有限制-1 
       /// </summary>
       public int  minStayTime{ get; set; }

     /// <summary>
       /// 最大停留天数 没有限制 -1
       /// </summary>
       public int maxStayTime { get; set; }

     /// <summary>
       /// 是否支持共享航班 true是 false否；
       /// </summary>
       public bool  isSupportCodeShare{ get; set; }

     /// <summary>
       /// 是否可退 true是 false否；如果不填， 淘宝根据航空公司，舱位匹配FD退改签
       /// </summary>
       public bool isRefund { get; set; }

     /// <summary>
       /// 是否可改true是 false否；如果不填， 淘宝根据航空公司，舱位匹配FD退改签
       /// </summary>
       public bool isChange  { get; set; }

     /// <summary>
       /// 是否可签true是 false否；如果不填， 淘宝根据航空公司，舱位匹配FD退改签
       /// </summary>
       public bool isIssue { get; set; }

     /// <summary>
       ///是否可升舱 true是 false否；如果不填， 淘宝根据航空公司，舱位匹配FD退改签
       /// </summary>
       public bool isUpgrade  { get; set; }

     /// <summary>
       /// 退票计算方法 1固定金额 2 百分比；如果不填，淘宝根据航空公司，舱位匹配“全量退改签”数据
       /// </summary>
       public int refundType { get; set; }

     /// <summary>
       /// 退票计算基准 1：Y/C/F舱价格（根据当前舱位等级取对应的Y/C/F价格） 2.单程票面价格 3 往返票面价一半 4：原仓位FD票面价 5：往返票面价
       /// </summary>
       public int refundBaseType { get; set; }

       /// <summary>
       /// 退票计算时间点及对应取值，最多支持500个字符
       /// </summary>
       public string  refundPriceInfo{ get; set; }

       /// <summary>
       /// 改票计算方式 1固定金额 2 百分比；如果不填，淘宝根据航空公司，舱位匹配“全量退改签”数据
       /// </summary>
       public int  changeType{ get; set; }

       /// <summary>
       /// 改票计算基准 1：Y/C/F舱价格（根据当前舱位等级取对应的Y/C/F价格） 2.单程票面价格 4：原仓位FD票面价
       /// </summary>
       public int  changeBaseType{ get; set; }

       /// <summary>
       /// 改票计算时间点及对应取值 不填，淘宝根据旅行有效期，航空公司，舱位匹配FD退改签
       /// </summary>
       public string  changePriceInfo{ get; set; }

       /// <summary>
       /// 退改签，如果是私有运价政策（带有价格），必须传退改签。如果公布运价（不带有价格）不填，淘宝根据旅行有效期，航空公司，舱位匹配FD退改签
       /// </summary>
       public string tuigaiqianInfo  { get; set; }


       /// <summary>
       /// 航段限制列表
       /// </summary>
       public List<PolicySegmentDO>  segments{ get; set; }

     /// <summary>
       /// 政策更新时间戳，主要用于政策删除，可以重复，全量是系统判断逻辑；timestamp<=delTime的政策都将被删除
       /// </summary>
       public long timestamp  { get; set; }
       /// <summary>
       /// 备注，最多500个字符
       /// </summary>
       public string  memo  { get; set; }

       /// <summary>
       /// 是否是私有库存 true是 false否；如果不填则为否
       /// </summary>
       public bool isPrivate  { get; set; }

       /// <summary>
       /// 仅支持AV中有的航班 true是 false否，如果不填，则为否；仅针对私有库存
       /// </summary>
       public bool onlyAVFlight  { get; set; }

       /// <summary>
       /// 最少成行人数 数字，为空不限制
       /// </summary>
       public int minimumTraveller { get; set; }


      
    }
}
