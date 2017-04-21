using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.OutPutAllPolicyZip.Model;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.Qunar
{
    public class QunarUploadPolicyRequest : UpLoadPolicyRequest
    {
       // private bool isRealTimeUpload = false;
       // private PolicyRecord realTimePolicyRecord = new PolicyRecord();
        public QunarUploadPolicyRequest()
        {
            PolicyData = new Dictionary<UploadTypeDetail, List<Policies>>();
            DefaultUploadConfig = new QunarUploadConfigResponse(); 
        }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string QunarUpLoadUrl { get; set; }

        /// <summary>
        /// 去哪儿政策类型
        /// </summary>
        public QunarPolicyType PolicyType { get; set; }

        /// <summary>
        /// 政策类型对应的数据
        /// </summary>
        public  List<Policies> PolicyDataOrgin { get; set; }

        /// <summary>
        /// 政策类型对应的数据
        /// </summary>
        public Dictionary<UploadTypeDetail, List<Policies>> PolicyData { get; set; }

        /// <summary>
        /// 上传操作人
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// 上传数量
        /// </summary>
        public int UploadCount { get; set; }

        /// <summary>
        /// 默认去哪儿上传政策配置
        /// </summary>
        public QunarUploadConfigResponse DefaultUploadConfig { get; set; }

        /// <summary>
        /// 上传数量
        /// </summary>
        public List<string> LstQunarCodes { get; set; }

       
    }
}
