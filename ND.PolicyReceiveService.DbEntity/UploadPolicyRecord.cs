using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// UploadPolicyRecord:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class UploadPolicyRecord
	{
		public UploadPolicyRecord()
		{}
    #region Model
		private Guid _id;
		private long _uid=0;
		private string _policyid="";
		private decimal _commisionmoney=0M;
		private decimal _commsionpoint=0M;
		private DateTime _createtime= DateTime.Now;
		private DateTime _updatetime= Convert.ToDateTime("2099-12-30");
		private int _updatesuccesscount=0;
		private int _updatefailedcount=0;
		private string _partenerpolicyid="";
		private string _policytype="";
		/// <summary>
		/// 
		/// </summary>
		public Guid Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long UId
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// �ϴ�����id
		/// </summary>
		public string PolicyId
		{
			set{ _policyid=value;}
			get{return _policyid;}
		}
		/// <summary>
		/// ��Ǯ
		/// </summary>
		public decimal CommisionMoney
		{
			set{ _commisionmoney=value;}
			get{return _commisionmoney;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal CommsionPoint
		{
			set{ _commsionpoint=value;}
			get{return _commsionpoint;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// ���һ�θ���ʱ��
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// ʵʱ���³ɹ�����
		/// </summary>
		public int UpdateSuccessCount
		{
			set{ _updatesuccesscount=value;}
			get{return _updatesuccesscount;}
		}
		/// <summary>
		/// ʵʱ����ʧ������
		/// </summary>
		public int UpdateFailedCount
		{
			set{ _updatefailedcount=value;}
			get{return _updatefailedcount;}
		}
		/// <summary>
		/// ������������id
		/// </summary>
		public string PartenerPolicyId
		{
			set{ _partenerpolicyid=value;}
			get{return _partenerpolicyid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string PolicyType
		{
			set{ _policytype=value;}
			get{return _policytype;}
		}
		#endregion Model

	}
}

