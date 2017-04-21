using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// UpLoadRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UpLoadRecord
	{
		public UpLoadRecord()
		{}
        #region Model
        private long _id;
        private string _purchasertype = "";
        private string _uploadtype = "";
        private string _lastupdatetime = "";
        private string _lastpolicyid = "0";
        private string _uploadfilepath = "";
        private string _responseparams = "";
        private int _notifyresult = 1;
        private DateTime _createtime = DateTime.Now;
        private string _requestparams = "";
        private int _isenabled = 1;
        private string _remark = "";
        private string _opername = "";
        private DateTime _completetime = DateTime.Now;
        private int _uploadcount = 0;
        private string _beforelastupdatetime = "";
        private string _beforelastpolicyid = "";
        private int _policytype = 1;
        private int _failedcount = 0;
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 平台 (去哪儿)
        /// </summary>
        public string PurchaserType
        {
            set { _purchasertype = value; }
            get { return _purchasertype; }
        }
        /// <summary>
        /// 上传类型 增量或者全量
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
        /// 最后一条政策编号
        /// </summary>
        public string LastPolicyId
        {
            set { _lastpolicyid = value; }
            get { return _lastpolicyid; }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        public string UploadFilePath
        {
            set { _uploadfilepath = value; }
            get { return _uploadfilepath; }
        }
        /// <summary>
        /// 响应参数
        /// </summary>
        public string ResponseParams
        {
            set { _responseparams = value; }
            get { return _responseparams; }
        }
        /// <summary>
        /// 通知结果 0-不成功 1-成功
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
        /// 是否有效
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
        /// 操作人员
        /// </summary>
        public string OperName
        {
            set { _opername = value; }
            get { return _opername; }
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        /// <summary>
        /// 上传数量
        /// </summary>
        public int UploadCount
        {
            set { _uploadcount = value; }
            get { return _uploadcount; }
        }
        /// <summary>
        /// 保存前一次更新时间
        /// </summary>
        public string BeforeLastUpdateTime
        {
            set { _beforelastupdatetime = value; }
            get { return _beforelastupdatetime; }
        }
        /// <summary>
        /// 保存前一次更新政策ID
        /// </summary>
        public string BeforeLastPolicyId
        {
            set { _beforelastpolicyid = value; }
            get { return _beforelastpolicyid; }
        }
        /// <summary>
        /// 1-普通政策
        /// </summary>
        public int PolicyType
        {
            set { _policytype = value; }
            get { return _policytype; }
        }
        /// <summary>
        /// 失败数量
        /// </summary>
        public int FailedCount
        {
            set { _failedcount = value; }
            get { return _failedcount; }
        }
        #endregion Model
	}
}

