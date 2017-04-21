using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.PolicyReceiveService.DbEntity;
namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:PolicyDetail
	/// </summary>
	public partial class PolicyDetailLib
	{
		public PolicyDetailLib()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PolicyDetail");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ND.PolicyReceiveService.DbEntity.PolicyDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PolicyDetail(");
			strSql.Append("PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree)");
			strSql.Append(" values (");
			strSql.Append("@PolicyId,@SrcType,@CommisionType,@Comment,@AirlineCode,@DptCity,@ArrCity,@FlightIn,@FlightOut,@Seat,@SaleEffectDate,@SaleExpireDate,@SaleForbidEffectDate,@SaleForbidExpireDate,@FlightEffectDate,@FlightExpireDate,@FlightForbidEffectDate,@FlightForbidExpireDate,@EarliestIssueDays,@IsFitChild,@CommisionPoint,@CommisionMoney,@isSetPrivate,@PrivateCount,@OfficeNo,@NeedSwitchPNR,@IsAutoIssue,@IsPata,@BigClientCode,@MinimumTraveller,@IsProviderScore,@IsSharingFlight,@InvoiceType,@TuiGaiRule,@ChangeWorkTime,@ReturnWorkTime,@VtWorkTime,@ChangeWorkTimeWeekend,@ReturnWorkTimeWeekend,@VtWorkTimeWeekend,@IssueWorkTime,@IssueWorkTimeWeekend,@TicketSpeed,@FlightCycle,@PolicyType,@PsgType,@Param1,@Param2,@Param3,@Param4,@PolicyStatus,@CreateTime,@DelDegree)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PolicyId", SqlDbType.BigInt,8),
					new SqlParameter("@SrcType", SqlDbType.Int,4),
					new SqlParameter("@CommisionType", SqlDbType.Int,4),
					new SqlParameter("@Comment", SqlDbType.NVarChar),
					new SqlParameter("@AirlineCode", SqlDbType.VarChar,500),
					new SqlParameter("@DptCity", SqlDbType.VarChar),
					new SqlParameter("@ArrCity", SqlDbType.VarChar),
					new SqlParameter("@FlightIn", SqlDbType.VarChar),
					new SqlParameter("@FlightOut", SqlDbType.VarChar),
					new SqlParameter("@Seat", SqlDbType.NVarChar),
					new SqlParameter("@SaleEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleExpireDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@EarliestIssueDays", SqlDbType.Int,4),
					new SqlParameter("@IsFitChild", SqlDbType.Int,4),
					new SqlParameter("@CommisionPoint", SqlDbType.Decimal,9),
					new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
					new SqlParameter("@isSetPrivate", SqlDbType.Int,4),
					new SqlParameter("@PrivateCount", SqlDbType.Int,4),
					new SqlParameter("@OfficeNo", SqlDbType.VarChar,50),
					new SqlParameter("@NeedSwitchPNR", SqlDbType.Int,4),
					new SqlParameter("@IsAutoIssue", SqlDbType.Int,4),
					new SqlParameter("@IsPata", SqlDbType.Int,4),
					new SqlParameter("@BigClientCode", SqlDbType.VarChar,50),
					new SqlParameter("@MinimumTraveller", SqlDbType.Int,4),
					new SqlParameter("@IsProviderScore", SqlDbType.Int,4),
					new SqlParameter("@IsSharingFlight", SqlDbType.Int,4),
					new SqlParameter("@InvoiceType", SqlDbType.Int,4),
					new SqlParameter("@TuiGaiRule", SqlDbType.VarChar,500),
					new SqlParameter("@ChangeWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ChangeWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@TicketSpeed", SqlDbType.VarChar,50),
					new SqlParameter("@FlightCycle", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.VarChar,100),
					new SqlParameter("@PsgType", SqlDbType.Int,4),
					new SqlParameter("@Param1", SqlDbType.VarChar,50),
					new SqlParameter("@Param2", SqlDbType.VarChar,50),
					new SqlParameter("@Param3", SqlDbType.VarChar,50),
					new SqlParameter("@Param4", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyStatus", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DelDegree", SqlDbType.Int,4)};
			parameters[0].Value = model.PolicyId;
			parameters[1].Value = model.SrcType;
			parameters[2].Value = model.CommisionType;
			parameters[3].Value = model.Comment;
			parameters[4].Value = model.AirlineCode;
			parameters[5].Value = model.DptCity;
			parameters[6].Value = model.ArrCity;
			parameters[7].Value = model.FlightIn;
			parameters[8].Value = model.FlightOut;
			parameters[9].Value = model.Seat;
			parameters[10].Value = model.SaleEffectDate;
			parameters[11].Value = model.SaleExpireDate;
			parameters[12].Value = model.SaleForbidEffectDate;
			parameters[13].Value = model.SaleForbidExpireDate;
			parameters[14].Value = model.FlightEffectDate;
			parameters[15].Value = model.FlightExpireDate;
			parameters[16].Value = model.FlightForbidEffectDate;
			parameters[17].Value = model.FlightForbidExpireDate;
			parameters[18].Value = model.EarliestIssueDays;
			parameters[19].Value = model.IsFitChild;
			parameters[20].Value = model.CommisionPoint;
			parameters[21].Value = model.CommisionMoney;
			parameters[22].Value = model.isSetPrivate;
			parameters[23].Value = model.PrivateCount;
			parameters[24].Value = model.OfficeNo;
			parameters[25].Value = model.NeedSwitchPNR;
			parameters[26].Value = model.IsAutoIssue;
			parameters[27].Value = model.IsPata;
			parameters[28].Value = model.BigClientCode;
			parameters[29].Value = model.MinimumTraveller;
			parameters[30].Value = model.IsProviderScore;
			parameters[31].Value = model.IsSharingFlight;
			parameters[32].Value = model.InvoiceType;
			parameters[33].Value = model.TuiGaiRule;
			parameters[34].Value = model.ChangeWorkTime;
			parameters[35].Value = model.ReturnWorkTime;
			parameters[36].Value = model.VtWorkTime;
			parameters[37].Value = model.ChangeWorkTimeWeekend;
			parameters[38].Value = model.ReturnWorkTimeWeekend;
			parameters[39].Value = model.VtWorkTimeWeekend;
			parameters[40].Value = model.IssueWorkTime;
			parameters[41].Value = model.IssueWorkTimeWeekend;
			parameters[42].Value = model.TicketSpeed;
			parameters[43].Value = model.FlightCycle;
			parameters[44].Value = model.PolicyType;
			parameters[45].Value = model.PsgType;
			parameters[46].Value = model.Param1;
			parameters[47].Value = model.Param2;
			parameters[48].Value = model.Param3;
			parameters[49].Value = model.Param4;
			parameters[50].Value = model.PolicyStatus;
			parameters[51].Value = model.CreateTime;
			parameters[52].Value = model.DelDegree;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ND.PolicyReceiveService.DbEntity.PolicyDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PolicyDetail set ");
			strSql.Append("PolicyId=@PolicyId,");
			strSql.Append("SrcType=@SrcType,");
			strSql.Append("CommisionType=@CommisionType,");
			strSql.Append("Comment=@Comment,");
			strSql.Append("AirlineCode=@AirlineCode,");
			strSql.Append("DptCity=@DptCity,");
			strSql.Append("ArrCity=@ArrCity,");
			strSql.Append("FlightIn=@FlightIn,");
			strSql.Append("FlightOut=@FlightOut,");
			strSql.Append("Seat=@Seat,");
			strSql.Append("SaleEffectDate=@SaleEffectDate,");
			strSql.Append("SaleExpireDate=@SaleExpireDate,");
			strSql.Append("SaleForbidEffectDate=@SaleForbidEffectDate,");
			strSql.Append("SaleForbidExpireDate=@SaleForbidExpireDate,");
			strSql.Append("FlightEffectDate=@FlightEffectDate,");
			strSql.Append("FlightExpireDate=@FlightExpireDate,");
			strSql.Append("FlightForbidEffectDate=@FlightForbidEffectDate,");
			strSql.Append("FlightForbidExpireDate=@FlightForbidExpireDate,");
			strSql.Append("EarliestIssueDays=@EarliestIssueDays,");
			strSql.Append("IsFitChild=@IsFitChild,");
			strSql.Append("CommisionPoint=@CommisionPoint,");
			strSql.Append("CommisionMoney=@CommisionMoney,");
			strSql.Append("isSetPrivate=@isSetPrivate,");
			strSql.Append("PrivateCount=@PrivateCount,");
			strSql.Append("OfficeNo=@OfficeNo,");
			strSql.Append("NeedSwitchPNR=@NeedSwitchPNR,");
			strSql.Append("IsAutoIssue=@IsAutoIssue,");
			strSql.Append("IsPata=@IsPata,");
			strSql.Append("BigClientCode=@BigClientCode,");
			strSql.Append("MinimumTraveller=@MinimumTraveller,");
			strSql.Append("IsProviderScore=@IsProviderScore,");
			strSql.Append("IsSharingFlight=@IsSharingFlight,");
			strSql.Append("InvoiceType=@InvoiceType,");
			strSql.Append("TuiGaiRule=@TuiGaiRule,");
			strSql.Append("ChangeWorkTime=@ChangeWorkTime,");
			strSql.Append("ReturnWorkTime=@ReturnWorkTime,");
			strSql.Append("VtWorkTime=@VtWorkTime,");
			strSql.Append("ChangeWorkTimeWeekend=@ChangeWorkTimeWeekend,");
			strSql.Append("ReturnWorkTimeWeekend=@ReturnWorkTimeWeekend,");
			strSql.Append("VtWorkTimeWeekend=@VtWorkTimeWeekend,");
			strSql.Append("IssueWorkTime=@IssueWorkTime,");
			strSql.Append("IssueWorkTimeWeekend=@IssueWorkTimeWeekend,");
			strSql.Append("TicketSpeed=@TicketSpeed,");
			strSql.Append("FlightCycle=@FlightCycle,");
			strSql.Append("PolicyType=@PolicyType,");
			strSql.Append("PsgType=@PsgType,");
			strSql.Append("Param1=@Param1,");
			strSql.Append("Param2=@Param2,");
			strSql.Append("Param3=@Param3,");
			strSql.Append("Param4=@Param4,");
			strSql.Append("PolicyStatus=@PolicyStatus,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("DelDegree=@DelDegree");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@PolicyId", SqlDbType.BigInt,8),
					new SqlParameter("@SrcType", SqlDbType.Int,4),
					new SqlParameter("@CommisionType", SqlDbType.Int,4),
					new SqlParameter("@Comment", SqlDbType.NVarChar),
					new SqlParameter("@AirlineCode", SqlDbType.VarChar,500),
					new SqlParameter("@DptCity", SqlDbType.VarChar),
					new SqlParameter("@ArrCity", SqlDbType.VarChar),
					new SqlParameter("@FlightIn", SqlDbType.VarChar),
					new SqlParameter("@FlightOut", SqlDbType.VarChar),
					new SqlParameter("@Seat", SqlDbType.NVarChar),
					new SqlParameter("@SaleEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleExpireDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@EarliestIssueDays", SqlDbType.Int,4),
					new SqlParameter("@IsFitChild", SqlDbType.Int,4),
					new SqlParameter("@CommisionPoint", SqlDbType.Decimal,9),
					new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
					new SqlParameter("@isSetPrivate", SqlDbType.Int,4),
					new SqlParameter("@PrivateCount", SqlDbType.Int,4),
					new SqlParameter("@OfficeNo", SqlDbType.VarChar,50),
					new SqlParameter("@NeedSwitchPNR", SqlDbType.Int,4),
					new SqlParameter("@IsAutoIssue", SqlDbType.Int,4),
					new SqlParameter("@IsPata", SqlDbType.Int,4),
					new SqlParameter("@BigClientCode", SqlDbType.VarChar,50),
					new SqlParameter("@MinimumTraveller", SqlDbType.Int,4),
					new SqlParameter("@IsProviderScore", SqlDbType.Int,4),
					new SqlParameter("@IsSharingFlight", SqlDbType.Int,4),
					new SqlParameter("@InvoiceType", SqlDbType.Int,4),
					new SqlParameter("@TuiGaiRule", SqlDbType.VarChar,500),
					new SqlParameter("@ChangeWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ChangeWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@TicketSpeed", SqlDbType.VarChar,50),
					new SqlParameter("@FlightCycle", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.VarChar,100),
					new SqlParameter("@PsgType", SqlDbType.Int,4),
					new SqlParameter("@Param1", SqlDbType.VarChar,50),
					new SqlParameter("@Param2", SqlDbType.VarChar,50),
					new SqlParameter("@Param3", SqlDbType.VarChar,50),
					new SqlParameter("@Param4", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyStatus", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DelDegree", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.PolicyId;
			parameters[1].Value = model.SrcType;
			parameters[2].Value = model.CommisionType;
			parameters[3].Value = model.Comment;
			parameters[4].Value = model.AirlineCode;
			parameters[5].Value = model.DptCity;
			parameters[6].Value = model.ArrCity;
			parameters[7].Value = model.FlightIn;
			parameters[8].Value = model.FlightOut;
			parameters[9].Value = model.Seat;
			parameters[10].Value = model.SaleEffectDate;
			parameters[11].Value = model.SaleExpireDate;
			parameters[12].Value = model.SaleForbidEffectDate;
			parameters[13].Value = model.SaleForbidExpireDate;
			parameters[14].Value = model.FlightEffectDate;
			parameters[15].Value = model.FlightExpireDate;
			parameters[16].Value = model.FlightForbidEffectDate;
			parameters[17].Value = model.FlightForbidExpireDate;
			parameters[18].Value = model.EarliestIssueDays;
			parameters[19].Value = model.IsFitChild;
			parameters[20].Value = model.CommisionPoint;
			parameters[21].Value = model.CommisionMoney;
			parameters[22].Value = model.isSetPrivate;
			parameters[23].Value = model.PrivateCount;
			parameters[24].Value = model.OfficeNo;
			parameters[25].Value = model.NeedSwitchPNR;
			parameters[26].Value = model.IsAutoIssue;
			parameters[27].Value = model.IsPata;
			parameters[28].Value = model.BigClientCode;
			parameters[29].Value = model.MinimumTraveller;
			parameters[30].Value = model.IsProviderScore;
			parameters[31].Value = model.IsSharingFlight;
			parameters[32].Value = model.InvoiceType;
			parameters[33].Value = model.TuiGaiRule;
			parameters[34].Value = model.ChangeWorkTime;
			parameters[35].Value = model.ReturnWorkTime;
			parameters[36].Value = model.VtWorkTime;
			parameters[37].Value = model.ChangeWorkTimeWeekend;
			parameters[38].Value = model.ReturnWorkTimeWeekend;
			parameters[39].Value = model.VtWorkTimeWeekend;
			parameters[40].Value = model.IssueWorkTime;
			parameters[41].Value = model.IssueWorkTimeWeekend;
			parameters[42].Value = model.TicketSpeed;
			parameters[43].Value = model.FlightCycle;
			parameters[44].Value = model.PolicyType;
			parameters[45].Value = model.PsgType;
			parameters[46].Value = model.Param1;
			parameters[47].Value = model.Param2;
			parameters[48].Value = model.Param3;
			parameters[49].Value = model.Param4;
			parameters[50].Value = model.PolicyStatus;
			parameters[51].Value = model.CreateTime;
			parameters[52].Value = model.DelDegree;
			parameters[53].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PolicyDetail ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PolicyDetail ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ND.PolicyReceiveService.DbEntity.PolicyDetail GetModel(long Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree from PolicyDetail ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
			parameters[0].Value = Id;

			ND.PolicyReceiveService.DbEntity.PolicyDetail model=new ND.PolicyReceiveService.DbEntity.PolicyDetail();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PolicyId"].ToString()!="")
				{
					model.PolicyId=long.Parse(ds.Tables[0].Rows[0]["PolicyId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SrcType"].ToString()!="")
				{
					model.SrcType=int.Parse(ds.Tables[0].Rows[0]["SrcType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionType"].ToString()!="")
				{
					model.CommisionType=int.Parse(ds.Tables[0].Rows[0]["CommisionType"].ToString());
				}
				model.Comment=ds.Tables[0].Rows[0]["Comment"].ToString();
				model.AirlineCode=ds.Tables[0].Rows[0]["AirlineCode"].ToString();
				model.DptCity=ds.Tables[0].Rows[0]["DptCity"].ToString();
				model.ArrCity=ds.Tables[0].Rows[0]["ArrCity"].ToString();
				model.FlightIn=ds.Tables[0].Rows[0]["FlightIn"].ToString();
				model.FlightOut=ds.Tables[0].Rows[0]["FlightOut"].ToString();
				model.Seat=ds.Tables[0].Rows[0]["Seat"].ToString();
				if(ds.Tables[0].Rows[0]["SaleEffectDate"].ToString()!="")
				{
					model.SaleEffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["SaleEffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SaleExpireDate"].ToString()!="")
				{
					model.SaleExpireDate=DateTime.Parse(ds.Tables[0].Rows[0]["SaleExpireDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SaleForbidEffectDate"].ToString()!="")
				{
					model.SaleForbidEffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["SaleForbidEffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SaleForbidExpireDate"].ToString()!="")
				{
					model.SaleForbidExpireDate=DateTime.Parse(ds.Tables[0].Rows[0]["SaleForbidExpireDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FlightEffectDate"].ToString()!="")
				{
					model.FlightEffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["FlightEffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FlightExpireDate"].ToString()!="")
				{
					model.FlightExpireDate=DateTime.Parse(ds.Tables[0].Rows[0]["FlightExpireDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FlightForbidEffectDate"].ToString()!="")
				{
					model.FlightForbidEffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["FlightForbidEffectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FlightForbidExpireDate"].ToString()!="")
				{
					model.FlightForbidExpireDate=DateTime.Parse(ds.Tables[0].Rows[0]["FlightForbidExpireDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EarliestIssueDays"].ToString()!="")
				{
					model.EarliestIssueDays=int.Parse(ds.Tables[0].Rows[0]["EarliestIssueDays"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFitChild"].ToString()!="")
				{
					model.IsFitChild=int.Parse(ds.Tables[0].Rows[0]["IsFitChild"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionPoint"].ToString()!="")
				{
					model.CommisionPoint=decimal.Parse(ds.Tables[0].Rows[0]["CommisionPoint"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionMoney"].ToString()!="")
				{
					model.CommisionMoney=decimal.Parse(ds.Tables[0].Rows[0]["CommisionMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isSetPrivate"].ToString()!="")
				{
					model.isSetPrivate=int.Parse(ds.Tables[0].Rows[0]["isSetPrivate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrivateCount"].ToString()!="")
				{
					model.PrivateCount=int.Parse(ds.Tables[0].Rows[0]["PrivateCount"].ToString());
				}
				model.OfficeNo=ds.Tables[0].Rows[0]["OfficeNo"].ToString();
				if(ds.Tables[0].Rows[0]["NeedSwitchPNR"].ToString()!="")
				{
					model.NeedSwitchPNR=int.Parse(ds.Tables[0].Rows[0]["NeedSwitchPNR"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAutoIssue"].ToString()!="")
				{
					model.IsAutoIssue=int.Parse(ds.Tables[0].Rows[0]["IsAutoIssue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPata"].ToString()!="")
				{
					model.IsPata=int.Parse(ds.Tables[0].Rows[0]["IsPata"].ToString());
				}
				model.BigClientCode=ds.Tables[0].Rows[0]["BigClientCode"].ToString();
				if(ds.Tables[0].Rows[0]["MinimumTraveller"].ToString()!="")
				{
					model.MinimumTraveller=int.Parse(ds.Tables[0].Rows[0]["MinimumTraveller"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsProviderScore"].ToString()!="")
				{
					model.IsProviderScore=int.Parse(ds.Tables[0].Rows[0]["IsProviderScore"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSharingFlight"].ToString()!="")
				{
					model.IsSharingFlight=int.Parse(ds.Tables[0].Rows[0]["IsSharingFlight"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InvoiceType"].ToString()!="")
				{
					model.InvoiceType=int.Parse(ds.Tables[0].Rows[0]["InvoiceType"].ToString());
				}
				model.TuiGaiRule=ds.Tables[0].Rows[0]["TuiGaiRule"].ToString();
				model.ChangeWorkTime=ds.Tables[0].Rows[0]["ChangeWorkTime"].ToString();
				model.ReturnWorkTime=ds.Tables[0].Rows[0]["ReturnWorkTime"].ToString();
				model.VtWorkTime=ds.Tables[0].Rows[0]["VtWorkTime"].ToString();
				model.ChangeWorkTimeWeekend=ds.Tables[0].Rows[0]["ChangeWorkTimeWeekend"].ToString();
				model.ReturnWorkTimeWeekend=ds.Tables[0].Rows[0]["ReturnWorkTimeWeekend"].ToString();
				model.VtWorkTimeWeekend=ds.Tables[0].Rows[0]["VtWorkTimeWeekend"].ToString();
				model.IssueWorkTime=ds.Tables[0].Rows[0]["IssueWorkTime"].ToString();
				model.IssueWorkTimeWeekend=ds.Tables[0].Rows[0]["IssueWorkTimeWeekend"].ToString();
				model.TicketSpeed=ds.Tables[0].Rows[0]["TicketSpeed"].ToString();
				model.FlightCycle=ds.Tables[0].Rows[0]["FlightCycle"].ToString();
				model.PolicyType=ds.Tables[0].Rows[0]["PolicyType"].ToString();
				if(ds.Tables[0].Rows[0]["PsgType"].ToString()!="")
				{
					model.PsgType=int.Parse(ds.Tables[0].Rows[0]["PsgType"].ToString());
				}
				model.Param1=ds.Tables[0].Rows[0]["Param1"].ToString();
				model.Param2=ds.Tables[0].Rows[0]["Param2"].ToString();
				model.Param3=ds.Tables[0].Rows[0]["Param3"].ToString();
				model.Param4=ds.Tables[0].Rows[0]["Param4"].ToString();
				if(ds.Tables[0].Rows[0]["PolicyStatus"].ToString()!="")
				{
					model.PolicyStatus=int.Parse(ds.Tables[0].Rows[0]["PolicyStatus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelDegree"].ToString()!="")
				{
					model.DelDegree=int.Parse(ds.Tables[0].Rows[0]["DelDegree"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree ");
			strSql.Append(" FROM PolicyDetail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree ");
			strSql.Append(" FROM PolicyDetail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "PolicyDetail";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region extend Method

        #region 获取updateSql语句
        public SqlParameter[] UpdateSql(PolicyDetail model, ref string updateSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PolicyDetail set ");
            strSql.Append(" PartnerPolicyId=@PartnerPolicyId,");
            strSql.Append("PolicyId=@PolicyId,");
            strSql.Append("SrcType=@SrcType,");
            strSql.Append("CommisionType=@CommisionType,");
            strSql.Append("Comment=@Comment,");
            strSql.Append("AirlineCode=@AirlineCode,");
            strSql.Append("DptCity=@DptCity,");
            strSql.Append("ArrCity=@ArrCity,");
            strSql.Append("FlightIn=@FlightIn,");
            strSql.Append("FlightOut=@FlightOut,");
            strSql.Append("Seat=@Seat,");
            strSql.Append("SaleEffectDate=@SaleEffectDate,");
            strSql.Append("SaleExpireDate=@SaleExpireDate,");
            strSql.Append("SaleForbidEffectDate=@SaleForbidEffectDate,");
            strSql.Append("SaleForbidExpireDate=@SaleForbidExpireDate,");
            strSql.Append("FlightEffectDate=@FlightEffectDate,");
            strSql.Append("FlightExpireDate=@FlightExpireDate,");
            strSql.Append("FlightForbidEffectDate=@FlightForbidEffectDate,");
            strSql.Append("FlightForbidExpireDate=@FlightForbidExpireDate,");
            strSql.Append("EarliestIssueDays=@EarliestIssueDays,");
            strSql.Append("IsFitChild=@IsFitChild,");
            strSql.Append("CommisionPoint=@CommisionPoint,");
            strSql.Append("CommisionMoney=@CommisionMoney,");
            strSql.Append("isSetPrivate=@isSetPrivate,");
            strSql.Append("PrivateCount=@PrivateCount,");
            strSql.Append("OfficeNo=@OfficeNo,");
            strSql.Append("NeedSwitchPNR=@NeedSwitchPNR,");
            strSql.Append("IsAutoIssue=@IsAutoIssue,");
            strSql.Append("IsPata=@IsPata,");
            strSql.Append("BigClientCode=@BigClientCode,");
            strSql.Append("MinimumTraveller=@MinimumTraveller,");
            strSql.Append("IsProviderScore=@IsProviderScore,");
            strSql.Append("IsSharingFlight=@IsSharingFlight,");
            strSql.Append("InvoiceType=@InvoiceType,");
            strSql.Append("TuiGaiRule=@TuiGaiRule,");
            strSql.Append("ChangeWorkTime=@ChangeWorkTime,");
            strSql.Append("ReturnWorkTime=@ReturnWorkTime,");
            strSql.Append("VtWorkTime=@VtWorkTime,");
            strSql.Append("ChangeWorkTimeWeekend=@ChangeWorkTimeWeekend,");
            strSql.Append("ReturnWorkTimeWeekend=@ReturnWorkTimeWeekend,");
            strSql.Append("VtWorkTimeWeekend=@VtWorkTimeWeekend,");
            strSql.Append("IssueWorkTime=@IssueWorkTime,");
            strSql.Append("IssueWorkTimeWeekend=@IssueWorkTimeWeekend,");
            strSql.Append("TicketSpeed=@TicketSpeed,");
            strSql.Append("FlightCycle=@FlightCycle,");
            strSql.Append("PolicyType=@PolicyType,");
            strSql.Append("PsgType=@PsgType,");
            strSql.Append("Param1=@Param1,");
            strSql.Append("Param2=@Param2,");
            strSql.Append("Param3=@Param3,");
            strSql.Append("Param4=@Param4,");
            strSql.Append("PolicyStatus=@PolicyStatus,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("DelDegree=@DelDegree");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@PolicyId", SqlDbType.BigInt,8),
					new SqlParameter("@SrcType", SqlDbType.Int,4),
					new SqlParameter("@CommisionType", SqlDbType.Int,4),
					new SqlParameter("@Comment", SqlDbType.NVarChar),
					new SqlParameter("@AirlineCode", SqlDbType.VarChar,500),
					new SqlParameter("@DptCity", SqlDbType.VarChar),
					new SqlParameter("@ArrCity", SqlDbType.VarChar),
					new SqlParameter("@FlightIn", SqlDbType.VarChar),
					new SqlParameter("@FlightOut", SqlDbType.VarChar),
					new SqlParameter("@Seat", SqlDbType.NVarChar),
					new SqlParameter("@SaleEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleExpireDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@SaleForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightExpireDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidEffectDate", SqlDbType.DateTime),
					new SqlParameter("@FlightForbidExpireDate", SqlDbType.DateTime),
					new SqlParameter("@EarliestIssueDays", SqlDbType.Int,4),
					new SqlParameter("@IsFitChild", SqlDbType.Int,4),
					new SqlParameter("@CommisionPoint", SqlDbType.Decimal,9),
					new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
					new SqlParameter("@isSetPrivate", SqlDbType.Int,4),
					new SqlParameter("@PrivateCount", SqlDbType.Int,4),
					new SqlParameter("@OfficeNo", SqlDbType.VarChar,50),
					new SqlParameter("@NeedSwitchPNR", SqlDbType.Int,4),
					new SqlParameter("@IsAutoIssue", SqlDbType.Int,4),
					new SqlParameter("@IsPata", SqlDbType.Int,4),
					new SqlParameter("@BigClientCode", SqlDbType.VarChar,50),
					new SqlParameter("@MinimumTraveller", SqlDbType.Int,4),
					new SqlParameter("@IsProviderScore", SqlDbType.Int,4),
					new SqlParameter("@IsSharingFlight", SqlDbType.Int,4),
					new SqlParameter("@InvoiceType", SqlDbType.Int,4),
					new SqlParameter("@TuiGaiRule", SqlDbType.VarChar,500),
					new SqlParameter("@ChangeWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@ChangeWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@VtWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTime", SqlDbType.VarChar,50),
					new SqlParameter("@IssueWorkTimeWeekend", SqlDbType.VarChar,50),
					new SqlParameter("@TicketSpeed", SqlDbType.VarChar,50),
					new SqlParameter("@FlightCycle", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.VarChar,100),
					new SqlParameter("@PsgType", SqlDbType.Int,4),
					new SqlParameter("@Param1", SqlDbType.VarChar,50),
					new SqlParameter("@Param2", SqlDbType.VarChar,50),
					new SqlParameter("@Param3", SqlDbType.VarChar,50),
					new SqlParameter("@Param4", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyStatus", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DelDegree", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.BigInt,8),
                    new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.PolicyId;
            parameters[1].Value = model.SrcType;
            parameters[2].Value = model.CommisionType;
            parameters[3].Value = model.Comment;
            parameters[4].Value = model.AirlineCode;
            parameters[5].Value = model.DptCity;
            parameters[6].Value = model.ArrCity;
            parameters[7].Value = model.FlightIn;
            parameters[8].Value = model.FlightOut;
            parameters[9].Value = model.Seat;
            parameters[10].Value = model.SaleEffectDate;
            parameters[11].Value = model.SaleExpireDate;
            parameters[12].Value = model.SaleForbidEffectDate;
            parameters[13].Value = model.SaleForbidExpireDate;
            parameters[14].Value = model.FlightEffectDate;
            parameters[15].Value = model.FlightExpireDate;
            parameters[16].Value = model.FlightForbidEffectDate;
            parameters[17].Value = model.FlightForbidExpireDate;
            parameters[18].Value = model.EarliestIssueDays;
            parameters[19].Value = model.IsFitChild;
            parameters[20].Value = model.CommisionPoint;
            parameters[21].Value = model.CommisionMoney;
            parameters[22].Value = model.isSetPrivate;
            parameters[23].Value = model.PrivateCount;
            parameters[24].Value = model.OfficeNo;
            parameters[25].Value = model.NeedSwitchPNR;
            parameters[26].Value = model.IsAutoIssue;
            parameters[27].Value = model.IsPata;
            parameters[28].Value = model.BigClientCode;
            parameters[29].Value = model.MinimumTraveller;
            parameters[30].Value = model.IsProviderScore;
            parameters[31].Value = model.IsSharingFlight;
            parameters[32].Value = model.InvoiceType;
            parameters[33].Value = model.TuiGaiRule;
            parameters[34].Value = model.ChangeWorkTime;
            parameters[35].Value = model.ReturnWorkTime;
            parameters[36].Value = model.VtWorkTime;
            parameters[37].Value = model.ChangeWorkTimeWeekend;
            parameters[38].Value = model.ReturnWorkTimeWeekend;
            parameters[39].Value = model.VtWorkTimeWeekend;
            parameters[40].Value = model.IssueWorkTime;
            parameters[41].Value = model.IssueWorkTimeWeekend;
            parameters[42].Value = model.TicketSpeed;
            parameters[43].Value = model.FlightCycle;
            parameters[44].Value = model.PolicyType;
            parameters[45].Value = model.PsgType;
            parameters[46].Value = model.Param1;
            parameters[47].Value = model.Param2;
            parameters[48].Value = model.Param3;
            parameters[49].Value = model.Param4;
            parameters[50].Value = model.PolicyStatus;
            parameters[51].Value = model.CreateTime;
            parameters[52].Value = model.DelDegree;
            parameters[53].Value = model.Id;
            parameters[54].Value = model.PartnerPolicyId;
            updateSql = strSql.ToString();
            return parameters;

        } 
        #endregion

       
        #endregion
	}
}

