
using ND.PolicyUploadService.DtoModel.dtoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.OutPutAllPolicyZip.Model
{
    public class TaoBaoFullPolicy
    {
       /// <summary>
       /// 成功失败标志，true成功 false 失败
       /// </summary>
       public bool success { get; set; }

       /// <summary>
       /// 错误码
       /// </summary>
       public string errorCode { get; set; }

       /// <summary>
       /// 错误信息
       /// </summary>
       public string errorMessage { get; set; }

       /// <summary>
       /// 本次政策更新的时间 格式：格式： ”yyyy -MM -dd HH:mm:ssdd ”，用于全量 更新后的第一次增更新 ，与 lastOuterId二选一
       /// </summary>
       public string lastModifiedTime { get; set; }

       /// <summary>
       /// 政策更新的最后政策id，用于全量 更新后的第一次增更新 ，与 lastOuterId二选一
       /// </summary>
       public string lastOuterId { get; set; }
       
       /// <summary>
       /// 政策清空时间
       /// </summary>
       public string delTime { get; set; }

       /// <summary>
       /// 政策清空ID
       /// </summary>
       public string delOuterId { get; set; }
       
       /// <summary>
       /// 子账号
       /// </summary>
       public string agentSubName { get; set; }

       /// <summary>
       /// 政策列表
       /// </summary>
       public List<policyDO> policyDOs { get; set; } 
    }
}
