
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.DbEntity;
using ND.PolicyService.Enums;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyService.CoreLib;
using ND.PolicyUploadService.DtoModel;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Core.PolicyCore;
using ND.PolicyUploadService.DtoModel.dtoEntity;

namespace ND.PolicyService.Core.PolicyCore.impl
{
    /// <summary>
    /// 默认政策回调处理类
    /// </summary>
    public class DefaultPolicyNotify : IPolicyNotify
    {
        #region 保存回调结果
        /// <summary>
        /// 保存回调结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SaveNotifyResponse SaveNotify(PolicyNotifyRequest request)
        {
           
                UpLoadRecordLib recLib = new UpLoadRecordLib();
                UploadPolicyRecordLib policyLib = new UploadPolicyRecordLib();


                UpLoadRecord rec = new UpLoadRecord();
                rec.UploadFilePath = request.FileNamePath;
                rec.LastPolicyId = request.PolicyRec.LastPolicyId.ToString();
                rec.LastUpdateTime = request.PolicyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                rec.UploadType = request.UploadType.ToString();
                rec.PurchaserType = request.Purchaser.ToString();
                rec.ResponseParams = request.ResponseParams;
                rec.NotifyResult = request.NotifyResult;
                rec.CreateTime = DateTime.Now;
                rec.IsEnabled = 1;
                rec.Remark = request.Remark;
                rec.RequestParams = request.RequestParams;
                rec.OperName = request.OperName;
                rec.UploadCount = request.UploadCount;
                rec.BeforeLastPolicyId = request.BeforePolicyRec.LastPolicyId.ToString();
                rec.BeforeLastUpdateTime = request.BeforePolicyRec.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                rec.PolicyType = (int)request.PolicyType;
                long r = recLib.Add(rec);
                if (r <= 0)
                {
                    string name = "Qunar\\NotifyQunarPolicyIdsRecLog";
                    CoreHelper.SaveLastUpTimeAndId(string.Join(",", request.UploadPolicyIds.ToArray()), name);
                    return new SaveNotifyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "保存回调结果失败:保存数据:" + JsonConvert.SerializeObject(rec) };
                }

                List<string> lstSql = new List<string>();
                foreach (KeyValuePair<string, string> item in request.UploadPolicyIds)
                {
                    UploadPolicyRecord uPolicy = new UploadPolicyRecord();
                    uPolicy.CommisionMoney = request.CommisionMoney;
                    uPolicy.CommsionPoint = request.CommisionPoint;
                    uPolicy.Id = System.Guid.NewGuid();
                    uPolicy.UId = r;
                    uPolicy.PolicyId = item.Key;
                    uPolicy.PartenerPolicyId = item.Value;
                    uPolicy.CreateTime = DateTime.Now;
                    uPolicy.PolicyType = request.PolicyType.ToString();
                    lstSql.Add(policyLib.Add(uPolicy, false));
                }

