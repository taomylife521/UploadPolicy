using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.CoreLib;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.RealTimeUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore.impl
{
   public class DefaultSearchPolicy:ISearchPolicy
    {
        public ND.PolicyUploadService.DtoModel.SearchPolicyResponse SearchPolicy(SearchPolicyRequest request)
        {
            SearchPolicyResponse rep=new SearchPolicyResponse();
            PolicySyncRecLib syncLib = new PolicySyncRecLib();
            UpLoadRecordLib recLib = new UpLoadRecordLib();
            PolicyRecord policyRec = new PolicyRecord();
            //if (!request.IsUpload)//如果只是查询走初始值
            //{
            //   policyRec = new PolicyRecord
            //    {
            //        LastPolicyId = 0,
            //        LastUpdateTime =Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 0:00:00")
            //    };
            //}
            //else//上传的话读取最新一次更新的记录
            //{
                
            //   // if(!request.IsRealTimeUpload)//是否实时更新到去哪儿，不是
            //   // {
                    
            //        policyRec = recLib.GetLastUploadRecored(request.pType, request.UType);//查询最新一次更新的记录 
            //    //}
            //    //else//是，实时更新到去哪儿
            //    //{
            //    //    IRealTimeUpload realTimeUpload = new DefaultRealTimeUpload();
            //    //    SearchRealTimeUploadResponse realTimeRep= realTimeUpload.SearchRealTimeUploadRecord(new DtoModel.RealTimeUpload.SearchRealTimeUploadRequest() { LockPerson=request.OperName,Purchaser =request.pType});
            //    //    if(realTimeRep.ErrCode == PolicyService.Enums.ResultType.Failed)
            //    //    {
            //    //        return new SearchPolicyResponse { lstPolicies = new List<Policies>(), LastPolicyRecord = new PolicyRecord(), TotalCount = 0 };
            //    //    }
            //    //    policyRec = realTimeRep.PolicyRec;
            //    //}
               
               
            //}
            
            string selectSql = "";
            int totalCount = 0;
            int pageSize = request.PageSize<=0 ? 100:request.PageSize;
            List<Policies> lstAddPolicies = syncLib.LoadPolicy(policyRec, request.CommisionMoney, request.CommsionPoint, pageSize, request.SqlWhere, ref selectSql, ref totalCount, request.UType, request.IsSearchTotalCount,request.IsUpload);//获取要上传的政策
            rep.lstPolicies = lstAddPolicies;
            rep.TotalCount = totalCount;
            rep.LastPolicyRecord = policyRec;
            return rep;
        }
    }
}
