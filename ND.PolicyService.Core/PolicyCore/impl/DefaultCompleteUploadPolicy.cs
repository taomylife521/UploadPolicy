using ND.PolicyService.CoreLib;
using ND.PolicyService.DbEntity;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore.impl
{
    public class DefaultCompleteUploadPolicy : ICompleteUploadPolicy
    {
        public ND.PolicyUploadService.DtoModel.CompleteUploadPolicy.CompleteUploadPolicyResponse SearchCompleteUploadPolicy()
        {
            CompleteUploadPolicyResponse rep = new CompleteUploadPolicyResponse();
            UploadPolicyRecordLib uPolicyLib = new UploadPolicyRecordLib();
           List<UploadPolicyRecord> lstUpload= uPolicyLib.GetModelList(" CreateTime> '"+DateTime.Now.ToString("yyyy-MM-dd")+" 0:00:00"+"'");
           lstUpload.ForEach(x =>
           {
               rep.CompleteUploadPolicyCollection.Add(new CompleteUploadPolicyDto()
               {
                   CommisionMoney =x.CommisionMoney,
                   CommsionPoint=x.CommsionPoint,
                   PolicyId = x.PolicyId,
                   PartenerPolicyId=x.PartenerPolicyId,
                   PolicyType = x.PolicyType
               });

           });
           rep.ErrCode = PolicyService.Enums.ResultType.Sucess;
           return rep;
        }
    }
}
