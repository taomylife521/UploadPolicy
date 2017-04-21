using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.PolicyService.CoreLib;
using ND.PolicyService.Enums;
using ND.PolicyService.DbEntity;//Please add references
namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:RealTimeUploadRecord
	/// </summary>
	public partial class RealTimeUploadRecordLib
	{
        public RealTimeUploadRecordLib()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RealTimeUploadRecord");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(RealTimeUploadRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RealTimeUploadRecord(");
			strSql.Append("Id,LastUpdateTime,IsLock,LockPerson,Remark,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@Id,@LastUpdateTime,@IsLock,@LockPerson,@Remark,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LastUpdateTime", SqlDbType.VarChar,50),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@LockPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.LastUpdateTime;
			parameters[2].Value = model.IsLock;
			parameters[3].Value = model.LockPerson;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.CreateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(RealTimeUploadRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RealTimeUploadRecord set ");
			strSql.Append("LastUpdateTime=@LastUpdateTime,");
			strSql.Append("IsLock=@IsLock,");
			strSql.Append("LockPerson=@LockPerson,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@LastUpdateTime", SqlDbType.VarChar,50),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@LockPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.LastUpdateTime;
			parameters[1].Value = model.IsLock;
			parameters[2].Value = model.LockPerson;
			parameters[3].Value = model.Remark;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.Id;

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
		public bool Delete(Guid Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RealTimeUploadRecord ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
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
			strSql.Append("delete from RealTimeUploadRecord ");
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
		public RealTimeUploadRecord GetModel(Guid Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,LastUpdateTime,IsLock,LockPerson,Remark,CreateTime from RealTimeUploadRecord ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = Id;

			RealTimeUploadRecord model=new RealTimeUploadRecord();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.LastUpdateTime=ds.Tables[0].Rows[0]["LastUpdateTime"].ToString();
				if(ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
				}
				model.LockPerson=ds.Tables[0].Rows[0]["LockPerson"].ToString();
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
			strSql.Append("select Id,LastUpdateTime,IsLock,LockPerson,Remark,CreateTime ");
			strSql.Append(" FROM RealTimeUploadRecord ");
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
			strSql.Append(" Id,LastUpdateTime,IsLock,LockPerson,Remark,CreateTime ");
			strSql.Append(" FROM RealTimeUploadRecord ");
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
			parameters[0].Value = "RealTimeUploadRecord";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region Extention Method
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RealTimeUploadRecord GetModel(PurchaserType purchaser)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,LastUpdateTime,IsLock,LockPerson,Remark,CreateTime from RealTimeUploadRecord  where  Purchaser = '" + purchaser.ToString() + "' order by CreateTime desc ");
            RealTimeUploadRecord model = new RealTimeUploadRecord();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.LastUpdateTime = ds.Tables[0].Rows[0]["LastUpdateTime"].ToString();
                if (ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
                }
                model.LockPerson = ds.Tables[0].Rows[0]["LockPerson"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public void DeleteModel(PurchaserType purchaser)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RealTimeUploadRecord  where Purchaser = '"+purchaser.ToString()+"' ");
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion
	}
}

