using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.RealTimeUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
    //[RoutePrefix("api/RealTimeUploadService")]
    //public class RealTimeUploadServiceController : ApiController,IRealTimeUpload
    //{
    //    [ResponseType(typeof(SearchRealTimeUploadResponse))]
    //    [HttpPost, Route("SearchRealTimeUploadRecord")]
    //    public DtoModel.RealTimeUpload.SearchRealTimeUploadResponse SearchRealTimeUploadRecord(DtoModel.RealTimeUpload.SearchRealTimeUploadRequest request)
    //    {
    //        IRealTimeUpload ru = new DefaultRealTimeUpload();
    //        return ru.SearchRealTimeUploadRecord(request);
    //    }

    //    [ResponseType(typeof(EmptyResponse))]
    //    [HttpPost, Route("SaveRealTimeUploadRecord")]
    //    public DtoModel.EmptyResponse SaveRealTimeUploadRecord(DtoModel.RealTimeUpload.SaveRealTimeUploadRequest request)
    //    {
    //        IRealTimeUpload ru = new DefaultRealTimeUpload();
    //        return ru.SaveRealTimeUploadRecord(request);
    //    }
    //}
}
