using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyUploadService.DtoModel.SeatDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.PolicyUploadService.WebApiHost.Controllers
{
    [RoutePrefix("api/SeatDiscountService")]
    public class SeatDiscountServiceController : ApiController,ISeatDiscount
    {
        [ResponseType(typeof(SeatDiscountListResponse))]
        [HttpPost, Route("SeatDiscountList")]
        public SeatDiscountListResponse SeatDiscountList()
        {
            ISeatDiscount seat = new DefaultSeatDiscount();
            return seat.SeatDiscountList();
        }
    }
}
