using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using System.Collections.Generic;
using ND.PolicyService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;

namespace ND.PolicyService.CoreLib
{
	/// <summary>
	/// 数据访问类:UpLoadRecord
	/// </summary>
	public partial class UpLoadRecordLib
	{
		public UpLoadRecordLib()
		{}
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UpLoadRecord");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ND.PolicyService.DbEntity.UpLoadRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UpLoadRecord(");
            strSql.Append("PurchaserType,UploadType,LastUpdateTime,LastPolicyId,UploadFilePath,ResponseParams,NotifyResult,CreateTime,RequestParams,IsEnabled,Remark,OperName,CompleteTime,UploadCount,BeforeLastUpdateTime,BeforeLastPolicyId,PolicyType,FailedCount)");
            strSql.Append(" values (");
            strSql.Append("@PurchaserType,@UploadType,@LastUpdateTime,@LastPolicyId,@UploadFilePath,@ResponseParams,@NotifyResult,@CreateTime,@RequestParams,@IsEnabled,@Remark,@OperName,@CompleteTime,@UploadCount,@BeforeLastUpdateTime,@BeforeLastPolicyId,@PolicyType,@FailedCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaserType", SqlDbType.NVarChar,50),
					new SqlParameter("@UploadType", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.VarChar,50),
					new SqlParameter("@LastPolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@UploadFilePath", SqlDbType.NVarChar,500),
					new SqlParameter("@ResponseParams", SqlDbType.NVarChar),
					new SqlParameter("@NotifyResult", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@RequestParams", SqlDbType.NVarChar),
					new SqlParameter("@IsEnabled", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@OperName", SqlDbType.NVarChar,50),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@UploadCount", SqlDbType.Int,4),
					new SqlParameter("@BeforeLastUpdateTime", SqlDbType.NVarChar,50),
					new SqlParameter("@BeforeLastPolicyId", SqlDbType.NVarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.Int,4),
					new SqlParameter("@FailedCount", SqlDbType.Int,4)};
            parameters[0].Value = model.PurchaserType;
            parameters[1].Value = model.UploadType;
            parameters[2].Value = model.LastUpdateTime;
            parameters[3].Value = model.LastPolicyId;
            parameters[4].Value = model.UploadFilePath;
            parameters[5].Value = model.ResponseParams;
            parameters[6].Value = model.NotifyResult;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.RequestParams;
            parameters[9].Value = model.IsEnabled;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.OperName;
            parameters[12].Value = model.CompleteTime;
            parameters[13].Value = model.UploadCount;
            parameters[14].Value = model.BeforeLastUpdateTime;
            parameters[15].Value = model.BeforeLastPolicyId;
            parameters[16].Value = model.PolicyType;
            parameters[17].Value = model.FailedCount;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(ND.PolicyService.DbEntity.UpLoadRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UpLoadRecord set ");
            strSql.Append("PurchaserType=@PurchaserType,");
            strSql.Append("UploadType=@UploadType,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastPolicyId=@LastPolicyId,");
            strSql.Append("UploadFilePath=@UploadFilePath,");
            strSql.Append("ResponseParams=@ResponseParams,");
            strSql.Append("NotifyResult=@NotifyResult,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("RequestParams=@RequestParams,");
            strSql.Append("IsEnabled=@IsEnabled,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("OperName=@OperName,");
            strSql.Append("CompleteTime=@CompleteTime,");
            strSql.Append("UploadCount=@UploadCount,");
            strSql.Append("BeforeLastUpdateTime=@BeforeLastUpdateTime,");
            strSql.Append("BeforeLastPolicyId=@BeforeLastPolicyId,");
            strSql.Append("PolicyType=@PolicyType,");
            strSql.Append("FailedCount=@FailedCount");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@PurchaserType", SqlDbType.NVarChar,50),
					new SqlParameter("@UploadType", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.VarChar,50),
					new SqlParameter("@LastPolicyId", SqlDbType.VarChar,50),
					new SqlParameter("@UploadFilePath", SqlDbType.NVarChar,500),
					new SqlParameter("@ResponseParams", SqlDbType.NVarChar),
					new SqlParameter("@NotifyResult", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@RequestParams", SqlDbType.NVarChar),
					new SqlParameter("@IsEnabled", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@OperName", SqlDbType.NVarChar,50),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@UploadCount", SqlDbType.Int,4),
					new SqlParameter("@BeforeLastUpdateTime", SqlDbType.NVarChar,50),
					new SqlParameter("@BeforeLastPolicyId", SqlDbType.NVarChar,50),
					new SqlParameter("@PolicyType", SqlDbType.Int,4),
					new SqlParameter("@FailedCount", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.PurchaserType;
            parameters[1].Value = model.UploadType;
            parameters[2].Value = model.LastUpdateTime;
            parameters[3].Value = model.LastPolicyId;
            parameters[4].Value = model.UploadFilePath;
            parameters[5].Value = model.ResponseParams;
            parameters[6].Value = model.NotifyResult;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.RequestParams;
            parameters[9].Value = model.IsEnabled;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.OperName;
            parameters[12].Value = model.CompleteTime;
            parameters[13].Value = model.UploadCount;
            parameters[14].Value = model.BeforeLastUpdateTime;
            parameters[15].Value = model.BeforeLastPolicyId;
            parameters[16].Value = model.PolicyType;
            parameters[17].Value = model.FailedCount;
            parameters[18].Value = model.Id;

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
        public bool Delete(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UpLoadRecord ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
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
            strSql.Append("delete from UpLoadRecord ");
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
        public ND.PolicyService.DbEntity.UpLoadRecord GetModel(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,PurchaserType,UploadType,LastUpdateTime,LastPolicyId,UploadFilePath,ResponseParams,NotifyResult,CreateTime,RequestParams,IsEnabled,Remark,OperName,CompleteTime,UploadCount,BeforeLastUpdateTime,BeforeLastPolicyId,PolicyType,FailedCount from UpLoadRecord ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
};
            parameters[0].Value = Id;

           ND.PolicyService.DbEntity.UpLoadRecord model = new ND.PolicyService.DbEntity.UpLoadRecord();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PurchaserType = ds.Tables[0].Rows[0]["PurchaserType"].ToString();
                model.UploadType = ds.Tables[0].Rows[0]["UploadType"].ToString();
                model.LastUpdateTime = ds.Tables[0].Rows[0]["LastUpdateTime"].ToString();
                model.LastPolicyId = ds.Tables[0].Rows[0]["LastPolicyId"].ToString();
                model.UploadFilePath = ds.Tables[0].Rows[0]["UploadFilePath"].ToString();
                model.ResponseParams = ds.Tables[0].Rows[0]["ResponseParams"].ToString();
                if (ds.Tables[0].Rows[0]["NotifyResult"].ToString() != "")
                {
                    model.NotifyResult = int.Parse(ds.Tables[0].Rows[0]["NotifyResult"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.RequestParams = ds.Tables[0].Rows[0]["RequestParams"].ToString();
                if (ds.Tables[0].Rows[0]["IsEnabled"].ToString() != "")
                {
                    model.IsEnabled = int.Parse(ds.Tables[0].Rows[0]["IsEnabled"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.OperName = ds.Tables[0].Rows[0]["OperName"].ToString();
                if (ds.Tables[0].Rows[0]["CompleteTime"].ToString() != "")
                {
                    model.CompleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["CompleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UploadCount"].ToString() != "")
                {
                    model.UploadCount = int.Parse(ds.Tables[0].Rows[0]["UploadCount"].ToString());
                }
                model.BeforeLastUpdateTime = ds.Tables[0].Rows[0]["BeforeLastUpdateTime"].ToString();
                model.BeforeLastPolicyId = ds.Tables[0].Rows[0]["BeforeLastPolicyId"].ToString();
                if (ds.Tables[0].Rows[0]["PolicyType"].ToString() != "")
                {
                    model.PolicyType = int.Parse(ds.Tables[0].Rows[0]["PolicyType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FailedCount"].ToString() != "")
                {
                    model.FailedCount = int.Parse(ds.Tables[0].Rows[0]["FailedCount"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PurchaserType,UploadType,LastUpdateTime,LastPolicyId,UploadFilePath,ResponseParams,NotifyResult,CreateTime,RequestParams,IsEnabled,Remark,OperName,CompleteTime,UploadCount,BeforeLastUpdateTime,BeforeLastPolicyId,PolicyType,FailedCount ");
            strSql.Append(" FROM UpLoadRecord ");
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
            strSql.Append(" Id,PurchaserType,UploadType,LastUpdateTime,LastPolicyId,UploadFilePath,ResponseParams,NotifyResult,CreateTime,RequestParams,IsEnabled,Remark,OperName,CompleteTime,UploadCount,BeforeLastUpdateTime,BeforeLastPolicyId,PolicyType,FailedCount ");
            strSql.Append(" FROM UpLoadRecord ");
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
            parameters[0].Value = "UpLoadRecord";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region extend method

        #region 获取modelList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UpLoadRecord> GetModelList(string strWhere)
        {
            List<UpLoadRecord> lstUpload = new List<UpLoadRecord>();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Id,PurchaserType,UploadType,LastUpdateTime,LastPolicyId,UploadFilePath,ResponseParams,NotifyResult,CreateTime,RequestParams,IsEnabled,Remark,OperName,CompleteTime,UploadCount,BeforeLastUpdateTime,BeforeLastPolicyId,PolicyType,FailedCount ");
                strSql.Append(" FROM UpLoadRecord ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" ORDER BY CreateTime desc ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UpLoadRecord model = new UpLoadRecord();
                    if (ds.Tables[0].Rows[i]["Id"].ToString() != "")
                    {
                        model.Id = long.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                    }
                    model.PurchaserType = ds.Tables[0].Rows[i]["PurchaserType"].ToString();
                    model.UploadType = ds.Tables[0].Rows[i]["UploadType"].ToString();
                    model.LastUpdateTime = ds.Tables[0].Rows[i]["LastUpdateTime"].ToString();
                    model.LastPolicyId = ds.Tables[0].Rows[i]["LastPolicyId"].ToString();
                    model.UploadFilePath = ds.Tables[0].Rows[i]["UploadFilePath"].ToString();
                    model.ResponseParams = ds.Tables[0].Rows[i]["ResponseParams"].ToString();
                    if (ds.Tables[0].Rows[i]["NotifyResult"].ToString() != "")
                    {
                        model.NotifyResult = int.Parse(ds.Tables[0].Rows[i]["NotifyResult"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[i]["CreateTime"].ToString());
                    }
                    model.RequestParams = ds.Tables[0].Rows[i]["RequestParams"].ToString();
                    if (ds.Tables[0].Rows[i]["IsEnabled"].ToString() != "")
                    {
                        model.IsEnabled = int.Parse(ds.Tables[0].Rows[i]["IsEnabled"].ToString());
                    }
                    model.Remark = ds.Tables[0].Rows[i]["Remark"].ToString();
                    model.OperName = ds.Tables[0].Rows[i]["OperName"].ToString();
                    if (ds.Tables[0].Rows[i]["CompleteTime"].ToString() != "")
                    {
                        model.CompleteTime = DateTime.Parse(ds.Tables[0].Rows[i]["CompleteTime"].ToString());
                    }
                    model.UploadCount = int.Parse(ds.Tables[0].Rows[i]["UploadCount"].ToString());
                    model.BeforeLastUpdateTime = ds.Tables[0].Rows[0]["BeforeLastUpdateTime"].ToString();
                    model.BeforeLastPolicyId = ds.Tables[0].Rows[0]["BeforeLastPolicyId"].ToString();
                    model.PolicyType = int.Parse(ds.Tables[0].Rows[i]["PolicyType"].ToString());
                    model.FailedCount = int.Parse(ds.Tables[0].Rows[i]["FailedCount"].ToString());
                    lstUpload.Add(model);
                }
                return lstUpload;
            }
            catch (Exception ex)
            {
                return lstUpload;
            }
        } 
        #endregion

        #region 查询最新一条记录
        public PolicyRecord GetLastUploadRecored(PurchaserType purchaser,UploadType uType)
        {
            PolicyRecord rec = new PolicyRecord();
            try
            {

                string strSql = "select top 1 LastUpdateTime,LastPolicyId from UpLoadRecord where IsEnabled=1 and NotifyResult=1 and UploadType='" + uType.ToString() + "' and PurchaserType='" + purchaser.ToString() + "' and IsEnabled =1  ORDER BY CreateTime DESC";
                SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql);
                bool flag = false;
                while (reader.Read())
                {
                    flag = true;
                    rec.LastPolicyId = long.Parse(reader["LastPolicyId"].ToString());
                    rec.LastUpdateTime = Convert.ToDateTime(reader["LastUpdateTime"]);

                }
                if(!flag)
                {
                    rec = new PolicyRecord() { LastUpdateTime =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00"), LastPolicyId = 0 };
                }
                reader.Close();
                reader = null;
               
            }
            catch(Exception ex)
            {
                rec = new PolicyRecord() { LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00"), LastPolicyId = 0 };
            }
            return rec;
        }
        #endregion

        #endregion
	}
}

