using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// QunarCode:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 去哪儿三字码
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		#endregion Model

	}
}

