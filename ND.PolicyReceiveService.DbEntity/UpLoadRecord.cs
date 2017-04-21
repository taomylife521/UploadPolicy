using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// UpLoadRecord:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ƽ̨ (ȥ�Ķ�)
        /// </summary>
        public string PurchaserType
        {
            set { _purchasertype = value; }
            get { return _purchasertype; }
        }
        /// <summary>
        /// �ϴ����� ��������ȫ��
        /// </summary>
        public string UploadType
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        /// <summary>
        /// ���һ�θ���ʱ��
        /// </summary>
        public string LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// ���һ�����߱��
        /// </summary>
        public string LastPolicyId
        {
            set { _lastpolicyid = value; }
            get { return _lastpolicyid; }
        }
        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        public string UploadFilePath
        {
            set { _uploadfilepath = value; }
            get { return _uploadfilepath; }
        }
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public string ResponseParams
        {
            set { _responseparams = value; }
            get { return _responseparams; }
        }
        /// <summary>
        /// ֪ͨ��� 0-���ɹ� 1-�ɹ�
        /// </summary>
        public int NotifyResult
        {
            set { _notifyresult = value; }
            get { return _notifyresult; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string RequestParams
        {
            set { _requestparams = value; }
            get { return _requestparams; }
        }
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public int IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public string OperName
        {
            set { _opername = value; }
            get { return _opername; }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime CompleteTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public int UploadCount
        {
            set { _uploadcount = value; }
            get { return _uploadcount; }
        }
        /// <summary>
        /// ����ǰһ�θ���ʱ��
        /// </summary>
        public string BeforeLastUpdateTime
        {
            set { _beforelastupdatetime = value; }
            get { return _beforelastupdatetime; }
        }
        /// <summary>
        /// ����ǰһ�θ�������ID
        /// </summary>
        public string BeforeLastPolicyId
        {
            set { _beforelastpolicyid = value; }
            get { return _beforelastpolicyid; }
        }
        /// <summary>
        /// 1-��ͨ����
        /// </summary>
        public int PolicyType
        {
            set { _policytype = value; }
            get { return _policytype; }
        }
        /// <summary>
        /// ʧ������
        /// </summary>
        public int FailedCount
        {
            set { _failedcount = value; }
            get { return _failedcount; }
        }
        #endregion Model
	}
}

