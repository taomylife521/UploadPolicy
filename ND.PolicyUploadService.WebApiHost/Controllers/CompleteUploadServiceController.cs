using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
     [RoutePrefix("api/CompleteUploadService")]
    public class CompleteUploadServiceController : ApiController, ICompleteUploadPolicy
    {
        [ResponseType(typeof(CompleteUploadPolicyResponse))]
        [HttpPost, Route("SearchCompleteUploadPolicy")]
        public DtoModel.CompleteUploadPolicy.CompleteUploadPolicyResponse SearchCompleteUploadPolicy()
        {
            ICompleteUploadPolicy upload = new DefaultCompleteUploadPolicy();
            return upload.SearchCompleteUploadPolicy();
        }
    }
}
