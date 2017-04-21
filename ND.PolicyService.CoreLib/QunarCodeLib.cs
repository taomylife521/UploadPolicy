using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.PolicyService.DbEntity;
using System.Collections.Generic;
namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:QunarCode
	/// </summary>
	public partial class QunarCodeLib
	{
        public QunarCodeLib()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from QunarCode");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ND.PolicyService.DbEntity.QunarCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QunarCode(");
			strSql.Append("Code)");
			strSql.Append(" values (");
			strSql.Append("@Code)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,50)};
			parameters[0].Value = model.Code;

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
		public bool Update(ND.PolicyService.DbEntity.QunarCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QunarCode set ");
			strSql.Append("Code=@Code");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,50),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.Id;

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
			strSql.Append("delete from QunarCode ");
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
			strSql.Append("delete from QunarCode ");
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
		public ND.PolicyService.DbEntity.QunarCode GetModel(long Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Code from QunarCode ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
			parameters[0].Value = Id;

			ND.PolicyService.DbEntity.QunarCode model=new ND.PolicyService.DbEntity.QunarCode();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.Code=ds.Tables[0].Rows[0]["Code"].ToString();
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
			strSql.Append("select Id,Code ");
			strSql.Append(" FROM QunarCode ");
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
			strSql.Append(" Id,Code ");
			strSql.Append(" FROM QunarCode ");
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
			parameters[0].Value = "QunarCode";
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
        public bool DeleteByCode(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QunarCode ");
            strSql.Append(" where Code=@Code");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar)
            };
            parameters[0].Value = code;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<QunarCode> GetModelList(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Id,Code from QunarCode where 1=1 ");
          if(!string.IsNullOrEmpty(code))
          {
              strSql.Append(" and Code = '" + code + "'");
          }


          List<QunarCode> lstModel = new List<QunarCode>();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ND.PolicyService.DbEntity.QunarCode model = new ND.PolicyService.DbEntity.QunarCode();
                if (ds.Tables[0].Rows[i]["Id"].ToString() != "")
                {
                    model.Id = long.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                }
                model.Code = ds.Tables[0].Rows[i]["Code"].ToString();
                lstModel.Add(model);
            }
            return lstModel;
           
        }
        #endregion
	}
}

