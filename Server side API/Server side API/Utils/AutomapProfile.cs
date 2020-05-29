using AutoMapper;
using Repository.Common;
using Server_side_API.Models;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_side_API.Utils
{
    public class AutomapProfile : Profile
    {
        public AutomapProfile()
        {
            CreateMap<VehicleMake, VehicleMakeServ>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelServ>().ReverseMap();
        }
    }
}