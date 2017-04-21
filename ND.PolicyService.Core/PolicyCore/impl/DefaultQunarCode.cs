using ND.PolicyService.CoreLib;
using ND.PolicyService.DbEntity;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.QunarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore.impl
{
    
   public class DefaultQunarCode:IQunarCode
    {
       public EmptyResponse AddCode(QunarCodeRequest request)
        {
            DeleteCode(request);
           QunarCodeLib codeLib = new QunarCodeLib();
           long r= codeLib.Add(new DbEntity.QunarCode()
            {
                Code=request.Code
            });
            if(r <= 0)
            {
                return new EmptyResponse { ErrCode = Enums.ResultType.Failed };
            }
            return new EmptyResponse { ErrCode = Enums.ResultType.Sucess };
        }

       public EmptyResponse DeleteCode(QunarCodeRequest request)
        {
            QunarCodeLib codeLib = new QunarCodeLib();
            bool r = codeLib.DeleteByCode(request.Code);
           if(r)
           {
               return new EmptyResponse { ErrCode = Enums.ResultType.Sucess };
           }
           return new EmptyResponse { ErrCode = Enums.ResultType.Failed };
           
        }

       public QunarCodeListResponse GetList(QunarCodeRequest request)
        {
            QunarCodeListResponse rep = new QunarCodeListResponse();
            QunarCodeLib codeLib = new QunarCodeLib();
            List<QunarCode> lstCodes = codeLib.GetModelList(request.Code);
            lstCodes.ForEach(x =>
            {
                rep.Codes.Add(x.Code);
            });
            return rep;
        }
    }
}
