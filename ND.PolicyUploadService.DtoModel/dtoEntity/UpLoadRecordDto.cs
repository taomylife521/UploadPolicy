using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.dtoEntity
{
   public class UpLoadRecordDto
    {
        #region Model
        private long _id;
        private string _purchasertype = "";
        private string _uploadtype = "";
        private string _lastupdatetime = "";
        private string _lastpolicyid = "";
        private string _uploadfilepath = "";
        private string _responseparams = "";
        private int _notifyresult = 0;
        private DateTime _createtime = DateTime.Now;
        private string _requestparams = "";
        private int _isenabled = 1;
        private string _remark = "";
        private string _opername = "";
        private DateTime _completetime = Convert.ToDateTime("2099-12-30");
        private int _uploadcount = 0;
        private string _beforelastupdatetime = "";
        private string _beforelastpolicyid = "";
        private int _policytype = 1;
        public int Policytype
        {
            set { _policytype = value; }
            get { return _policytype; }
        }

        public string Beforelastpolicyid
        {
            set { _beforelastpolicyid = value; }
            get { return _beforelastpolicyid; }
        }

        public string Beforelastupdatetime
        {
            set { _beforelastupdatetime = value; }
            get { return _beforelastupdatetime; }
        }

        public int Uploadcount
        {
            set { _uploadcount = value; }
            get { return _uploadcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 平台：去哪儿、淘宝
        /// </summary>
        public string PurchaserType
        {
            set { _purchasertype = value; }
            get { return _purchasertype; }
        }
        /// <summary>
        /// 上传类型:全量上传，增量上传
        /// </summary>
        public string UploadType
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public string LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 最后一次更新政策的id
        /// </summary>
        public string LastPolicyId
        {
            set { _lastpolicyid = value; }
            get { return _lastpolicyid; }
        }
        /// <summary>
        /// 上传的文件路径
        /// </summary>
        public string UploadFilePath
        {
            set { _uploadfilepath = value; }
            get { return _uploadfilepath; }
        }
        /// <summary>
        /// 平台回调结果
        /// </summary>
        public string ResponseParams
        {
            set { _responseparams = value; }
            get { return _responseparams; }
        }
        /// <summary>
        /// 平台回调结果 1-成功 0-不成功
        /// </summary>
        public int NotifyResult
        {
            set { _notifyresult = value; }
            get { return _notifyresult; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 请求参数 
        /// </summary>
        public string RequestParams
        {
            set { _requestparams = value; }
            get { return _requestparams; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 上传人员姓名
        /// </summary>
        public string OperName
        {
            set { _opername = value; }
            get { return _opername; }
        }
        /// <summary>
        /// 上传完成时间
        /// </summary>
        public DateTime CompleteTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        #endregion Model
    }
}
