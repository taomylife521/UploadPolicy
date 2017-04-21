using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.dtoEntity.TaoBao
{
   public class TaoBaoDeletePolicy
    {
       /// <summary>
       /// 起飞机场（大写），出发机场三字码，大写且只支持单个出发地，政策上有多个出发到达，匹配到一个就删除
       /// </summary>
       public string depAirport { get; set; }

       /// <summary>
       /// 到达机场（大写），到达机场三字码，大写且只支持单个目的地，政策上有多个出发到达，匹配到一个就删除
       /// </summary>
       public string arrAirport { get; set; }

       /// <summary>
       /// 航司二字码（大写） 航司二字码大写，支持单个航司二字码
       /// </summary>
       public string airline { get; set; }

       /// <summary>
       /// 政策id删除，代理商内部政策代码支持模糊检索  ,模 糊检索部分用“ 用“ *”代替。例  :输入 “abcpolicy*”,匹配 “abcpolicy%”格式的所有政策 (只 支持 * 在最 后),且支持单个模糊 ,如果多个 模糊请传如果多个deletePolicy
       /// </summary>
       public string outIdPrefix { get; set; }

       /// <summary>
       /// 舱位（往返政策对应去程舱位） 舱位代码，大写且只支持单个舱位的录入，政策上有多个舱位，匹配到一个就删除
       /// </summary>
       public string cabin { get; set; }
    }
}
