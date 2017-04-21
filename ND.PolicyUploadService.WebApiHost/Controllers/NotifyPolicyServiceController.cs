using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
     [RoutePrefix("api/NotifyPolicyService")]
    public class NotifyPolicyServiceController : ApiController,IPolicyNotify
    {

        #region 更新回调结果
         [ResponseType(typeof(EmptyResponse))]
         [HttpPost, Route("UpdateNotify")]
         public DtoModel.EmptyResponse UpdateNotify(DtoModel.UpdateNotifyRequest request)
         {
             IPolicyNotify notify = new DefaultPolicyNotify();
             return notify.UpdateNotify(request);
         } 
        #endregion

        #region 查询回调结果
         [ResponseType(typeof(SearchNotifyResponse))]
         [HttpPost, Route("SearchNotifyList")]
        public DtoModel.SearchNotifyResponse SearchNotifyList(DtoModel.SearchNotifyRequest request)
        {
            IPolicyNotify notify = new DefaultPolicyNotify();
            return notify.SearchNotifyList(request);
        } 
        #endregion

         #region 保存回调结果
         [ResponseType(typeof(SaveNotifyResponse))]
         [HttpPost, Route("SaveNotify")]
         public SaveNotifyResponse SaveNotify(PolicyNotifyRequest request)
         {
             IPolicyNotify notify = new DefaultPolicyNotify();
            return notify.SaveNotify(request);
         } 
         #endregion
    }
}
