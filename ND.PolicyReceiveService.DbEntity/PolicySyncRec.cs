using System;
namespace ND.PolicyReceiveService.DbEntity
{
	/// <summary>
	/// PolicySyncRec:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PolicySyncRec
	{
		public PolicySyncRec()
		{}
		#region Model
		private long _id;
		private DateTime _updatetime= DateTime.Now;
		private int _partnerid=0;
		private string _partnername="";
		private string _partnerpolicyid="";
		/// <summary>
		/// 政策id
		/// </summary>
		public long Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 同步时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 供应商id
		/// </summary>
		public int PartnerId
		{
			set{ _partnerid=value;}
			get{return _partnerid;}
		}
		/// <summary>
		/// 合作伙伴名称
		/// </summary>
		public string PartnerName
		{
			set{ _partnername=value;}
			get{return _partnername;}
		}
		/// <summary>
		/// 合作伙伴id
		/// </summary>
		public string PartnerPolicyId
		{
			set{ _partnerpolicyid=value;}
			get{return _partnerpolicyid;}
		}
		#endregion Model

	}
}

