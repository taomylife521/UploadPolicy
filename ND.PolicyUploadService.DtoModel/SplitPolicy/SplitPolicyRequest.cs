using ND.PolicyReceiveService.DbEntity;
using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
  public class SplitPolicyRequest
    {
      public SplitPolicyRequest()
       {
           Policies = new List<Policies>();
       }
      /// <summary>
      /// 拆分的政策类型
      /// </summary>
      public PoliciesType PolicyType { get; set; }
       /// <summary>
       /// 采购商类型
       /// </summary>
       public PurchaserType Purchaser { get; set; }

       /// <summary>
       /// 分离的政策数据
       /// </summary>

      public List<Policies> Policies { get; set; }

      /// <summary>
      /// 最大开启的任务数量
      /// </summary>
      public int SplitMaxTaskCount { get; set; }

      /// <summary>
      /// 每个任务处理最大的数量
      /// </summary>
      public int SplitPerTaskMaxCount { get; set; }

      /// <summary>
      /// 存储最大开启的任务数量
      /// </summary>
      public int StorageMaxTaskCount { get; set; }

      /// <summary>
      /// 存储每个任务处理最大的数量
      /// </summary>
      public int StoragePerTaskMaxCount { get; set; }
    }
}
