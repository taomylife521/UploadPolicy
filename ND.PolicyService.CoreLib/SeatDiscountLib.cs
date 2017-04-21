using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.PolicyService.DbEntity;
using System.Collections.Generic;

namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:SeatDiscount
	/// </summary>
	public partial class SeatDiscountLib
	{
        public SeatDiscountLib()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "SeatDiscount"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SeatDiscount");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ND.PolicyService.DbEntity.SeatDiscount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SeatDiscount(");
			strSql.Append("AirlineCode,Seat,Discount,IsEnabled,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@AirlineCode,@Seat,@Discount,@IsEnabled,@CreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AirlineCode", SqlDbType.VarChar,10),
					new SqlParameter("@Seat", SqlDbType.VarChar,10),
					new SqlParameter("@Discount", SqlDbType.VarChar,50),
					new SqlParameter("@IsEnabled", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.AirlineCode;
			parameters[1].Value = model.Seat;
			parameters[2].Value = model.Discount;
			parameters[3].Value = model.IsEnabled;
			parameters[4].Value = model.CreateTime;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ND.PolicyService.DbEntity.SeatDiscount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SeatDiscount set ");
			strSql.Append("AirlineCode=@AirlineCode,");
			strSql.Append("Seat=@Seat,");
			strSql.Append("Discount=@Discount,");
			strSql.Append("IsEnabled=@IsEnabled,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@AirlineCode", SqlDbType.VarChar,10),
					new SqlParameter("@Seat", SqlDbType.VarChar,10),
					new SqlParameter("@Discount", SqlDbType.VarChar,50),
					new SqlParameter("@IsEnabled", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.AirlineCode;
			parameters[1].Value = model.Seat;
			parameters[2].Value = model.Discount;
			parameters[3].Value = model.IsEnabled;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.id;

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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SeatDiscount ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SeatDiscount ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public ND.PolicyService.DbEntity.SeatDiscount GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,AirlineCode,Seat,Discount,IsEnabled,CreateTime from SeatDiscount ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			ND.PolicyService.DbEntity.SeatDiscount model=new ND.PolicyService.DbEntity.SeatDiscount();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.AirlineCode=ds.Tables[0].Rows[0]["AirlineCode"].ToString();
				model.Seat=ds.Tables[0].Rows[0]["Seat"].ToString();
				model.Discount=ds.Tables[0].Rows[0]["Discount"].ToString();
				if(ds.Tables[0].Rows[0]["IsEnabled"].ToString()!="")
				{
					model.IsEnabled=int.Parse(ds.Tables[0].Rows[0]["IsEnabled"].ToString());
				}
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
			strSql.Append("select id,AirlineCode,Seat,Discount,IsEnabled,CreateTime ");
			strSql.Append(" FROM SeatDiscount ");
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
			strSql.Append(" id,AirlineCode,Seat,Discount,IsEnabled,CreateTime ");
			strSql.Append(" FROM SeatDiscount ");
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
			parameters[0].Value = "SeatDiscount";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region extentd method
        public List<SeatDiscount> GetModelList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,AirlineCode,Seat,Discount,IsEnabled,CreateTime from SeatDiscount ");
            strSql.Append(" where IsEnabled=1");
         

         
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<SeatDiscount> lstSeats = new List<SeatDiscount>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ND.PolicyService.DbEntity.SeatDiscount model = new ND.PolicyService.DbEntity.SeatDiscount();
                    if (ds.Tables[0].Rows[i]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                    }
                    model.AirlineCode = ds.Tables[0].Rows[i]["AirlineCode"].ToString();
                    model.Seat = ds.Tables[0].Rows[i]["Seat"].ToString();
                    model.Discount = ds.Tables[0].Rows[i]["Discount"].ToString();
                    if (ds.Tables[0].Rows[i]["IsEnabled"].ToString() != "")
                    {
                        model.IsEnabled = int.Parse(ds.Tables[0].Rows[i]["IsEnabled"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CreateTime"].ToString() != "")         
                    {
                        model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[i]["CreateTime"].ToString());
                    }
                    lstSeats.Add(model);
               
            }
            return lstSeats;
        }
        #endregion
	}
}

