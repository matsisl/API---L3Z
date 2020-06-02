using AutoMapper;
using Repository.Common;
using Server_side_API.Models;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;

namespace Server_side_API.Utils
{
    public class AutomapProfile : Profile
    {
        public AutomapProfile()
        {
            CreateMap<VehicleMake, VehicleMakeServ>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelServ>().ReverseMap();
            //CreateMap<VehicleModel, VehicleModelServ>().ForMember(dest=>dest.VehicleMake,otp=>otp.MapFrom(src=>new VehicleMake() { Id=src.MakeId})).ReverseMap().ForMember(dest=>dest.MakeId,otp=>otp.MapFrom(src=>src.VehicleMake.Id));
        }
    }
}