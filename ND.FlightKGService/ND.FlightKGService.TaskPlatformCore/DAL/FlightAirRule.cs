using System;
namespace ND.FlightKGService.TaskPlatformCore.DAL
{
	/// <summary>
	/// ���չ�˾�����
	/// </summary>
	[Serializable]
	public partial class FlightAirRule
	{
		public FlightAirRule()
		{}
		#region Model
		private string _id="";
		private string _airline="";
		private string _seatclass="";
		private string _returnn="";
        private string _change = "";
		private string _endorsement="";
		private string _returnndis="";
		private bool _isdel= false;
		private int _createby=0;
		private DateTime _createtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// ���ڹ涨
        /// </summary>
        public string change
        {
            set { _change = value; }
            get { return _change; }
        }
		/// <summary>
		/// ���չ�˾����
		/// </summary>
		public string airline
		{
			set{ _airline=value;}
			get{return _airline;}
		}
		/// <summary>
		/// ��λ��
		/// </summary>
		public string seatclass
		{
			set{ _seatclass=value;}
			get{return _seatclass;}
		}
		/// <summary>
		/// ��Ʊ�涨
		/// </summary>
		public string returnn
		{
			set{ _returnn=value;}
			get{return _returnn;}
		}
		/// <summary>
		/// ǩת�涨
		/// </summary>
		public string endorsement
		{
			set{ _endorsement=value;}
			get{return _endorsement;}
		}
		/// <summary>
		/// ��Ʊ˵�� ����:10-2-50 ���2Сʱǰ��10�� �����Сʱ����50
		/// </summary>
		public string returnndis
		{
			set{ _returnndis=value;}
			get{return _returnndis;}
		}
		/// <summary>
		/// �Ƿ�ɾ��  0��δɾ���� 1��ɾ��
		/// </summary>
		public bool isdel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// ������Id
		/// </summary>
		public int createby
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

