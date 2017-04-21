using System;
namespace ND.PolicyService.DbEntity
{
	/// <summary>
	/// SeatDiscount:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SeatDiscount
	{
		public SeatDiscount()
		{}
		#region Model
		private int _id;
		private string _airlinecode="";
		private string _seat="";
		private string _discount="";
		private int _isenabled=1;
		private DateTime _createtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AirlineCode
		{
			set{ _airlinecode=value;}
			get{return _airlinecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Seat
		{
			set{ _seat=value;}
			get{return _seat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsEnabled
		{
			set{ _isenabled=value;}
			get{return _isenabled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

