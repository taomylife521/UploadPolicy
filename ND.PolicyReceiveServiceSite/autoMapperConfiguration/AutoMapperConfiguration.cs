using AutoMapper;
using NDFront.Lib.DtoModel.flight.autoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ND.WebService.LogIISHost.autoMapperConfiguration
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<PolicyMapperProfile>());
        }
    }
}