                if (lstSql.Count > 0)
                {
                    int r2 = DbHelperSQL.ExecuteSqlTran(lstSql);
                    if (r2 <= 0)
                    {
                        string name = "Qunar\\NotifyQunarPolicyIdsRecLog";
                        CoreHelper.SaveLastUpTimeAndId(DateTime.Now + ":保存上传政策id失败:" + string.Join(",", request.UploadPolicyIds.ToArray()), name);
                        return new SaveNotifyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "保存回调结果失败:保存数据:" + JsonConvert.SerializeObject(rec) };
                    }
                }
                return new SaveNotifyResponse { ErrCode = PolicyService.Enums.ResultType.Sucess, ErrMsg = "", UploadStatusId = r.ToString() };
               
          
          

        } 
        #endregion

        #region 查询回调结果
        /// <summary>
        /// 查询回调结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SearchNotifyResponse SearchNotifyList(SearchNotifyRequest request)
        {
            try
            {
                UpLoadRecordLib recLib = new UpLoadRecordLib();
                List<UpLoadRecordDto> notifyList = new List<UpLoadRecordDto>();
                if(!string.IsNullOrEmpty(request.UploadStatusId))
                {
                    UpLoadRecord rec = recLib.GetModel(int.Parse(request.UploadStatusId));
                    if (rec.NotifyResult <= 0)//不成功
                    {
                        return new SearchNotifyResponse() { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "未找到任何上传记录"};
                    }
                    notifyList.Add(new UpLoadRecordDto
                    {
                        CompleteTime=rec.CompleteTime,
                        CreateTime=rec.CreateTime,
                        Id=rec.Id,
                        NotifyResult=rec.NotifyResult,
                        IsEnabled=rec.IsEnabled,
                        LastPolicyId=rec.LastPolicyId,
                        LastUpdateTime=rec.LastUpdateTime,
                        OperName = rec.OperName,
                        PurchaserType= rec.PurchaserType,
                        Remark=rec.Remark,
                        RequestParams=rec.RequestParams,
                        ResponseParams=rec.ResponseParams,
                        UploadFilePath=rec.UploadFilePath,
                        UploadType=rec.UploadType,
                        Beforelastpolicyid=rec.BeforeLastPolicyId,
                        Beforelastupdatetime=rec.BeforeLastUpdateTime,
                        Uploadcount=rec.UploadCount,
                        Policytype = rec.PolicyType
                    });  
                }
                else
                {
                    string strSql = "";
                    if(request.IsSearchSystem)
                    {
                        strSql = " and OperName='system'";
                    }
                    List<UpLoadRecord> lstRecord = recLib.GetModelList("  CreateTime> '" + DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00" + "' and IsEnabled=1 " + strSql);
                    lstRecord.ForEach(x =>
                    {
                        notifyList.Add(new UpLoadRecordDto
                        {
                            CompleteTime = x.CompleteTime,
                            CreateTime = x.CreateTime,
                            Id = x.Id,
                            NotifyResult = x.NotifyResult,
                            IsEnabled = x.IsEnabled,
                            LastPolicyId = x.LastPolicyId,
                            LastUpdateTime = x.LastUpdateTime,
                            OperName = x.OperName,
                            PurchaserType = x.PurchaserType,
                            Remark = x.Remark,
                            RequestParams = x.RequestParams,
                            ResponseParams = x.ResponseParams,
                            UploadFilePath = x.UploadFilePath,
                            UploadType = x.UploadType,
                            Beforelastpolicyid = x.BeforeLastPolicyId,
                            Beforelastupdatetime = x.BeforeLastUpdateTime,
                            Uploadcount = x.UploadCount,
                            Policytype = x.PolicyType
                        });  
                    });
                }

                return new SearchNotifyResponse() { ErrCode = PolicyService.Enums.ResultType.Sucess, ErrMsg = "",NotifyList=notifyList };
            }
            catch (Exception ex)
            {
                return new SearchNotifyResponse() { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg =  ex.Message };
            }

        } 
        #endregion

        #region UpdateNotify 更改回调结果
        public EmptyResponse UpdateNotify(UpdateNotifyRequest request)
        {
            try
            {

                UpLoadRecordLib recLib = new UpLoadRecordLib();
                UploadPolicyRecordLib recPolicyLib = new UploadPolicyRecordLib();
                PolicySyncRecLib syncLib = new PolicySyncRecLib();
               
               
                request.UpdateStatusId = request.UpdateStatusId.Replace("\"", "").Replace("\\","");
                UpLoadRecord rec = recLib.GetModel(int.Parse(request.UpdateStatusId));
                if (rec == null)
                {
                    return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "未找到此上传的记录！" };
                }
                if(rec.OperName.ToLower() == "system")
                {
                    if ((SuccessStatus)request.NotifyResult == SuccessStatus.Success)
                   {
                      recPolicyLib.BlukyUpdatePolicy(rec.Id.ToString(), true);
                      recLib.Delete(rec.Id);//删除
                   }
                    else
                    {
                        recPolicyLib.BlukyUpdatePolicy(rec.Id.ToString(), false);
                        recLib.Delete(rec.Id);//删除
                    }
                    return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Sucess };
                }
               

                PurchaserType Purchaser = (PurchaserType)Enum.Parse(typeof(PurchaserType), rec.PurchaserType);
                UploadType uType = (UploadType)Enum.Parse(typeof(UploadType), rec.UploadType);
                
                #region 1.保存文件记录
                if (request.IsSucess)
                {
                   
                    string timeAndId = Convert.ToDateTime(rec.LastUpdateTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "|" + rec.LastPolicyId.ToString();
                   // CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\" + System.Guid.NewGuid() + ".txt", "读取最后记录"+timeAndId);//创建文件
                    // File.WriteAllText("e://1.txt", timeAndId);
                    string name = uType == UploadType.FullUpload ? rec.PurchaserType + "\\" + rec.PurchaserType + "FullPolicyRecLog" : rec.PurchaserType + "\\" + rec.PurchaserType + "IncrementPolicyRecLog";
                    CoreHelper.SaveLastUpTimeAndId(timeAndId, name);
                   // CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\" + System.Guid.NewGuid() + ".txt", "保存最后记录成功");//创建文件
                }
              
                #endregion

                #region 2.更新表中记录
               
                rec.ResponseParams = request.ResponseParams;
                rec.NotifyResult = (int)request.NotifyResult;
                rec.FailedCount = request.NotifyResult == SuccessStatus.Failed ? rec.FailedCount + 1 : rec.FailedCount;
                rec.CompleteTime = DateTime.Now;
                rec.LastUpdateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00";
                rec.LastPolicyId = "0";
                bool r = recLib.Update(rec);
                if (!r)
                {
                    return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "更新记录失败！" };
                }
                bool flag = false;
                if (request.NotifyResult == SuccessStatus.Failed || request.NotifyResult == SuccessStatus.Other)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
                recPolicyLib.BlukyUpdatePolicy(rec.Id.ToString(), flag);
                #endregion


                #region 3.批量更新已经上传完成的政策
                if (request.IsSucess)
                {
                    string selectSql = "";
                    int totalCount = 0;
                   List<UploadPolicyRecord> lstUploadIds= recPolicyLib.GetModelList(" UId='" + rec .Id+ "'");
                   List<string> lstAddPolicies = new List<string>();
                   lstUploadIds.ForEach(x =>
                   {
                       lstAddPolicies.Add(x.PolicyId);
                   });
                    //List<string> lstAddPolicies = rec.Remark.Split(',').ToList();
                    if (lstAddPolicies.Count > 0)
                    {
                        bool r2 = syncLib.BlukyUpdatePolicyUploaded(lstAddPolicies);
                        if (!r)
                        {
                            return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "批量把政策置为已上传失败！" };
                        }
                    }
                }
                #endregion

                return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Sucess };
            }
            catch(Exception ex)
            {
               
                return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Failed,ErrMsg=JsonConvert.SerializeObject(ex) };
            }
        } 
        #endregion
    }
}
