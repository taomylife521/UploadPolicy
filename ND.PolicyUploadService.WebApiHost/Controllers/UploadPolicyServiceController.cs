using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
    //public class UploadPolicyServiceController : ApiController,IUpload
    //{

    //    public ND.PolicyUploadService.DtoModel.UploadResponse Upload(ND.PolicyUploadService.DtoModel.SearchPolicyRequest request)
    //    {
    //        UploadResponse res = new UploadResponse();
    //            UploadPolicyResponse rep = new UploadPolicyResponse();

    //        switch (request.pType)
    //        {
    //            case PolicyService.Enums.PurchaserType.Qunar:
    //                {
    //                    IUploadPolicy uplload = new QunarUpLoadPolicy();
                    
    //                    //uplload.OnWoking += uplload_OnWoking;
    //                    QunarUploadPolicyRequest qunarRequest = new QunarUploadPolicyRequest()
    //                    {
    //                        FormatFilePath = ConfigurationManager.AppSettings["FormatQunarFilePath"].ToString(),//xml文件路径
    //                        FormatZipFilePath = ConfigurationManager.AppSettings["FormatQunarZipFilePath"].ToString(),//压缩包文件路径
    //                        IsPrintSql = Convert.ToBoolean(ConfigurationManager.AppSettings["IsPrintSql"].ToString()),
    //                        MaxTaskCount = int.Parse(ConfigurationManager.AppSettings["MaxTaskCount"].ToString()),
    //                        PerTaskCount = int.Parse(ConfigurationManager.AppSettings["PerTaskCount"].ToString()),
    //                        PageSize = request.PageSize <= 0 ? 50 : request.PageSize,
    //                        PolicyType = request.PolicyType,
    //                        SqlWhere = request.SqlWhere,
    //                        UploadType = request.UType,
    //                        QunarUpLoadUrl = ConfigurationManager.AppSettings["QunarUpLoadUrl"].ToString(),
    //                        CommisionMoney = request.CommisionMoney == null ? 0 : request.CommisionMoney,
    //                        CommsionPoint = request.CommsionPoint == null ? 0 : request.CommsionPoint,
    //                        OperName=request.OperName
    //                    };
    //                    if (request.UType == UploadType.Incremental)
    //                    {
    //                        rep = uplload.UpLoadIncrementPolicy(qunarRequest);
    //                    }
    //                    else if (request.UType == UploadType.FullUpload)
    //                    {
    //                        rep = uplload.UploadFullPolicy(qunarRequest);
    //                    }
    //                    if (rep.ErrCode == ResultType.Failed)
    //                    {
    //                        res = new UploadResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = rep.ErrMsg, Excption = rep.Excption };
    //                        return res;
    //                    }
    //                }
    //                break;
    //            case PolicyService.Enums.PurchaserType.TaoBao:
    //                res = new UploadResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "暂未开通淘宝上传接口!" };
    //                return res;
    //                break;
    //            default:
    //                res = new UploadResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = "未知的上传平台" };
    //                break;

    //        }
    //        res = new UploadResponse { ErrCode = PolicyService.Enums.ResultType.Sucess,UploadStatusId =rep.UploadStatusId  };
    //        return res;
    //    }
    //}
}
