using AutoMapper;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public class ServiceProfile: Profile
    {
        public ServiceProfile()
        {
            CreateMap<VehicleMakeServ, VehicleMakeRepo>().ReverseMap();
        }
    }
}
