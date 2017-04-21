using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.QunarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
   [RoutePrefix("api/QunarCodeService")]
    public class QunarCodeServiceController : ApiController,IQunarCode
    {
       [ResponseType(typeof(EmptyResponse))]
       [HttpPost, Route("AddCode")]
       public EmptyResponse AddCode(QunarCodeRequest request)
       {
           IQunarCode qunarCode = new DefaultQunarCode();
           return qunarCode.AddCode(request);

       }

       [ResponseType(typeof(EmptyResponse))]
       [HttpPost, Route("DeleteCode")]
       public EmptyResponse DeleteCode(QunarCodeRequest request)
       {
           IQunarCode qunarCode = new DefaultQunarCode();
           return qunarCode.DeleteCode(request);
       }

        [ResponseType(typeof(QunarCodeListResponse))]
        [HttpPost, Route("GetList")]
       public QunarCodeListResponse GetList(QunarCodeRequest request)
        {
            IQunarCode qunarCode = new DefaultQunarCode();
            return qunarCode.GetList(request);
        }
    }
}
