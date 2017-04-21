using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.PolicyService.CoreLib;
using ND.PolicyService.DbEntity;
using System.Collections.Generic;
namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:UploadPolicyRecord
	/// </summary>
	public partial class UploadPolicyRecordLib
	{
        public UploadPolicyRecordLib()
		{}
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UploadPolicyRecord");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(ND.PolicyService.DbEntity.UploadPolicyRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UploadPolicyRecord(");
            strSql.Append("Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType)");
            strSql.Append(" values (");
            strSql.Append("@Id,@UId,@PolicyId,@CommisionMoney,@CommsionPoint,@CreateTime,@UpdateTime,@UpdateSuccessCount,@UpdateFailedCount,@PartenerPolicyId,@PolicyType)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UId", SqlDbType.BigInt,8),
					new SqlParameter("@PolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
					new SqlParameter("@CommsionPoint", SqlDbType.Decimal,9),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateSuccessCount", SqlDbType.Int,4),
					new SqlParameter("@UpdateFailedCount", SqlDbType.Int,4),
					new SqlParameter("@PartenerPolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UId;
            parameters[2].Value = model.PolicyId;
            parameters[3].Value = model.CommisionMoney;
            parameters[4].Value = model.CommsionPoint;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.UpdateSuccessCount;
            parameters[8].Value = model.UpdateFailedCount;
            parameters[9].Value = model.PartenerPolicyId;
            parameters[10].Value = model.PolicyType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ND.PolicyService.DbEntity.UploadPolicyRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UploadPolicyRecord set ");
            strSql.Append("UId=@UId,");
            strSql.Append("PolicyId=@PolicyId,");
            strSql.Append("CommisionMoney=@CommisionMoney,");
            strSql.Append("CommsionPoint=@CommsionPoint,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateSuccessCount=@UpdateSuccessCount,");
            strSql.Append("UpdateFailedCount=@UpdateFailedCount,");
            strSql.Append("PartenerPolicyId=@PartenerPolicyId,");
            strSql.Append("PolicyType=@PolicyType");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@UId", SqlDbType.BigInt,8),
					new SqlParameter("@PolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
					new SqlParameter("@CommsionPoint", SqlDbType.Decimal,9),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateSuccessCount", SqlDbType.Int,4),
					new SqlParameter("@UpdateFailedCount", SqlDbType.Int,4),
					new SqlParameter("@PartenerPolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.VarChar,50),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.UId;
            parameters[1].Value = model.PolicyId;
            parameters[2].Value = model.CommisionMoney;
            parameters[3].Value = model.CommsionPoint;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.UpdateSuccessCount;
            parameters[7].Value = model.UpdateFailedCount;
            parameters[8].Value = model.PartenerPolicyId;
            parameters[9].Value = model.PolicyType;
            parameters[10].Value = model.Id;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UploadPolicyRecord ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = Id;

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
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UploadPolicyRecord ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public ND.PolicyService.DbEntity.UploadPolicyRecord GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType from UploadPolicyRecord ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier)};
            parameters[0].Value = Id;

            ND.PolicyService.DbEntity.UploadPolicyRecord model = new ND.PolicyService.DbEntity.UploadPolicyRecord();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UId"].ToString() != "")
                {
                    model.UId = long.Parse(ds.Tables[0].Rows[0]["UId"].ToString());
                }
                model.PolicyId = ds.Tables[0].Rows[0]["PolicyId"].ToString();
                if (ds.Tables[0].Rows[0]["CommisionMoney"].ToString() != "")
                {
                    model.CommisionMoney = decimal.Parse(ds.Tables[0].Rows[0]["CommisionMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommsionPoint"].ToString() != "")
                {
                    model.CommsionPoint = decimal.Parse(ds.Tables[0].Rows[0]["CommsionPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateSuccessCount"].ToString() != "")
                {
                    model.UpdateSuccessCount = int.Parse(ds.Tables[0].Rows[0]["UpdateSuccessCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateFailedCount"].ToString() != "")
                {
                    model.UpdateFailedCount = int.Parse(ds.Tables[0].Rows[0]["UpdateFailedCount"].ToString());
                }
                model.PartenerPolicyId = ds.Tables[0].Rows[0]["PartenerPolicyId"].ToString();
                model.PolicyType = ds.Tables[0].Rows[0]["PolicyType"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType ");
            strSql.Append(" FROM UploadPolicyRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType ");
            strSql.Append(" FROM UploadPolicyRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "UploadPolicyRecord";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region extention Method

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public string Add(UploadPolicyRecord model, bool isExcute = true)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UploadPolicyRecord(");
            strSql.Append("Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType)");
            strSql.Append(" values (");
            strSql.Append(" '" + model.Id + "',");
            strSql.Append(" '" + model.UId + "',");
            strSql.Append(" '" + model.PolicyId + "',");
            strSql.Append(" '" + model.CommisionMoney + "',");
            strSql.Append(" '" + model.CommsionPoint + "',");
            strSql.Append(" '" + model.CreateTime + "',");
            strSql.Append(" '" + model.UpdateTime + "',");
            strSql.Append(" '" + model.UpdateSuccessCount + "',");
            strSql.Append(" '" + model.UpdateFailedCount + "',");
            strSql.Append(" '" + model.PartenerPolicyId + "',");
            strSql.Append(" '" + model.PolicyType + "')");
            //strSql.Append("@Id,@UId,@PolicyId,@CommisionMoney,@CommsionPoint,@CreateTime)");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
            //        new SqlParameter("@UId", SqlDbType.BigInt,8),
            //        new SqlParameter("@PolicyId", SqlDbType.VarChar,50),
            //        new SqlParameter("@CommisionMoney", SqlDbType.Decimal,9),
            //        new SqlParameter("@CommsionPoint", SqlDbType.Decimal,9),
            //        new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            //parameters[0].Value = model.Id;
            //parameters[1].Value = model.UId;
            //parameters[2].Value = model.PolicyId;
            //parameters[3].Value = model.CommisionMoney;
            //parameters[4].Value = model.CommsionPoint;
            //parameters[5].Value = model.CreateTime;

            if (!isExcute)
            {

                return strSql.ToString();
            }
            int r = DbHelperSQL.ExecuteSql(strSql.ToString());
            return r.ToString();
        } 
        #endregion

        #region 获得数据列表 GetModelList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UploadPolicyRecord> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType ");
            strSql.Append(" FROM UploadPolicyRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UploadPolicyRecord> lstUpload = new List<UploadPolicyRecord>();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                UploadPolicyRecord model = new UploadPolicyRecord();
                if (ds.Tables[0].Rows[i]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[i]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[i]["UId"].ToString() != "")
                {
                    model.UId = long.Parse(ds.Tables[0].Rows[i]["UId"].ToString());
                }
                model.PolicyId = ds.Tables[0].Rows[0]["PolicyId"].ToString();
                if (ds.Tables[0].Rows[i]["CommisionMoney"].ToString() != "")
                {
                    model.CommisionMoney = decimal.Parse(ds.Tables[0].Rows[i]["CommisionMoney"].ToString());
                }
                if (ds.Tables[0].Rows[i]["CommsionPoint"].ToString() != "")
                {
                    model.CommsionPoint = decimal.Parse(ds.Tables[0].Rows[i]["CommsionPoint"].ToString());
                }
                if (ds.Tables[0].Rows[i]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[i]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[i]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[i]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[i]["UpdateSuccessCount"].ToString() != "")
                {
                    model.UpdateSuccessCount = int.Parse(ds.Tables[0].Rows[i]["UpdateSuccessCount"].ToString());
                }
                if (ds.Tables[0].Rows[i]["UpdateFailedCount"].ToString() != "")
                {
                    model.UpdateFailedCount = int.Parse(ds.Tables[0].Rows[i]["UpdateFailedCount"].ToString());
                }
                if (ds.Tables[0].Rows[i]["PartenerPolicyId"].ToString() != "")
                {
                    model.PartenerPolicyId = ds.Tables[0].Rows[i]["PartenerPolicyId"].ToString();
                }
                model.PolicyType = ds.Tables[0].Rows[i]["PolicyType"].ToString();
                lstUpload.Add(model);
            }
            return lstUpload;
        } 
        #endregion

        #region 批量更新数据
        public bool  BlukyUpdatePolicy(string uId,bool isSucess=false)
        {
            try
            {
              UploadPolicyRecord rec= GetModel(uId);  
                StringBuilder strWhere = new StringBuilder();
                if (isSucess)
                {
                    rec.UpdateSuccessCount = rec.UpdateSuccessCount + 1;
                }
                else
                {
                    rec.UpdateFailedCount = rec.UpdateFailedCount + 1;
                }
                rec.CreateTime = DateTime.Now;
                rec.UpdateTime = DateTime.Now;
                rec.UId = 0;
                rec.Id = System.Guid.NewGuid();
                Add(rec);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ND.PolicyService.DbEntity.UploadPolicyRecord GetModel(string uId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UId,PolicyId,CommisionMoney,CommsionPoint,CreateTime,UpdateTime,UpdateSuccessCount,UpdateFailedCount,PartenerPolicyId,PolicyType from UploadPolicyRecord ");
            strSql.Append(" where UId=@UId ORDER BY CreateTime desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@UId", SqlDbType.VarChar)};
            parameters[0].Value = uId;

            ND.PolicyService.DbEntity.UploadPolicyRecord model = new ND.PolicyService.DbEntity.UploadPolicyRecord();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UId"].ToString() != "")
                {
                    model.UId = long.Parse(ds.Tables[0].Rows[0]["UId"].ToString());
                }
                model.PolicyId = ds.Tables[0].Rows[0]["PolicyId"].ToString();
                if (ds.Tables[0].Rows[0]["CommisionMoney"].ToString() != "")
                {
                    model.CommisionMoney = decimal.Parse(ds.Tables[0].Rows[0]["CommisionMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommsionPoint"].ToString() != "")
                {
                    model.CommsionPoint = decimal.Parse(ds.Tables[0].Rows[0]["CommsionPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateSuccessCount"].ToString() != "")
                {
                    model.UpdateSuccessCount = int.Parse(ds.Tables[0].Rows[0]["UpdateSuccessCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateFailedCount"].ToString() != "")
                {
                    model.UpdateFailedCount = int.Parse(ds.Tables[0].Rows[0]["UpdateFailedCount"].ToString());
                }
                model.PartenerPolicyId = ds.Tables[0].Rows[0]["PartenerPolicyId"].ToString();
                model.PolicyType = ds.Tables[0].Rows[0]["PolicyType"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion
	}
}

