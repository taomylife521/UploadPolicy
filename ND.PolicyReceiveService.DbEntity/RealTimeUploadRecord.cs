using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// RealTimeUploadRecord:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class RealTimeUploadRecord
	{
		public RealTimeUploadRecord()
		{}
		#region Model
		private Guid _id;
		private string _lastupdatetime="";
		private int _islock=0;
		private string _lockperson="";
		private string _remark="";
		private DateTime _createtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public Guid Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �ϴθ���ʱ��
		/// </summary>
		public string LastUpdateTime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 0-û������ 1-����
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string LockPerson
		{
			set{ _lockperson=value;}
			get{return _lockperson;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

