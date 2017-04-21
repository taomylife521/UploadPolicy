using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ND.PolicyReceiveService.DbEntity;
using System.Collections;
using ND.PolicyService.Enums;
using System.IO;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums.Upload;
using Newtonsoft.Json;
namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:PolicySyncRec
	/// </summary>
	public partial class PolicySyncRecLib
	{
		public PolicySyncRecLib()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PolicySyncRec");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ND.PolicyReceiveService.DbEntity.PolicySyncRec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PolicySyncRec(");
			strSql.Append("UpdateTime,PartnerId,PartnerName,PartnerPolicyId)");
			strSql.Append(" values (");
			strSql.Append("@UpdateTime,@PartnerId,@PartnerName,@PartnerPolicyId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PartnerId", SqlDbType.Int,4),
					new SqlParameter("@PartnerName", SqlDbType.NVarChar,50),
					new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,100)};
			parameters[0].Value = model.UpdateTime;
			parameters[1].Value = model.PartnerId;
			parameters[2].Value = model.PartnerName;
			parameters[3].Value = model.PartnerPolicyId;

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
		public bool Update(ND.PolicyReceiveService.DbEntity.PolicySyncRec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PolicySyncRec set ");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("PartnerId=@PartnerId,");
			strSql.Append("PartnerName=@PartnerName,");
			strSql.Append("PartnerPolicyId=@PartnerPolicyId");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PartnerId", SqlDbType.Int,4),
					new SqlParameter("@PartnerName", SqlDbType.NVarChar,50),
					new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,100),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.UpdateTime;
			parameters[1].Value = model.PartnerId;
			parameters[2].Value = model.PartnerName;
			parameters[3].Value = model.PartnerPolicyId;
			parameters[4].Value = model.Id;

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
			strSql.Append("delete from PolicySyncRec ");
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
			strSql.Append("delete from PolicySyncRec ");
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
		public ND.PolicyReceiveService.DbEntity.PolicySyncRec GetModel(long Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,UpdateTime,PartnerId,PartnerName,PartnerPolicyId from PolicySyncRec ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
			parameters[0].Value = Id;

			ND.PolicyReceiveService.DbEntity.PolicySyncRec model=new ND.PolicyReceiveService.DbEntity.PolicySyncRec();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartnerId"].ToString()!="")
				{
					model.PartnerId=int.Parse(ds.Tables[0].Rows[0]["PartnerId"].ToString());
				}
				model.PartnerName=ds.Tables[0].Rows[0]["PartnerName"].ToString();
				model.PartnerPolicyId=ds.Tables[0].Rows[0]["PartnerPolicyId"].ToString();
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
			strSql.Append("select Id,UpdateTime,PartnerId,PartnerName,PartnerPolicyId ");
			strSql.Append(" FROM PolicySyncRec ");
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
			strSql.Append(" Id,UpdateTime,PartnerId,PartnerName,PartnerPolicyId ");
			strSql.Append(" FROM PolicySyncRec ");
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
			parameters[0].Value = "PolicySyncRec";
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

        #region 旧代码
        //#region 从从库中获取存在的政策列表
        //public List<string> ExistPartenerIdList(string[] arryPolicyId91e)
        //{

        //    string strSQL = "SELECT PartnerPolicyId FROM ndFlightSlave.dbo.PolicySyncRec WHERE PartnerPolicyId IN ('" + String.Join("','", arryPolicyId91e) + "')";
        //    List<string> existIdList = new List<string>();
        //    SqlDataReader dr = DbHelperSQL.ExecuteReader(strSQL);
        //    while (dr.Read())
        //    {
        //        existIdList.Add(dr[0].ToString());
        //    }
        //    dr.Dispose();
        //    dr.Close();
        //    return existIdList;
        //}
        //#endregion

        //#region 更新数据
        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(ND.PolicyReceiveService.DbEntity.PolicySyncRec model, PolicyDetail policyDetail)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("update PolicySyncRec set ");
        //        strSql.Append("UpdateTime=@UpdateTime,");
        //        strSql.Append("PartnerId=@PartnerId,");
        //        strSql.Append("PartnerName=@PartnerName,");
        //        strSql.Append("PartnerPolicyId=@PartnerPolicyId");
        //        strSql.Append(" where Id=@Id");
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@UpdateTime", SqlDbType.DateTime),
        //            new SqlParameter("@PartnerId", SqlDbType.Int,4),
        //            new SqlParameter("@PartnerName", SqlDbType.NVarChar,50),
        //            new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,100),
        //            new SqlParameter("@Id", SqlDbType.BigInt,8)};
        //        parameters[0].Value = model.UpdateTime;
        //        parameters[1].Value = model.PartnerId;
        //        parameters[2].Value = model.PartnerName;
        //        parameters[3].Value = model.PartnerPolicyId;
        //        parameters[4].Value = model.Id;

        //        PolicyDetailLib policyDetailLib = new PolicyDetailLib();
        //        string policyUpdateSql = "";
        //        SqlParameter[] parametersDetail = policyDetailLib.UpdateSql(policyDetail, ref policyUpdateSql);
        //        Hashtable ht = new Hashtable();
        //        ht.Add(strSql.ToString(), parameters);
        //        ht.Add(policyUpdateSql, parametersDetail);
        //        DbHelperSQL.ExecuteSqlTran(ht);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //#endregion

        //#region 添加数据
        //public bool Add(PolicySyncRec syncRec, PolicyDetail model)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append(" declare @PId BIGINT ");
        //        strSql.Append(" insert into PolicySyncRec(");
        //        strSql.Append(" UpdateTime,PartnerId,PartnerName,PartnerPolicyId)");
        //        strSql.Append(" values (");
        //        strSql.Append(" @UpdateTime,@PartnerId,@PartnerName,@PartnerPolicyId)");
        //        strSql.Append(";Set @PId=@@IDENTITY ");

        //        strSql.Append(" insert into PolicyDetail(");
        //        strSql.Append("PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree,PartnerPolicyId,PlatformCommisionPoint,PlatformCommisionMoney)");
        //        strSql.Append(" values (");
        //        strSql.Append("@PId,@SrcType,@CommisionType,@Comment,@AirlineCode,@DptCity,@ArrCity,@FlightIn,@FlightOut,@Seat,@SaleEffectDate,@SaleExpireDate,@SaleForbidEffectDate,@SaleForbidExpireDate,@FlightEffectDate,@FlightExpireDate,@FlightForbidEffectDate,@FlightForbidExpireDate,@EarliestIssueDays,@IsFitChild,@CommisionPoint,@CommisionMoney,@isSetPrivate,@PrivateCount,@OfficeNo,@NeedSwitchPNR,@IsAutoIssue,@IsPata,@BigClientCode,@MinimumTraveller,@IsProviderScore,@IsSharingFlight,@InvoiceType,@TuiGaiRule,@ChangeWorkTime,@ReturnWorkTime,@VtWorkTime,@ChangeWorkTimeWeekend,@ReturnWorkTimeWeekend,@VtWorkTimeWeekend,@IssueWorkTime,@IssueWorkTimeWeekend,@TicketSpeed,@FlightCycle,@PolicyType,@PsgType,@Param1,@Param2,@Param3,@Param4,@PolicyStatus,@CreateTime,@DelDegree,@PartnerPolicyId,@PlatformCommisionPoint,@PlatformCommisionMoney)");
        //        strSql.Append(";select @@IDENTITY");
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@SrcType", SqlDbType.Int,4),
        //            new SqlParameter("@CommisionType", SqlDbType.Int,4),
        //            new SqlParameter("@Comment", SqlDbType.NVarChar),
        //            new SqlParameter("@AirlineCode", SqlDbType.VarChar,500),
        //            new SqlParameter("@DptCity", SqlDbType.VarChar),
        //            new SqlParameter("@ArrCity", SqlDbType.VarChar),
        //            new SqlParameter("@FlightIn", SqlDbType.VarChar),
        //            new SqlParameter("@FlightOut", SqlDbType.VarChar),
        //            new SqlParameter("@Seat", SqlDbType.NVarChar),
        //            new SqlParameter("@SaleEffectDate", SqlDbType.DateTime),
        //            new SqlParameter("@SaleExpireDate", SqlDbType.DateTime),
        //            new SqlParameter("@SaleForbidEffectDate", SqlDbType.DateTime),
        //            new SqlParameter("@SaleForbidExpireDate", SqlDbType.DateTime),
        //            new SqlParameter("@FlightEffectDate", SqlDbType.DateTime),
        //            new SqlParameter("@FlightExpireDate", SqlDbType.DateTime),
        //            new SqlParameter("@FlightForbidEffectDate", SqlDbType.DateTime),
        //            new SqlParameter("@FlightForbidExpireDate", SqlDbType.DateTime),
        //            new SqlParameter("@EarliestIssueDays", SqlDbType.Int,4),
        //            new SqlParameter("@IsFitChild", SqlDbType.Int,4),
        //            new SqlParameter("@CommisionPoint", SqlDbType.Decimal,9),
        //            new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
        //            new SqlParameter("@isSetPrivate", SqlDbType.Int,4),
        //            new SqlParameter("@PrivateCount", SqlDbType.Int,4),
        //            new SqlParameter("@OfficeNo", SqlDbType.VarChar,50),
        //            new SqlParameter("@NeedSwitchPNR", SqlDbType.Int,4),
        //            new SqlParameter("@IsAutoIssue", SqlDbType.Int,4),
        //            new SqlParameter("@IsPata", SqlDbType.Int,4),
        //            new SqlParameter("@BigClientCode", SqlDbType.VarChar,50),
        //            new SqlParameter("@MinimumTraveller", SqlDbType.Int,4),
        //            new SqlParameter("@IsProviderScore", SqlDbType.Int,4),
        //            new SqlParameter("@IsSharingFlight", SqlDbType.Int,4),
        //            new SqlParameter("@InvoiceType", SqlDbType.Int,4),
        //            new SqlParameter("@TuiGaiRule", SqlDbType.VarChar,500),
        //            new SqlParameter("@ChangeWorkTime", SqlDbType.VarChar,50),
        //            new SqlParameter("@ReturnWorkTime", SqlDbType.VarChar,50),
        //            new SqlParameter("@VtWorkTime", SqlDbType.VarChar,50),
        //            new SqlParameter("@ChangeWorkTimeWeekend", SqlDbType.VarChar,50),
        //            new SqlParameter("@ReturnWorkTimeWeekend", SqlDbType.VarChar,50),
        //            new SqlParameter("@VtWorkTimeWeekend", SqlDbType.VarChar,50),
        //            new SqlParameter("@IssueWorkTime", SqlDbType.VarChar,50),
        //            new SqlParameter("@IssueWorkTimeWeekend", SqlDbType.VarChar,50),
        //            new SqlParameter("@TicketSpeed", SqlDbType.VarChar,50),
        //            new SqlParameter("@FlightCycle", SqlDbType.VarChar,50),
        //            new SqlParameter("@PolicyType", SqlDbType.VarChar,100),
        //            new SqlParameter("@PsgType", SqlDbType.Int,4),
        //            new SqlParameter("@Param1", SqlDbType.VarChar,50),
        //            new SqlParameter("@Param2", SqlDbType.VarChar,50),
        //            new SqlParameter("@Param3", SqlDbType.VarChar,50),
        //            new SqlParameter("@Param4", SqlDbType.VarChar,50),
        //            new SqlParameter("@PolicyStatus", SqlDbType.Int,4),
        //            new SqlParameter("@CreateTime", SqlDbType.DateTime),
        //            new SqlParameter("@DelDegree", SqlDbType.Int,4),
        //            new SqlParameter("@UpdateTime", SqlDbType.DateTime),
        //            new SqlParameter("@PartnerId", SqlDbType.Int,4),
        //            new SqlParameter("@PartnerName", SqlDbType.NVarChar,50),
        //            new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,100),
        //            new SqlParameter("@PlatformCommisionPoint", SqlDbType.Decimal,4),
        //            new SqlParameter("@PlatformCommisionMoney", SqlDbType.Decimal,4)
        //                                };

        //        parameters[0].Value = model.SrcType;
        //        parameters[1].Value = model.CommisionType;
        //        parameters[2].Value = model.Comment;
        //        parameters[3].Value = model.AirlineCode;
        //        parameters[4].Value = model.DptCity;
        //        parameters[5].Value = model.ArrCity;
        //        parameters[6].Value = model.FlightIn;
        //        parameters[7].Value = model.FlightOut;
        //        parameters[8].Value = model.Seat;
        //        parameters[9].Value = model.SaleEffectDate;
        //        parameters[10].Value = model.SaleExpireDate;
        //        parameters[11].Value = model.SaleForbidEffectDate;
        //        parameters[12].Value = model.SaleForbidExpireDate;
        //        parameters[13].Value = model.FlightEffectDate;
        //        parameters[14].Value = model.FlightExpireDate;
        //        parameters[15].Value = model.FlightForbidEffectDate;
        //        parameters[16].Value = model.FlightForbidExpireDate;
        //        parameters[17].Value = model.EarliestIssueDays;
        //        parameters[18].Value = model.IsFitChild;
        //        parameters[19].Value = model.CommisionPoint;
        //        parameters[20].Value = model.CommisionMoney;
        //        parameters[21].Value = model.isSetPrivate;
        //        parameters[22].Value = model.PrivateCount;
        //        parameters[23].Value = model.OfficeNo;
        //        parameters[24].Value = model.NeedSwitchPNR;
        //        parameters[25].Value = model.IsAutoIssue;
        //        parameters[26].Value = model.IsPata;
        //        parameters[27].Value = model.BigClientCode;
        //        parameters[28].Value = model.MinimumTraveller;
        //        parameters[29].Value = model.IsProviderScore;
        //        parameters[30].Value = model.IsSharingFlight;
        //        parameters[31].Value = model.InvoiceType;
        //        parameters[32].Value = model.TuiGaiRule;
        //        parameters[33].Value = model.ChangeWorkTime;
        //        parameters[34].Value = model.ReturnWorkTime;
        //        parameters[35].Value = model.VtWorkTime;
        //        parameters[36].Value = model.ChangeWorkTimeWeekend;
        //        parameters[37].Value = model.ReturnWorkTimeWeekend;
        //        parameters[38].Value = model.VtWorkTimeWeekend;
        //        parameters[39].Value = model.IssueWorkTime;
        //        parameters[40].Value = model.IssueWorkTimeWeekend;
        //        parameters[41].Value = model.TicketSpeed;
        //        parameters[42].Value = model.FlightCycle;
        //        parameters[43].Value = model.PolicyType;
        //        parameters[44].Value = model.PsgType;
        //        parameters[45].Value = model.Param1;
        //        parameters[46].Value = model.Param2;
        //        parameters[47].Value = model.Param3;
        //        parameters[48].Value = model.Param4;
        //        parameters[49].Value = model.PolicyStatus;
        //        parameters[50].Value = model.CreateTime;
        //        parameters[51].Value = model.DelDegree;
        //        parameters[52].Value = syncRec.UpdateTime;
        //        parameters[53].Value = syncRec.PartnerId;
        //        parameters[54].Value = syncRec.PartnerName;
        //        parameters[55].Value = syncRec.PartnerPolicyId;
        //        parameters[56].Value = model.PlatformCommisionPoint;
        //        parameters[57].Value = model.PlatformCommisionMoney;
        //        Hashtable ht = new Hashtable();
        //        ht.Add(strSql.ToString(), parameters);
        //        DbHelperSQL.ExecuteSqlTran(ht);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //#endregion

        //#region 获取增量新增政策数据
        //public List<Policies> LoadIncrementalAddPolicy(PolicyRecord policyRec, decimal commisionMoney, decimal commsionPoint, int maxCount, string whereCondition, PurchaserType purchaser)
        //{
        //    List<Policies> lstPolicy = new List<Policies>();
        //    int pageSize = maxCount;
        //    string tableName = purchaser + "Policy";
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        StringBuilder strWhere = new StringBuilder();
        //        string OrderBy = "  ORDER BY pd.id asc ";

        //        #region 要查询的字段
        //        List<string> fileds = new List<string> {
        //        "pd.Id",//政策id
        //        "pd.PolicyId",//未拆分前的政策id
        //        "pd.SrcType",
        //        "pd.CommisionType",
        //        "pd.Comment",
        //        "pd.AirlineCode",
        //        "pd.DptCity",
        //        "pd.ArrCity",
        //        "pd.FlightIn",
        //        "pd.FlightOut",
        //        "pd.Seat",
        //        "pd.SaleEffectDate",
        //        "pd.SaleExpireDate",
        //        "pd.SaleForbidEffectDate",
        //        "pd.SaleForbidExpireDate",
        //        "pd.FlightEffectDate",
        //        "pd.FlightExpireDate",
        //        "pd.FlightForbidEffectDate",
        //        "pd.FlightForbidExpireDate",
        //        "pd.EarliestIssueDays",
        //        "pd.IsFitChild",
        //        "pd.CommisionPoint+("+commsionPoint+") as CommisionPoint",
        //        "pd.PlatformCommisionPoint",
        //        "pd.CommisionMoney+("+commisionMoney+") as CommisionMoney",
        //        "pd.PlatformCommisionMoney",
        //        "pd.isSetPrivate",
        //        "pd.PrivateCount",
        //        "pd.OfficeNo",
        //        "pd.NeedSwitchPNR",
        //        "pd.IsAutoIssue",
        //        "pd.IsPata",
        //        "pd.BigClientCode",
        //        "pd.MinimumTraveller",
        //        "pd.IsProviderScore",
        //        "pd.IsSharingFlight",
        //        "pd.InvoiceType",
        //        "pd.TuiGaiRule",
        //        "pd.ChangeWorkTime",
        //        "pd.ReturnWorkTime",
        //        "pd.VtWorkTime",
        //        "pd.ChangeWorkTimeWeekend",
        //        "pd.ReturnWorkTimeWeekend",
        //        "pd.VtWorkTimeWeekend",
        //        "pd.IssueWorkTime",
        //        "pd.IssueWorkTimeWeekend",
        //        "pd.TicketSpeed",
        //        "pd.FlightCycle",
        //        "pd.PolicyType",
        //        "pd.PsgType",
        //        "pd.Param1",
        //        "pd.Param2",
        //        "pd.Param3",
        //        "pd.Param4",
        //        "pd.PolicyStatus",
        //        "pd.PlatformPolicyStatus"
        //    };
        //        #endregion

        //        strSql.Append("select top " + pageSize.ToString() + " * from " + tableName + " as pd ");
        //        strSql.Append(" where  pd.PlatformPolicyStatus=1 and pd.DelDegree=1 ");
        //        strSql.Append(" and (pd.CreateTime > '" + policyRec.LastUpdateTime + "' or (pd.CreateTime = '" + policyRec.LastUpdateTime + "' and pd.Id > " + policyRec.LastPolicyId + "))");
        //        if (!string.IsNullOrEmpty(whereCondition))
        //        {
        //            strSql.Append(" and " + whereCondition);
        //        }
        //        strSql.Append(OrderBy);


        //        //string fds= String.Join(",", fileds.ToArray());
        //        //string excuteSql = "select * from (select " + String.Join(",", fileds.ToArray()) + ", ROW_NUMBER() OVER(" + OrderBy + ") AS rowNo FROM " + strTable.ToString() + strWhere.ToString() + ")  AS T WHERE rowNo BETWEEN 1 AND 20";

        //        SqlDataReader dataReader = DbHelperSQL.ExecuteReader(strSql.ToString());

        //        while (dataReader.Read())
        //        {
        //            #region 封装实体
        //            Policies py = new Policies();
        //            py.PolicyId = long.Parse(dataReader["PolicyId"].ToString());
        //            py.Id = long.Parse(dataReader["Id"].ToString());
        //            py.UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"].ToString());
        //            py.PartnerId = int.Parse(dataReader["PartnerId"].ToString());
        //            py.PartnerName = dataReader["PartnerName"].ToString();
        //            py.PartnerPolicyId = dataReader["PartnerPolicyId"].ToString();
        //            py.SrcType = int.Parse(dataReader["SrcType"].ToString());
        //            py.CommisionType = int.Parse(dataReader["CommisionType"].ToString());
        //            py.Comment = dataReader["Comment"].ToString();
        //            py.AirlineCode = dataReader["AirlineCode"].ToString();
        //            py.DptCity = dataReader["DptCity"].ToString();
        //            py.ArrCity = dataReader["ArrCity"].ToString();
        //            py.FlightIn = dataReader["FlightIn"].ToString();
        //            py.FlightOut = dataReader["FlightOut"].ToString();
        //            py.Seat = dataReader["Seat"].ToString();
        //            py.SaleEffectDate = Convert.ToDateTime(dataReader["SaleEffectDate"].ToString());
        //            py.SaleExpireDate = Convert.ToDateTime(dataReader["SaleExpireDate"].ToString());
        //            py.SaleForbidEffectDate = Convert.ToDateTime(dataReader["SaleForbidEffectDate"].ToString());
        //            py.SaleForbidExpireDate = Convert.ToDateTime(dataReader["SaleForbidExpireDate"].ToString());
        //            py.FlightEffectDate = Convert.ToDateTime(dataReader["FlightEffectDate"].ToString());
        //            py.FlightExpireDate = Convert.ToDateTime(dataReader["FlightExpireDate"].ToString());
        //            py.FlightForbidEffectDate = Convert.ToDateTime(dataReader["FlightForbidEffectDate"].ToString());
        //            py.FlightForbidExpireDate = Convert.ToDateTime(dataReader["FlightForbidExpireDate"].ToString());
        //            py.EarliestIssueDays = int.Parse(dataReader["EarliestIssueDays"].ToString());
        //            py.IsFitChild = int.Parse(dataReader["IsFitChild"].ToString());
        //            py.CommisionPoint = Convert.ToDecimal(dataReader["CommisionPoint"].ToString());
        //            py.PlatformCommisionPoint = Convert.ToDecimal(dataReader["PlatformCommisionPoint"].ToString());
        //            py.CommisionMoney = Convert.ToDecimal(dataReader["CommisionMoney"].ToString());
        //            py.PlatformCommisionMoney = Convert.ToDecimal(dataReader["PlatformCommisionMoney"].ToString());
        //            py.isSetPrivate = int.Parse(dataReader["isSetPrivate"].ToString());
        //            py.PrivateCount = int.Parse(dataReader["PrivateCount"].ToString());
        //            py.OfficeNo = dataReader["OfficeNo"].ToString();
        //            py.NeedSwitchPNR = int.Parse(dataReader["NeedSwitchPNR"].ToString());
        //            py.IsAutoIssue = int.Parse(dataReader["IsAutoIssue"].ToString());
        //            py.IsPata = int.Parse(dataReader["IsPata"].ToString());
        //            py.BigClientCode = dataReader["BigClientCode"].ToString();
        //            py.MinimumTraveller = int.Parse(dataReader["MinimumTraveller"].ToString());
        //            py.IsProviderScore = int.Parse(dataReader["IsProviderScore"].ToString());
        //            py.IsSharingFlight = int.Parse(dataReader["IsSharingFlight"].ToString());
        //            py.InvoiceType = int.Parse(dataReader["InvoiceType"].ToString());
        //            py.TuiGaiRule = dataReader["TuiGaiRule"].ToString();
        //            py.ChangeWorkTime = dataReader["ChangeWorkTime"].ToString();
        //            py.ReturnWorkTime = dataReader["ReturnWorkTime"].ToString();
        //            py.VtWorkTime = dataReader["VtWorkTime"].ToString();
        //            py.ChangeWorkTimeWeekend = dataReader["ChangeWorkTimeWeekend"].ToString();
        //            py.ReturnWorkTimeWeekend = dataReader["ReturnWorkTimeWeekend"].ToString();
        //            py.VtWorkTimeWeekend = dataReader["VtWorkTimeWeekend"].ToString();
        //            py.IssueWorkTime = dataReader["IssueWorkTime"].ToString();
        //            py.IssueWorkTimeWeekend = dataReader["IssueWorkTimeWeekend"].ToString();
        //            py.TicketSpeed = dataReader["TicketSpeed"].ToString();
        //            py.FlightCycle = dataReader["FlightCycle"].ToString();
        //            py.PolicyType = dataReader["PolicyType"].ToString();
        //            py.PsgType = int.Parse(dataReader["PsgType"].ToString());
        //            py.Param1 = dataReader["Param1"].ToString();
        //            py.Param2 = dataReader["Param2"].ToString();
        //            py.Param3 = dataReader["Param3"].ToString();
        //            py.Param4 = dataReader["Param4"].ToString();
        //            py.PolicyStatus = int.Parse(dataReader["PolicyStatus"].ToString());
        //            lstPolicy.Add(py);
        //            #endregion
        //        }
        //        dataReader.Close();
        //        dataReader = null;
        //        return lstPolicy;
        //    }
        //    catch (Exception ex)
        //    {
        //        return lstPolicy;
        //    }
        //}
        //#endregion

        //#region 获取增量删除政策数据
        //public List<Policies> LoadIncrementalDeletePolicy(PolicyRecord policyRec)
        //{
        //    List<Policies> lstPolicy = new List<Policies>();
        //    int pageSize = 100;
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        string OrderBy = "  ORDER BY ps.id asc ";

        //        #region 要查询的字段
        //        List<string> fileds = new List<string> {
        //        "ps.Id",//政策id
        //        "ps.UpdateTime",//获取供货商的更新时间
        //        "ps.PartnerId",//供应商id
        //        "ps.PartnerName",//供应商名称
        //        "ps.PartnerPolicyId",//供应商政策id
        //        "pd.SrcType",
        //        "pd.CommisionType",
        //        "pd.Comment",
        //        "pd.AirlineCode",
        //        "pd.DptCity",
        //        "pd.ArrCity",
        //        "pd.FlightIn",
        //        "pd.FlightOut",
        //        "pd.Seat",
        //        "pd.SaleEffectDate",
        //        "pd.SaleExpireDate",
        //        "pd.SaleForbidEffectDate",
        //        "pd.SaleForbidExpireDate",
        //        "pd.FlightEffectDate",
        //        "pd.FlightExpireDate",
        //        "pd.FlightForbidEffectDate",
        //        "pd.FlightForbidExpireDate",
        //        "pd.EarliestIssueDays",
        //        "pd.IsFitChild",
        //        "pd.CommisionPoint",
        //        "pd.PlatformCommisionPoint",
        //        "pd.CommisionMoney",
        //        "pd.PlatformCommisionMoney",
        //        "pd.isSetPrivate",
        //        "pd.PrivateCount",
        //        "pd.OfficeNo",
        //        "pd.NeedSwitchPNR",
        //        "pd.IsAutoIssue",
        //        "pd.IsPata",
        //        "pd.BigClientCode",
        //        "pd.MinimumTraveller",
        //        "pd.IsProviderScore",
        //        "pd.IsSharingFlight",
        //        "pd.InvoiceType",
        //        "pd.TuiGaiRule",
        //        "pd.ChangeWorkTime",
        //        "pd.ReturnWorkTime",
        //        "pd.VtWorkTime",
        //        "pd.ChangeWorkTimeWeekend",
        //        "pd.ReturnWorkTimeWeekend",
        //        "pd.VtWorkTimeWeekend",
        //        "pd.IssueWorkTime",
        //        "pd.IssueWorkTimeWeekend",
        //        "pd.TicketSpeed",
        //        "pd.FlightCycle",
        //        "pd.PolicyType",
        //        "pd.PsgType",
        //        "pd.Param1",
        //        "pd.Param2",
        //        "pd.Param3",
        //        "pd.Param4",
        //        "pd.PolicyStatus",
        //        "pd.PlatformPolicyStatus"
        //    };
        //        #endregion

        //        strSql.Append("select top " + pageSize.ToString() + " " + String.Join(",", fileds.ToArray()) + " from ndFlightSlave.PolicyDetail as pd inner join ndFlightSlave.PolicySyncRec as ps on pd.PartnerPolicyId = ps.PartnerPolicyId");
        //        strSql.Append(" where  ps.DelDegree=0 and pd.DelDegree=0 and ps.PlatformPolicyStatus=1 and pd.PlatformPolicyStatus=1 ");
        //        strSql.Append(" and (ps.UpdateTime > " + policyRec.LastUpdateTime + " or (ps.UpdateTime = " + policyRec.LastUpdateTime + " and ps.Id > " + policyRec.LastPolicyId + "))");
        //        strSql.Append(OrderBy);
        //        SqlDataReader dataReader = DbHelperSQL.ExecuteReader(strSql.ToString());

        //        while (dataReader.Read())
        //        {
        //            #region 封装实体
        //            Policies py = new Policies();
        //            py.PolicyId = long.Parse(dataReader["PolicyId"].ToString());
        //            py.Id = long.Parse(dataReader["Id"].ToString());
        //            py.UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"].ToString());
        //            py.PartnerId = int.Parse(dataReader["PartnerId"].ToString());
        //            py.PartnerName = dataReader["PartnerName"].ToString();
        //            py.PartnerPolicyId = dataReader["PartnerPolicyId"].ToString();
        //            py.SrcType = int.Parse(dataReader["SrcType"].ToString());
        //            py.CommisionType = int.Parse(dataReader["CommisionType"].ToString());
        //            py.Comment = dataReader["Comment"].ToString();
        //            py.AirlineCode = dataReader["AirlineCode"].ToString();
        //            py.DptCity = dataReader["DptCity"].ToString();
        //            py.ArrCity = dataReader["ArrCity"].ToString();
        //            py.FlightIn = dataReader["FlightIn"].ToString();
        //            py.FlightOut = dataReader["FlightOut"].ToString();
        //            py.Seat = dataReader["Seat"].ToString();
        //            py.SaleEffectDate = Convert.ToDateTime(dataReader["SaleEffectDate"].ToString());
        //            py.SaleExpireDate = Convert.ToDateTime(dataReader["SaleExpireDate"].ToString());
        //            py.SaleForbidEffectDate = Convert.ToDateTime(dataReader["SaleForbidEffectDate"].ToString());
        //            py.SaleForbidExpireDate = Convert.ToDateTime(dataReader["SaleForbidExpireDate"].ToString());
        //            py.FlightEffectDate = Convert.ToDateTime(dataReader["FlightEffectDate"].ToString());
        //            py.FlightExpireDate = Convert.ToDateTime(dataReader["FlightExpireDate"].ToString());
        //            py.FlightForbidEffectDate = Convert.ToDateTime(dataReader["FlightForbidEffectDate"].ToString());
        //            py.FlightForbidExpireDate = Convert.ToDateTime(dataReader["FlightForbidExpireDate"].ToString());
        //            py.EarliestIssueDays = int.Parse(dataReader["EarliestIssueDays"].ToString());
        //            py.IsFitChild = int.Parse(dataReader["IsFitChild"].ToString());
        //            py.CommisionPoint = Convert.ToDecimal(dataReader["CommisionPoint"].ToString());
        //            py.PlatformCommisionPoint = Convert.ToDecimal(dataReader["PlatformCommisionPoint"].ToString());
        //            py.CommisionMoney = Convert.ToDecimal(dataReader["CommisionMoney"].ToString());
        //            py.PlatformCommisionMoney = Convert.ToDecimal(dataReader["PlatformCommisionMoney"].ToString());
        //            py.isSetPrivate = int.Parse(dataReader["isSetPrivate"].ToString());
        //            py.PrivateCount = int.Parse(dataReader["PrivateCount"].ToString());
        //            py.OfficeNo = dataReader["OfficeNo"].ToString();
        //            py.NeedSwitchPNR = int.Parse(dataReader["NeedSwitchPNR"].ToString());
        //            py.IsAutoIssue = int.Parse(dataReader["IsAutoIssue"].ToString());
        //            py.IsPata = int.Parse(dataReader["IsPata"].ToString());
        //            py.BigClientCode = dataReader["BigClientCode"].ToString();
        //            py.MinimumTraveller = int.Parse(dataReader["MinimumTraveller"].ToString());
        //            py.IsProviderScore = int.Parse(dataReader["IsProviderScore"].ToString());
        //            py.IsSharingFlight = int.Parse(dataReader["IsSharingFlight"].ToString());
        //            py.InvoiceType = int.Parse(dataReader["InvoiceType"].ToString());
        //            py.TuiGaiRule = dataReader["TuiGaiRule"].ToString();
        //            py.ChangeWorkTime = dataReader["ChangeWorkTime"].ToString();
        //            py.ReturnWorkTime = dataReader["ReturnWorkTime"].ToString();
        //            py.VtWorkTime = dataReader["VtWorkTime"].ToString();
        //            py.ChangeWorkTimeWeekend = dataReader["ChangeWorkTimeWeekend"].ToString();
        //            py.ReturnWorkTimeWeekend = dataReader["ReturnWorkTimeWeekend"].ToString();
        //            py.VtWorkTimeWeekend = dataReader["VtWorkTimeWeekend"].ToString();
        //            py.IssueWorkTime = dataReader["IssueWorkTime"].ToString();
        //            py.IssueWorkTimeWeekend = dataReader["IssueWorkTimeWeekend"].ToString();
        //            py.TicketSpeed = dataReader["TicketSpeed"].ToString();
        //            py.FlightCycle = dataReader["FlightCycle"].ToString();
        //            py.PolicyType = dataReader["PolicyType"].ToString();
        //            py.PsgType = int.Parse(dataReader["PsgType"].ToString());
        //            py.Param1 = dataReader["Param1"].ToString();
        //            py.Param2 = dataReader["Param2"].ToString();
        //            py.Param3 = dataReader["Param3"].ToString();
        //            py.Param4 = dataReader["Param4"].ToString();
        //            py.PolicyStatus = int.Parse(dataReader["PolicyStatus"].ToString());
        //            // py.PlatformPolicyStatus = int.Parse(dataReader["PlatformPolicyStatus"].ToString());
        //            lstPolicy.Add(py);
        //            #endregion
        //        }
        //        return lstPolicy;
        //    }
        //    catch (Exception ex)
        //    {
        //        return lstPolicy;
        //    }
        //}
        //#endregion 
        #endregion

        #region 批量软删除政策
        public void BatchDelPolicy(string[] arryPolicyId91e)
        {
            try
            {
                string strSyncSQL = "Update DelDegree=0,UpdateTime=getDate() FROM dbo.PolicySyncRec WHERE PartnerPolicyId IN ('" + String.Join("','", arryPolicyId91e) + "')";
                string strDetailSQL = "Update DelDegree=0 FROM dbo.PolicyDetail WHERE PartnerPolicyId IN ('" + String.Join("','", arryPolicyId91e) + "')";
                List<string> lstSqls = new List<string>();
                lstSqls.Add(strSyncSQL);
                lstSqls.Add(strDetailSQL);
                DbHelperSQL.ExecuteSqlTran(lstSqls);
            }
            catch (Exception ex)
            {
                string path = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.ReceivePolicyService\\Err_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.WriteAllText(path, "批量删除数据异常:" + JsonConvert.SerializeObject(ex));
            }

        }
        #endregion

        #region 添加数据
        public bool Add(PolicySyncRec syncRec, PolicyDetail model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" declare @PId BIGINT ");
                strSql.Append(" insert into PolicySyncRec(");
                strSql.Append(" UpdateTime,PartnerId,PartnerName,PartnerPolicyId)");
                strSql.Append(" values (");
                strSql.Append(" @UpdateTime,@PartnerId,@PartnerName,@PartnerPolicyId)");
                strSql.Append(";Set @PId=@@IDENTITY ");

                strSql.Append(" insert into PolicyDetail(");
                strSql.Append("PolicyId,SrcType,CommisionType,Comment,AirlineCode,DptCity,ArrCity,FlightIn,FlightOut,Seat,SaleEffectDate,SaleExpireDate,SaleForbidEffectDate,SaleForbidExpireDate,FlightEffectDate,FlightExpireDate,FlightForbidEffectDate,FlightForbidExpireDate,EarliestIssueDays,IsFitChild,CommisionPoint,CommisionMoney,isSetPrivate,PrivateCount,OfficeNo,NeedSwitchPNR,IsAutoIssue,IsPata,BigClientCode,MinimumTraveller,IsProviderScore,IsSharingFlight,InvoiceType,TuiGaiRule,ChangeWorkTime,ReturnWorkTime,VtWorkTime,ChangeWorkTimeWeekend,ReturnWorkTimeWeekend,VtWorkTimeWeekend,IssueWorkTime,IssueWorkTimeWeekend,TicketSpeed,FlightCycle,PolicyType,PsgType,Param1,Param2,Param3,Param4,PolicyStatus,CreateTime,DelDegree,PartnerPolicyId,PlatformCommisionPoint,PlatformCommisionMoney)");
                strSql.Append(" values (");
                strSql.Append("@PId,@SrcType,@CommisionType,@Comment,@AirlineCode,@DptCity,@ArrCity,@FlightIn,@FlightOut,@Seat,@SaleEffectDate,@SaleExpireDate,@SaleForbidEffectDate,@SaleForbidExpireDate,@FlightEffectDate,@FlightExpireDate,@FlightForbidEffectDate,@FlightForbidExpireDate,@EarliestIssueDays,@IsFitChild,@CommisionPoint,@CommisionMoney,@isSetPrivate,@PrivateCount,@OfficeNo,@NeedSwitchPNR,@IsAutoIssue,@IsPata,@BigClientCode,@MinimumTraveller,@IsProviderScore,@IsSharingFlight,@InvoiceType,@TuiGaiRule,@ChangeWorkTime,@ReturnWorkTime,@VtWorkTime,@ChangeWorkTimeWeekend,@ReturnWorkTimeWeekend,@VtWorkTimeWeekend,@IssueWorkTime,@IssueWorkTimeWeekend,@TicketSpeed,@FlightCycle,@PolicyType,@PsgType,@Param1,@Param2,@Param3,@Param4,@PolicyStatus,@CreateTime,@DelDegree,@PartnerPolicyId,@PlatformCommisionPoint,@PlatformCommisionMoney)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
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
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PartnerId", SqlDbType.Int,4),
					new SqlParameter("@PartnerName", SqlDbType.NVarChar,50),
					new SqlParameter("@PartnerPolicyId", SqlDbType.VarChar,100),
                    new SqlParameter("@PlatformCommisionPoint", SqlDbType.Decimal,4),
                    new SqlParameter("@PlatformCommisionMoney", SqlDbType.Decimal,4)
                                        };

                parameters[0].Value = model.SrcType;
                parameters[1].Value = model.CommisionType;
                parameters[2].Value = model.Comment;
                parameters[3].Value = model.AirlineCode;
                parameters[4].Value = model.DptCity;
                parameters[5].Value = model.ArrCity;
                parameters[6].Value = model.FlightIn;
                parameters[7].Value = model.FlightOut;
                parameters[8].Value = model.Seat;
                parameters[9].Value = model.SaleEffectDate;
                parameters[10].Value = model.SaleExpireDate;
                parameters[11].Value = model.SaleForbidEffectDate;
                parameters[12].Value = model.SaleForbidExpireDate;
                parameters[13].Value = model.FlightEffectDate;
                parameters[14].Value = model.FlightExpireDate;
                parameters[15].Value = model.FlightForbidEffectDate;
                parameters[16].Value = model.FlightForbidExpireDate;
                parameters[17].Value = model.EarliestIssueDays;
                parameters[18].Value = model.IsFitChild;
                parameters[19].Value = model.CommisionPoint;
                parameters[20].Value = model.CommisionMoney;
                parameters[21].Value = model.isSetPrivate;
                parameters[22].Value = model.PrivateCount;
                parameters[23].Value = model.OfficeNo;
                parameters[24].Value = model.NeedSwitchPNR;
                parameters[25].Value = model.IsAutoIssue;
                parameters[26].Value = model.IsPata;
                parameters[27].Value = model.BigClientCode;
                parameters[28].Value = model.MinimumTraveller;
                parameters[29].Value = model.IsProviderScore;
                parameters[30].Value = model.IsSharingFlight;
                parameters[31].Value = model.InvoiceType;
                parameters[32].Value = model.TuiGaiRule;
                parameters[33].Value = model.ChangeWorkTime;
                parameters[34].Value = model.ReturnWorkTime;
                parameters[35].Value = model.VtWorkTime;
                parameters[36].Value = model.ChangeWorkTimeWeekend;
                parameters[37].Value = model.ReturnWorkTimeWeekend;
                parameters[38].Value = model.VtWorkTimeWeekend;
                parameters[39].Value = model.IssueWorkTime;
                parameters[40].Value = model.IssueWorkTimeWeekend;
                parameters[41].Value = model.TicketSpeed;
                parameters[42].Value = model.FlightCycle;
                parameters[43].Value = model.PolicyType;
                parameters[44].Value = model.PsgType;
                parameters[45].Value = model.Param1;
                parameters[46].Value = model.Param2;
                parameters[47].Value = model.Param3;
                parameters[48].Value = model.Param4;
                parameters[49].Value = model.PolicyStatus;
                parameters[50].Value = model.CreateTime;
                parameters[51].Value = model.DelDegree;
                parameters[52].Value = DateTime.Now;
                parameters[53].Value = syncRec.PartnerId;
                parameters[54].Value = syncRec.PartnerName;
                parameters[55].Value = syncRec.PartnerPolicyId;
                parameters[56].Value = model.PlatformCommisionPoint;
                parameters[57].Value = model.PlatformCommisionMoney;
                Hashtable ht = new Hashtable();
                ht.Add(strSql.ToString(), parameters);
                DbHelperSQL.ExecuteSqlTran(ht);
                return true;
            }
            catch (Exception ex)
            {

                string path = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.ReceivePolicyService\\Err_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.WriteAllText(path, "添加数据异常:" + JsonConvert.SerializeObject(ex));
                return false;
            }
        }
        #endregion

        #region 从去哪儿表中获取政策
        public List<Policies> LoadQunarPolicy(int pageSize, PolicyRecord policyRec, string whereCondition, PurchaserType purchaser, ref string selectSql,ref int totalCount, UploadType upLoadType, decimal commisionMoney=0,decimal commmisionPoint=0)
        {
            List<Policies> lstPolicy = new List<Policies>();
           
            //string dbName = purchaserType == PurchaserType.Qunar ? "qunarPolicySlave" : (purchaserType == PurchaserType.TaoBao ? "taobaoPolicySlave" : "ndFlight");
          
            string tableName = purchaser.ToString() + "Policy";
            try
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();
                string OrderBy = "  ORDER BY UpdateTime,Id ";

                #region 要查询的字段
                List<string> fileds = new List<string> {
                "Id ",//政策id
                "CONVERT(varchar(100), UpdateTime, 121) as UpdateTime",//获取供货商的更新时间
                "PartnerId",//供应商id
                "PartnerName",//供应商名称
                "PartnerPolicyId",//供应商政策id
                "SrcType",
                "PolicyId",
                "CommisionType",
                "Comment",
                "AirlineCode",
                "DptCity",
                "ArrCity",
                "FlightIn",
                "FlightOut",
                "Seat",
                "SaleEffectDate",
                "SaleExpireDate",
                "SaleForbidEffectDate",
                "SaleForbidExpireDate",
                "FlightEffectDate",
                "FlightExpireDate",
                "FlightForbidEffectDate",
                "FlightForbidExpireDate",
                "EarliestIssueDays",
                "IsFitChild",
                "CommisionPoint",
                "PlatformCommisionPoint",
                "CommisionMoney",
                "PlatformCommisionMoney",
                "isSetPrivate",
                "PrivateCount",
                "OfficeNo",
                "NeedSwitchPNR",
                "IsAutoIssue",
                "IsPata",
                "BigClientCode",
                "MinimumTraveller",
                "IsProviderScore",
                "IsSharingFlight",
                "InvoiceType",
                "TuiGaiRule",
                "ChangeWorkTime",
                "ReturnWorkTime",
                "VtWorkTime",
                "ChangeWorkTimeWeekend",
                "ReturnWorkTimeWeekend",
                "VtWorkTimeWeekend",
                "IssueWorkTime",
                "IssueWorkTimeWeekend",
                "TicketSpeed",
                "FlightCycle",
                "PolicyType",
                "PsgType",
                "Param1",
                "Param2",
                "Param3",
                "Param4",
                "PolicyStatus",
                "PlatformPolicyStatus",
                "DelDegree",
                "IsUpload"
            };
                #endregion

                //strSql.Append("select top "+pageSize.ToString()+" "+ String.Join(",", fileds.ToArray())+" from "+dbName+".dbo.PolicyDetail as pd inner join "+dbName+".dbo.PolicySyncRec as ps on pd.PartnerPolicyId = ps.PartnerPolicyId ");
                strSql.Append("select top " + pageSize.ToString() + "  " + String.Join(",", fileds.ToArray()) + " from "  + tableName + " ");
                strSql.Append(" where PlatformPolicyStatus = 1 and  SaleEffectDate <= getDate() and SaleExpireDate >= getDate()");
                if (upLoadType == UploadType.FullUpload)
                {
                    strSql.Append(" and DelDegree=1 ");
                }
                //else
                //{
                //    strSql.Append(" and DelDegree=1 ");
                //}
                strSql.Append(" and (UpdateTime > '" + policyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' or (UpdateTime = '" + policyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' and Id > " + policyRec.LastPolicyId + "))");
                if (!string.IsNullOrEmpty(whereCondition))
                {
                    strSql.Append(" and " + whereCondition);
                }
                strSql.Append(OrderBy);

                selectSql = strSql.ToString();
                //string fds= String.Join(",", fileds.ToArray());
                //string excuteSql = "select * from (select " + String.Join(",", fileds.ToArray()) + ", ROW_NUMBER() OVER(" + OrderBy + ") AS rowNo FROM " + strTable.ToString() + strWhere.ToString() + ")  AS T WHERE rowNo BETWEEN 1 AND 20";
               
                SqlDataReader dataReader = DbHelperSQL.ExecuteReader(strSql.ToString());

                while (dataReader.Read())
                {
                    #region 封装实体
                    Policies py = new Policies();
                    py.Id = long.Parse(dataReader["Id"].ToString());
                    py.PolicyId = long.Parse(dataReader["PolicyId"].ToString());
                    py.UpdateTime = Convert.ToDateTime(Convert.ToDateTime(dataReader["UpdateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    py.PartnerId = int.Parse(dataReader["PartnerId"].ToString());
                    py.PartnerName = dataReader["PartnerName"].ToString();
                    py.PartnerPolicyId = dataReader["PartnerPolicyId"].ToString();
                    py.SrcType = int.Parse(dataReader["SrcType"].ToString());
                    py.CommisionType = int.Parse(dataReader["CommisionType"].ToString());
                    py.Comment = dataReader["Comment"].ToString();
                    py.AirlineCode = dataReader["AirlineCode"].ToString();
                    py.DptCity = dataReader["DptCity"].ToString();
                    py.ArrCity = dataReader["ArrCity"].ToString();
                    py.FlightIn = dataReader["FlightIn"].ToString();
                    py.FlightOut = dataReader["FlightOut"].ToString();
                    py.Seat = dataReader["Seat"].ToString();
                    py.SaleEffectDate = Convert.ToDateTime(dataReader["SaleEffectDate"].ToString());
                    py.SaleExpireDate = Convert.ToDateTime(dataReader["SaleExpireDate"].ToString());
                    py.SaleForbidEffectDate = Convert.ToDateTime(dataReader["SaleForbidEffectDate"].ToString());
                    py.SaleForbidExpireDate = Convert.ToDateTime(dataReader["SaleForbidExpireDate"].ToString());
                    py.FlightEffectDate = Convert.ToDateTime(dataReader["FlightEffectDate"].ToString());
                    py.FlightExpireDate = Convert.ToDateTime(dataReader["FlightExpireDate"].ToString());
                    py.FlightForbidEffectDate = Convert.ToDateTime(dataReader["FlightForbidEffectDate"].ToString());
                    py.FlightForbidExpireDate = Convert.ToDateTime(dataReader["FlightForbidExpireDate"].ToString());
                    py.EarliestIssueDays = int.Parse(dataReader["EarliestIssueDays"].ToString());
                    py.IsFitChild = int.Parse(dataReader["IsFitChild"].ToString());
                    py.CommisionPoint = Convert.ToDecimal(dataReader["CommisionPoint"].ToString())+commmisionPoint;
                    py.PlatformCommisionPoint = py.CommisionPoint;
                    py.CommisionMoney = Convert.ToDecimal(dataReader["CommisionMoney"].ToString()) + commisionMoney;
                    py.PlatformCommisionMoney = py.CommisionMoney;
                    py.isSetPrivate = int.Parse(dataReader["isSetPrivate"].ToString());
                    py.PrivateCount = int.Parse(dataReader["PrivateCount"].ToString());
                    py.OfficeNo = dataReader["OfficeNo"].ToString();
                    py.NeedSwitchPNR = int.Parse(dataReader["NeedSwitchPNR"].ToString());
                    py.IsAutoIssue = int.Parse(dataReader["IsAutoIssue"].ToString());
                    py.IsPata = int.Parse(dataReader["IsPata"].ToString());
                    py.BigClientCode = dataReader["BigClientCode"].ToString();
                    py.MinimumTraveller = int.Parse(dataReader["MinimumTraveller"].ToString());
                    py.IsProviderScore = int.Parse(dataReader["IsProviderScore"].ToString());
                    py.IsSharingFlight = int.Parse(dataReader["IsSharingFlight"].ToString());
                    py.InvoiceType = int.Parse(dataReader["InvoiceType"].ToString());
                    py.TuiGaiRule = dataReader["TuiGaiRule"].ToString();
                    py.ChangeWorkTime = dataReader["ChangeWorkTime"].ToString();
                    py.ReturnWorkTime = dataReader["ReturnWorkTime"].ToString();
                    py.VtWorkTime = dataReader["VtWorkTime"].ToString();
                    py.ChangeWorkTimeWeekend = dataReader["ChangeWorkTimeWeekend"].ToString();
                    py.ReturnWorkTimeWeekend = dataReader["ReturnWorkTimeWeekend"].ToString();
                    py.VtWorkTimeWeekend = dataReader["VtWorkTimeWeekend"].ToString();
                    py.IssueWorkTime = dataReader["IssueWorkTime"].ToString();
                    py.IssueWorkTimeWeekend = dataReader["IssueWorkTimeWeekend"].ToString();
                    py.TicketSpeed = dataReader["TicketSpeed"].ToString();
                    py.FlightCycle = dataReader["FlightCycle"].ToString();
                    py.PolicyType = dataReader["PolicyType"].ToString();
                    py.PsgType = int.Parse(dataReader["PsgType"].ToString());
                    py.Param1 = dataReader["Param1"].ToString();
                    py.Param2 = dataReader["Param2"].ToString();
                    py.Param3 = dataReader["Param3"].ToString();
                    py.Param4 = dataReader["Param4"].ToString();
                    py.PolicyStatus = int.Parse(dataReader["PolicyStatus"].ToString());
                    py.DelDegree = int.Parse(dataReader["DelDegree"].ToString());
                    py.IsUpload = int.Parse(dataReader["IsUpload"].ToString());
                    //py.PlatformPolicyStatus = int.Parse(dataReader["PlatformPolicyStatus"].ToString());
                    lstPolicy.Add(py);
                    #endregion
                }
                dataReader.Close();
                dataReader = null;
                string strCount = strSql.ToString().Replace("top " + pageSize.ToString() + "  " + String.Join(",", fileds.ToArray()), "count(*) as rt ").Replace(OrderBy,"");
                 totalCount =Convert.ToInt32(DbHelperSQL.ExecuteScalar(strCount));
                
                return lstPolicy;
            }
            catch (Exception ex)
            {
                return lstPolicy;
            }
        }
        #endregion

        #region 查询供应商政策数据
        public List<Policies> LoadPolicy(PolicyRecord policyRec, decimal commisionMoney, decimal commsionPoint, int pageCount, string whereCondition, ref string selectSql, ref int totalCount, UploadType upLoadType,bool isSearchTotalCount=false,bool isUpload=false)
        {
            List<Policies> lstPolicy = new List<Policies>();
            int pageSize = pageCount;
            //string dbName = purchaserType == PurchaserType.Qunar ? "qunarPolicySlave" : (purchaserType == PurchaserType.TaoBao ? "taobaoPolicySlave" : "ndFlight");
           // string dbName = "ndFlightPolicySlave";
             string dbName = "ndFlightPolicySlave";
            try
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();
                string OrderBy = "  ORDER BY UpdateTime desc,ps.Id desc";

                #region 要查询的字段
                List<string> fileds = new List<string> {
                "ps.Id ",//政策id
                "CONVERT(varchar(100), ps.UpdateTime, 121) as UpdateTime",//获取供货商的更新时间
                "ps.PartnerId",//供应商id
                "ps.PartnerName",//供应商名称
                "ps.PartnerPolicyId",//供应商政策id
                "ps.DelDegree",//供应商政策id
                "pd.SrcType",
                "pd.PolicyId",
                "pd.CommisionType",
                "pd.Comment",
                "pd.AirlineCode",
                "pd.DptCity",
                "pd.ArrCity",
                "pd.FlightIn",
                "pd.FlightOut",
                "pd.Seat",
                "pd.SaleEffectDate",
                "pd.SaleExpireDate",
                "pd.SaleForbidEffectDate",
                "pd.SaleForbidExpireDate",
                "pd.FlightEffectDate",
                "pd.FlightExpireDate",
                "pd.FlightForbidEffectDate",
                "pd.FlightForbidExpireDate",
                "pd.EarliestIssueDays",
                "pd.IsFitChild",
                "pd.CommisionPoint",
                "pd.PlatformCommisionPoint",
                "pd.CommisionMoney",
                "pd.PlatformCommisionMoney",
                "pd.isSetPrivate",
                "pd.PrivateCount",
                "pd.OfficeNo",
                "pd.NeedSwitchPNR",
                "pd.IsAutoIssue",
                "pd.IsPata",
                "pd.BigClientCode",
                "pd.MinimumTraveller",
                "pd.IsProviderScore",
                "pd.IsSharingFlight",
                "pd.InvoiceType",
                "pd.TuiGaiRule",
                "pd.ChangeWorkTime",
                "pd.ReturnWorkTime",
                "pd.VtWorkTime",
                "pd.ChangeWorkTimeWeekend",
                "pd.ReturnWorkTimeWeekend",
                "pd.VtWorkTimeWeekend",
                "pd.IssueWorkTime",
                "pd.IssueWorkTimeWeekend",
                "pd.TicketSpeed",
                "pd.FlightCycle",
                "pd.PolicyType",
                "pd.PsgType",
                "pd.Param1",
                "pd.Param2",
                "pd.Param3",
                "pd.Param4",
                "pd.PolicyStatus",
                "pd.PlatformPolicyStatus",
                "ps.IsUpload"
            };
                #endregion

                //strSql.Append("select top "+pageSize.ToString()+" "+ String.Join(",", fileds.ToArray())+" from "+dbName+".dbo.PolicyDetail as pd inner join "+dbName+".dbo.PolicySyncRec as ps on pd.PartnerPolicyId = ps.PartnerPolicyId ");
                strSql.Append("select top " + pageSize.ToString() + "  " + String.Join(",", fileds.ToArray()) + " from " + dbName + ".dbo.PolicyDetail as pd inner join " + dbName + ".dbo.PolicySyncRec as ps on pd.PartnerPolicyId = ps.PartnerPolicyId ");
                strSql.Append(" where ps.PlatformPolicyStatus = 1   and pd.PolicyStatus=1 and pd.PlatformPolicyStatus=1  and pd.CommisionPoint >0 and  pd.SaleEffectDate <= getDate() and pd.SaleExpireDate >= getDate()");
               // strSql.Append(" and (ps.UpdateTime > '" + policyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' or (ps.UpdateTime = '" + policyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' and ps.Id > " + policyRec.LastPolicyId + "))");
                if(isUpload)//如果是上传
                {
                    if(upLoadType == UploadType.FullUpload)
                    {
                        strSql.Append(" and ps.DelDegree=1 and pd.DelDegree=1 ");
                    }

                    strSql.Append("  and ps.IsUpload=0 ");//and ps.UpdateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00" + "'
                }
                if (!string.IsNullOrEmpty(whereCondition))
                {
                    strSql.Append(" and " + whereCondition);
                }
                strSql.Append(OrderBy);

                selectSql = strSql.ToString();
                //string fds= String.Join(",", fileds.ToArray());
                //string excuteSql = "select * from (select " + String.Join(",", fileds.ToArray()) + ", ROW_NUMBER() OVER(" + OrderBy + ") AS rowNo FROM " + strTable.ToString() + strWhere.ToString() + ")  AS T WHERE rowNo BETWEEN 1 AND 20";

               
                SqlDataReader dataReader = DbHelperSQL.ExecuteReader(strSql.ToString());

                while (dataReader.Read())
                {
                    #region 封装实体
                    Policies py = new Policies();
                    py.Id = long.Parse(dataReader["Id"].ToString());
                    py.PolicyId = long.Parse(dataReader["PolicyId"].ToString());
                    py.UpdateTime = Convert.ToDateTime(Convert.ToDateTime(dataReader["UpdateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    py.PartnerId = int.Parse(dataReader["PartnerId"].ToString());
                    py.PartnerName = dataReader["PartnerName"].ToString();
                    py.PartnerPolicyId = dataReader["PartnerPolicyId"].ToString();
                    py.SrcType = int.Parse(dataReader["SrcType"].ToString());
                    py.CommisionType = int.Parse(dataReader["CommisionType"].ToString());
                    py.Comment = dataReader["Comment"].ToString();
                    py.AirlineCode = dataReader["AirlineCode"].ToString();
                    py.DptCity = dataReader["DptCity"].ToString();
                    py.ArrCity = dataReader["ArrCity"].ToString();
                    py.FlightIn = dataReader["FlightIn"].ToString();
                    py.FlightOut = dataReader["FlightOut"].ToString();
                    py.Seat = dataReader["Seat"].ToString();
                    py.SaleEffectDate = Convert.ToDateTime(dataReader["SaleEffectDate"].ToString());
                    py.SaleExpireDate = Convert.ToDateTime(dataReader["SaleExpireDate"].ToString());
                    py.SaleForbidEffectDate = Convert.ToDateTime(dataReader["SaleForbidEffectDate"].ToString());
                    py.SaleForbidExpireDate = Convert.ToDateTime(dataReader["SaleForbidExpireDate"].ToString());
                    py.FlightEffectDate = Convert.ToDateTime(dataReader["FlightEffectDate"].ToString());
                    py.FlightExpireDate = Convert.ToDateTime(dataReader["FlightExpireDate"].ToString());
                    py.FlightForbidEffectDate = Convert.ToDateTime(dataReader["FlightForbidEffectDate"].ToString());
                    py.FlightForbidExpireDate = Convert.ToDateTime(dataReader["FlightForbidExpireDate"].ToString());
                    py.EarliestIssueDays = int.Parse(dataReader["EarliestIssueDays"].ToString());
                    py.IsFitChild = int.Parse(dataReader["IsFitChild"].ToString());
                    py.CommisionPoint = Convert.ToDecimal(dataReader["CommisionPoint"].ToString());
                    py.PlatformCommisionPoint = py.CommisionPoint + commsionPoint;
                    py.CommisionMoney = Convert.ToDecimal(dataReader["CommisionMoney"].ToString());
                    py.PlatformCommisionMoney = py.CommisionMoney + commisionMoney;
                    py.isSetPrivate = int.Parse(dataReader["isSetPrivate"].ToString());
                    py.PrivateCount = int.Parse(dataReader["PrivateCount"].ToString());
                    py.OfficeNo = dataReader["OfficeNo"].ToString();
                    py.NeedSwitchPNR = int.Parse(dataReader["NeedSwitchPNR"].ToString());
                    py.IsAutoIssue = int.Parse(dataReader["IsAutoIssue"].ToString());
                    py.IsPata = int.Parse(dataReader["IsPata"].ToString());
                    py.BigClientCode = dataReader["BigClientCode"].ToString();
                    py.MinimumTraveller = int.Parse(dataReader["MinimumTraveller"].ToString());
                    py.IsProviderScore = int.Parse(dataReader["IsProviderScore"].ToString());
                    py.IsSharingFlight = int.Parse(dataReader["IsSharingFlight"].ToString());
                    py.InvoiceType = int.Parse(dataReader["InvoiceType"].ToString());
                    py.TuiGaiRule = dataReader["TuiGaiRule"].ToString();
                    py.ChangeWorkTime = dataReader["ChangeWorkTime"].ToString();
                    py.ReturnWorkTime = dataReader["ReturnWorkTime"].ToString();
                    py.VtWorkTime = dataReader["VtWorkTime"].ToString();
                    py.ChangeWorkTimeWeekend = dataReader["ChangeWorkTimeWeekend"].ToString();
                    py.ReturnWorkTimeWeekend = dataReader["ReturnWorkTimeWeekend"].ToString();
                    py.VtWorkTimeWeekend = dataReader["VtWorkTimeWeekend"].ToString();
                    py.IssueWorkTime = dataReader["IssueWorkTime"].ToString();
                    py.IssueWorkTimeWeekend = dataReader["IssueWorkTimeWeekend"].ToString();
                    py.TicketSpeed = dataReader["TicketSpeed"].ToString();
                    py.FlightCycle = dataReader["FlightCycle"].ToString();
                    py.PolicyType = dataReader["PolicyType"].ToString();
                    py.PsgType = int.Parse(dataReader["PsgType"].ToString());
                    py.Param1 = dataReader["Param1"].ToString();
                    py.Param2 = dataReader["Param2"].ToString();
                    py.Param3 = dataReader["Param3"].ToString();
                    py.Param4 = dataReader["Param4"].ToString();
                    py.PolicyStatus = int.Parse(dataReader["PolicyStatus"].ToString());
                    py.IsUpload = int.Parse(dataReader["IsUpload"].ToString());
                    py.DelDegree = int.Parse(dataReader["DelDegree"].ToString());
                    // py.IsUpload = int.Parse(dataReader["IsUpload"].ToString());
                    //py.PlatformPolicyStatus = int.Parse(dataReader["PlatformPolicyStatus"].ToString());
                    lstPolicy.Add(py);
                    #endregion
                }
                dataReader.Close();
                dataReader = null;
                if (isSearchTotalCount)
                {
                    string strCount = strSql.ToString().Replace("top " + pageSize.ToString() + "  " + String.Join(",", fileds.ToArray()), "count(*) as rt ").Replace(OrderBy, "");
                    totalCount = Convert.ToInt32(DbHelperSQL.ExecuteScalar(strCount));
                }
                return lstPolicy;
            }
            catch (Exception ex)
            {
                return lstPolicy;
            }
        }
        #endregion

  

        #region 批量更新政策为已上传
        public bool BlukyUpdatePolicyUploaded(List<string> lstPolicies)
        {
            //ArrayList arrPolicy = new ArrayList();
            //lstPolicies.ForEach(x=>{
            //arrPolicy.Add(x.Id);
            //});
            string sql = "USE ndFlightPolicy;update PolicySyncRec set IsUpload = 1 where id in (" + string.Join(",", lstPolicies.ToArray()) + ")";//UpdateTime='"+DateTime.Now.ToString("yyyy-MM-dd")+ " 0:00:00"+"'
           // File.WriteAllText("e://2.txt", sql);
            int r=DbHelperSQL.ExecuteSql(sql);
            if(r<=0)
            {
                return false;
            }
            return true;
        }
        #endregion
        #endregion
	}
}

