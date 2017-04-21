using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
    public class SearchPolicyServiceController : ApiController,ISearchPolicy
    {
       
        public DtoModel.SearchPolicyResponse SearchPolicy(DtoModel.SearchPolicyRequest request)
        {
            ISearchPolicy policy = new DefaultSearchPolicy();
            return policy.SearchPolicy(request);
        }
      
    }
}
