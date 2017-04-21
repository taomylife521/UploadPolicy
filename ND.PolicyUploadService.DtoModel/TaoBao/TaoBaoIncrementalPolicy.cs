using ND.PolicyUploadService.DtoModel.dtoEntity;
using ND.PolicyUploadService.DtoModel.dtoEntity.TaoBao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.TaoBao
{
   public class TaoBaoIncrementalPolicy
    {
        /// <summary>
        /// 成功失败标志，true成功 false 失败
        /// </summary>
       public bool success { get { return true; } }

        /// <summary>
        /// 错误码
        /// </summary>
       public string errorCode { get { return ""; } }

        /// <summary>
        /// 错误信息
        /// </summary>
       public string errorMessage { get { return ""; } }

        /// <summary>
        /// 更新政策列表
        /// </summary>
        public List<policyDO> policyDOs { get; set; }

       /// <summary>
       /// 删除政策id列表，被删除的政策id用逗号分隔，最大支持1000个ID，如果需要大批量删除请用deletePolicy节点
       /// </summary>
        public string deleteIds { get; set; }

        /// <summary>
        /// 子账号
        /// </summary>
        public string agentSubName { get; set; }

        public List<TaoBaoDeletePolicy> deletePolicys { get; set; }

        /// <summary>
        /// 本次政策更新的时间 格式：格式： ”yyyy -MM -dd HH:mm:ssdd ”，用于全量 更新后的第一次增更新 ，与 lastOuterId二选一
        /// </summary>
        public string lastModifiedTime { get; set; }

        /// <summary>
        /// 政策更新的最后政策id，用于全量 更新后的第一次增更新 ，与 lastOuterId二选一
        /// </summary>
        public string lastOuterId { get; set; }
        
       /// <summary>
       /// 本次更新是否还有更多政策
       /// </summary>
        public bool isAnyMore { get; set; }

      

        
    }
}
