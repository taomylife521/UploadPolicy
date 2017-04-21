using ND.PolicyReceiveService.Helper;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
      [RoutePrefix("api/QunarUploadConfigService")]
    public class QunarUploadConfigServiceController : ApiController
    {
        [ResponseType(typeof(QunarUploadConfigResponse))]
        [HttpPost, Route("LoadDefautConfig")]
        public QunarUploadConfigResponse LoadDefautConfig()
        {
            //string path = System.IO.Directory.GetCurrentDirectory();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string xmlCodes = File.ReadAllText(path + "bin\\QunarUploadConfig.xml");
            QunarUploadConfigResponse codeList = XmlHelper.Deserialize(typeof(QunarUploadConfigResponse), xmlCodes) as QunarUploadConfigResponse;
            return codeList;
        }

        [ResponseType(typeof(EmptyResponse))]
        [HttpPost, Route("SaveQunarDefautConfig")]
        public EmptyResponse SaveQunarDefautConfig(QunarUploadConfigResponse request)
        {

           // string path = System.IO.Directory.GetCurrentDirectory();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string content = XmlHelper.Serializer(typeof(QunarUploadConfigResponse), request);
             File.WriteAllText(path + "bin\\QunarUploadConfig.xml",content);
            return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Sucess, ErrMsg = "" };
        }
    }
}
