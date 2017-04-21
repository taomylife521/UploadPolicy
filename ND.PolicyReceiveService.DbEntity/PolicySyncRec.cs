using System;
namespace ND.PolicyReceiveService.DbEntity
{
	/// <summary>
	/// PolicySyncRec:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ����id
		/// </summary>
		public long Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ͬ��ʱ��
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// ��Ӧ��id
		/// </summary>
		public int PartnerId
		{
			set{ _partnerid=value;}
			get{return _partnerid;}
		}
		/// <summary>
		/// �����������
		/// </summary>
		public string PartnerName
		{
			set{ _partnername=value;}
			get{return _partnername;}
		}
		/// <summary>
		/// �������id
		/// </summary>
		public string PartnerPolicyId
		{
			set{ _partnerpolicyid=value;}
			get{return _partnerpolicyid;}
		}
		#endregion Model

	}
}

