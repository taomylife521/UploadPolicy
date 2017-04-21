using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// UploadPolicyRecord:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 上传政策id
		/// </summary>
		public string PolicyId
		{
			set{ _policyid=value;}
			get{return _policyid;}
		}
		/// <summary>
		/// 留钱
		/// </summary>
		public decimal CommisionMoney
		{
			set{ _commisionmoney=value;}
			get{return _commisionmoney;}
		}
		/// <summary>
		/// 留点
		/// </summary>
		public decimal CommsionPoint
		{
			set{ _commsionpoint=value;}
			get{return _commsionpoint;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 最后一次更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 实时更新成功数量
		/// </summary>
		public int UpdateSuccessCount
		{
			set{ _updatesuccesscount=value;}
			get{return _updatesuccesscount;}
		}
		/// <summary>
		/// 实时更新失败数量
		/// </summary>
		public int UpdateFailedCount
		{
			set{ _updatefailedcount=value;}
			get{return _updatefailedcount;}
		}
		/// <summary>
		/// 第三方的政策id
		/// </summary>
		public string PartenerPolicyId
		{
			set{ _partenerpolicyid=value;}
			get{return _partenerpolicyid;}
		}
		/// <summary>
		/// 政策类型
		/// </summary>
		public string PolicyType
		{
			set{ _policytype=value;}
			get{return _policytype;}
		}
		#endregion Model

	}
}

