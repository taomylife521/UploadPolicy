using System;
namespace ND.PolicyReceiveService.DbEntity
{
	/// <summary>
	/// PolicyDetail:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class PolicyDetail
	{
		public PolicyDetail()
		{}
		#region Model
        private string _partnerpolicyid = "";
		private long _id;
		private long _policyid=0;
		private int _srctype=0;
		private int _commisiontype=0;
		private string _comment="";
		private string _airlinecode="";
		private string _dptcity="";
		private string _arrcity="";
		private string _flightin="";
		private string _flightout="";
		private string _seat="";
		private DateTime _saleeffectdate= Convert.ToDateTime("2099-12-30");
		private DateTime _saleexpiredate= Convert.ToDateTime("2099-12-30");
		private DateTime _saleforbideffectdate= Convert.ToDateTime("2099-12-30");
		private DateTime _saleforbidexpiredate= Convert.ToDateTime("2099-12-30");
		private DateTime _flighteffectdate= Convert.ToDateTime("2099-12-30");
		private DateTime _flightexpiredate= Convert.ToDateTime("2099-12-30");
		private DateTime _flightforbideffectdate= Convert.ToDateTime("2099-12-30");
		private DateTime _flightforbidexpiredate= Convert.ToDateTime("2099-12-30");
		private int _earliestissuedays=0;
		private int _isfitchild=0;
		private decimal _commisionpoint=0M;
		private decimal _commisionmoney=0M;
        private decimal _platformcommisionpoint = 0M;
        private decimal _platformcommisionmoney = 0M;
		private int _issetprivate=0;
		private int _privatecount=0;
		private string _officeno="";
		private int _needswitchpnr=0;
		private int _isautoissue=0;
		private int _ispata=1;
		private string _bigclientcode="";
		private int _minimumtraveller=9;
		private int _isproviderscore=0;
		private int _issharingflight=0;
		private int _invoicetype=1;
		private string _tuigairule="";
		private string _changeworktime="";
		private string _returnworktime="";
		private string _vtworktime="";
		private string _changeworktimeweekend="";
		private string _returnworktimeweekend="";
		private string _vtworktimeweekend="";
		private string _issueworktime="";
		private string _issueworktimeweekend="";
		private string _ticketspeed="";
		private string _flightcycle="";
		private string _policytype="";
		private int _psgtype=0;
		private string _param1="";
		private string _param2="";
		private string _param3="";
		private string _param4="";
		private int _policystatus=1;
		private DateTime _createtime= DateTime.Now;
		private int _deldegree=1;

        /// <summary>
        /// �������id
        /// </summary>
        public string PartnerPolicyId
        {
            set { _partnerpolicyid = value; }
            get { return _partnerpolicyid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public long Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ����id
		/// </summary>
		public long PolicyId
		{
			set{ _policyid=value;}
			get{return _policyid;}
		}
		/// <summary>
		/// ��������[1:����/2:����/3:���̼�����/4:����/5:ȱ�ڳ�]
		/// </summary>
		public int SrcType
		{
			set{ _srctype=value;}
			get{return _srctype;}
		}
		/// <summary>
		/// ��Ʒ��� ��������[1:��ͨ/2:����/3:���(�ؼ۲�)/����Ϊ�մ���ȫ��]
		/// </summary>
		public int CommisionType
		{
			set{ _commisiontype=value;}
			get{return _commisiontype;}
		}
		/// <summary>
		/// ���߱�ע
		/// </summary>
		public string Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		/// <summary>
		/// ���չ�˾����
		/// </summary>
		public string AirlineCode
		{
			set{ _airlinecode=value;}
			get{return _airlinecode;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string DptCity
		{
			set{ _dptcity=value;}
			get{return _dptcity;}
		}
		/// <summary>
		/// �ִ����
		/// </summary>
		public string ArrCity
		{
			set{ _arrcity=value;}
			get{return _arrcity;}
		}
		/// <summary>
		/// ���ú���
		/// </summary>
		public string FlightIn
		{
			set{ _flightin=value;}
			get{return _flightin;}
		}
		/// <summary>
		/// �����ú���
		/// </summary>
		public string FlightOut
		{
			set{ _flightout=value;}
			get{return _flightout;}
		}
		/// <summary>
		/// ��λ
		/// </summary>
		public string Seat
		{
			set{ _seat=value;}
			get{return _seat;}
		}
		/// <summary>
		/// ��Ʊ��Ч�ڿ�ʼʱ��(���۳�Ʊ��ʼ����)��ʽ��2013-05-04
		/// </summary>
		public DateTime SaleEffectDate
		{
			set{ _saleeffectdate=value;}
			get{return _saleeffectdate;}
		}
		/// <summary>
		/// ��Ʊ����ʱ��(���۳�Ʊ��������)2015-02-03
		/// </summary>
		public DateTime SaleExpireDate
		{
			set{ _saleexpiredate=value;}
			get{return _saleexpiredate;}
		}
		/// <summary>
		/// ��ֹ��Ʊ��ʼʱ��(��ֹ���۳�Ʊ��ʼ����)
		/// </summary>
		public DateTime SaleForbidEffectDate
		{
			set{ _saleforbideffectdate=value;}
			get{return _saleforbideffectdate;}
		}
		/// <summary>
		/// ��ֹ��Ʊ����ʱ��(��ֹ���۳�Ʊ��������)
		/// </summary>
		public DateTime SaleForbidExpireDate
		{
			set{ _saleforbidexpiredate=value;}
			get{return _saleforbidexpiredate;}
		}
		/// <summary>
		/// ������Ч��ʼ����(������ʼ����)
		/// </summary>
		public DateTime FlightEffectDate
		{
			set{ _flighteffectdate=value;}
			get{return _flighteffectdate;}
		}
		/// <summary>
		/// �������ʱ��(���н�������)
		/// </summary>
		public DateTime FlightExpireDate
		{
			set{ _flightexpiredate=value;}
			get{return _flightexpiredate;}
		}
		/// <summary>
		/// ��ֹ������Ч��ʼ����(��ֹ������ʼ����)
		/// </summary>
		public DateTime FlightForbidEffectDate
		{
			set{ _flightforbideffectdate=value;}
			get{return _flightforbideffectdate;}
		}
		/// <summary>
		/// ��ֹ������Ч��������(��ֹ���н�������)
		/// </summary>
		public DateTime FlightForbidExpireDate
		{
			set{ _flightforbidexpiredate=value;}
			get{return _flightforbidexpiredate;}
		}
		/// <summary>
		/// ��ǰԤ������
		/// </summary>
		public int EarliestIssueDays
		{
			set{ _earliestissuedays=value;}
			get{return _earliestissuedays;}
		}
		/// <summary>
		/// �Ƿ��ʺ϶�ͯ 0-���ʺ� 1-�ʺ�
		/// </summary>
		public int IsFitChild
		{
			set{ _isfitchild=value;}
			get{return _isfitchild;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal CommisionPoint
		{
			set{ _commisionpoint=value;}
			get{return _commisionpoint;}
		}
		/// <summary>
		/// ����(��Ǯ)
		/// </summary>
		public decimal CommisionMoney
		{
			set{ _commisionmoney=value;}
			get{return _commisionmoney;}
		}

        /// <summary>
        /// ���Ⱪ©����
        /// </summary>
        public decimal PlatformCommisionPoint
        {
            set { _commisionpoint = value; }
            get { return _commisionpoint; }
        }
        /// <summary>
        /// ���Ⱪ©����(��Ǯ)
        /// </summary>
        public decimal PlatformCommisionMoney
        {
            set { _commisionmoney = value; }
            get { return _commisionmoney; }
        }
		/// <summary>
		/// �Ƿ����ÿ�� 0-������ 1-����
		/// </summary>
		public int isSetPrivate
		{
			set{ _issetprivate=value;}
			get{return _issetprivate;}
		}
		/// <summary>
		/// ��isSetPrivateΪ1ʱ��ֵ������д
		/// </summary>
		public int PrivateCount
		{
			set{ _privatecount=value;}
			get{return _privatecount;}
		}
		/// <summary>
		/// ��Ȩoffice��
		/// </summary>
		public string OfficeNo
		{
			set{ _officeno=value;}
			get{return _officeno;}
		}
		/// <summary>
		/// �Ƿ��軻pnr0-����Ҫ 1- ��Ҫ
		/// </summary>
		public int NeedSwitchPNR
		{
			set{ _needswitchpnr=value;}
			get{return _needswitchpnr;}
		}
		/// <summary>
		/// 0-�����Զ���Ʊ 1-�Զ���Ʊ
		/// </summary>
		public int IsAutoIssue
		{
			set{ _isautoissue=value;}
			get{return _isautoissue;}
		}
		/// <summary>
		/// �Ƿ���Ҫ��� 1-��Ҫ 0-����Ҫ
		/// </summary>
		public int IsPata
		{
			set{ _ispata=value;}
			get{return _ispata;}
		}
		/// <summary>
		/// ��ͻ�����
		/// </summary>
		public string BigClientCode
		{
			set{ _bigclientcode=value;}
			get{return _bigclientcode;}
		}
		/// <summary>
		/// ���ٳ�������
		/// </summary>
		public int MinimumTraveller
		{
			set{ _minimumtraveller=value;}
			get{return _minimumtraveller;}
		}
		/// <summary>
		/// 0-���ṩ���ÿͻ��� 1- �ṩ���ÿͻ���
		/// </summary>
		public int IsProviderScore
		{
			set{ _isproviderscore=value;}
			get{return _isproviderscore;}
		}
		/// <summary>
		/// 0-���ǹ����� 1-������
		/// </summary>
		public int IsSharingFlight
		{
			set{ _issharingflight=value;}
			get{return _issharingflight;}
		}
		/// <summary>
		/// 0-���ṩ�г̵� 1-�ṩ�г̵� 
		/// </summary>
		public int InvoiceType
		{
			set{ _invoicetype=value;}
			get{return _invoicetype;}
		}
		/// <summary>
		/// �˸Ĺ涨
		/// </summary>
		public string TuiGaiRule
		{
			set{ _tuigairule=value;}
			get{return _tuigairule;}
		}
		/// <summary>
		/// ���ڹ����չ���ʱ��
		/// </summary>
		public string ChangeWorkTime
		{
			set{ _changeworktime=value;}
			get{return _changeworktime;}
		}
		/// <summary>
		/// ��Ʊ�����չ���ʱ��
		/// </summary>
		public string ReturnWorkTime
		{
			set{ _returnworktime=value;}
			get{return _returnworktime;}
		}
		/// <summary>
		/// ��Ʊ�����չ���ʱ��
		/// </summary>
		public string VtWorkTime
		{
			set{ _vtworktime=value;}
			get{return _vtworktime;}
		}
		/// <summary>
		/// ������ĩ����ʱ��
		/// </summary>
		public string ChangeWorkTimeWeekend
		{
			set{ _changeworktimeweekend=value;}
			get{return _changeworktimeweekend;}
		}
		/// <summary>
		/// ��Ʊ��ĩ����ʱ��
		/// </summary>
		public string ReturnWorkTimeWeekend
		{
			set{ _returnworktimeweekend=value;}
			get{return _returnworktimeweekend;}
		}
		/// <summary>
		/// ��Ʊ��ĩ����ʱ��
		/// </summary>
		public string VtWorkTimeWeekend
		{
			set{ _vtworktimeweekend=value;}
			get{return _vtworktimeweekend;}
		}
		/// <summary>
		/// ����ʱ�䣨��Ʊʱ�䣩
		/// </summary>
		public string IssueWorkTime
		{
			set{ _issueworktime=value;}
			get{return _issueworktime;}
		}
		/// <summary>
		/// ��Ʊ��ĩ����ʱ��
		/// </summary>
		public string IssueWorkTimeWeekend
		{
			set{ _issueworktimeweekend=value;}
			get{return _issueworktimeweekend;}
		}
		/// <summary>
		/// ��ƱЧ��
		/// </summary>
		public string TicketSpeed
		{
			set{ _ticketspeed=value;}
			get{return _ticketspeed;}
		}
		/// <summary>
		/// �硰1,2,3,4����ʾ��һ���ܶ������������ĵĺ�����á���Ϊ�գ����ʾ�����á�
		/// </summary>
		public string FlightCycle
		{
			set{ _flightcycle=value;}
			get{return _flightcycle;}
		}
		/// <summary>
		/// �������ͣ�BSp or B2B��
		/// </summary>
		public string PolicyType
		{
			set{ _policytype=value;}
			get{return _policytype;}
		}
		/// <summary>
		/// �˿�����[1:����/2:��ͯ]
		/// </summary>
		public int PsgType
		{
			set{ _psgtype=value;}
			get{return _psgtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Param1
		{
			set{ _param1=value;}
			get{return _param1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Param2
		{
			set{ _param2=value;}
			get{return _param2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Param3
		{
			set{ _param3=value;}
			get{return _param3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Param4
		{
			set{ _param4=value;}
			get{return _param4;}
		}
		/// <summary>
		/// 0-��Ч 1-��Ч
		/// </summary>
		public int PolicyStatus
		{
			set{ _policystatus=value;}
			get{return _policystatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 0-��ɾ�� 1-δɾ��
		/// </summary>
		public int DelDegree
		{
			set{ _deldegree=value;}
			get{return _deldegree;}
		}
		#endregion Model

	}
}

