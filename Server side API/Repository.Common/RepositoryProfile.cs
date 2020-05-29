using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            CreateMap<VehicleMakeRepo, VehicleMake>().ReverseMap();
            CreateMap<VehicleModelRepo, VehicleModel>().ReverseMap();
        }
    }
}
