using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// QunarCode:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class QunarCode
	{
		public QunarCode()
		{}
		#region Model
		private long _id;
		private string _code="";
		/// <summary>
		/// 
		/// </summary>
		public long Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ȥ�Ķ�������
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		#endregion Model

	}
}

