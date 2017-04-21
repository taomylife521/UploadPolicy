using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// RealTimeUploadRecord:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 上次更新时间
		/// </summary>
		public string LastUpdateTime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 0-没有锁定 1-锁定
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 锁定人
		/// </summary>
		public string LockPerson
		{
			set{ _lockperson=value;}
			get{return _lockperson;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

