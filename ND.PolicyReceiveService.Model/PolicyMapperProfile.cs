using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Model;

namespace NDFront.Lib.DtoModel.flight.autoMapper
{
   public class PolicyMapperProfile:AutoMapperBase
    {
       public PolicyMapperProfile()
       {
          // Mapper.CreateMap<IDataReader, Policy>();
          // Mapper.CreateMap<IDataReader, Policy19e>();

           Mapper.CreateMap<IDataReader, PolicyDetail>();
           Mapper.CreateMap<IDataReader, PolicySyncRec>();

           Mapper.CreateMap<Policies, PolicyDetail>();
           Mapper.CreateMap<Policies, PolicySyncRec>();

         //  Mapper.CreateMap<PolicyInfo,Policy>();
          // Mapper.CreateMap<PolicyInfo,Policy19e>();
       }
    }

     
}
