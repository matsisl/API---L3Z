using AutoMapper;
using DAL;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private VehicleContext VehicleContext;
        private IMapper mapper;

        public UnitOfWork(VehicleContext vehicleContext, IMapper mapper, VehicleMakeRepository vehicleMakeRepository, VehicleModelRepository vehicleModelRepository)
        {
            VehicleContext = vehicleContext;
            this.mapper = mapper;
            VehicleMakeRepository = vehicleMakeRepository;
            VehicleModelRepository = vehicleModelRepository;
        }

        public IVehicleMakeRepository VehicleMakeRepository { get; set; }

        public IVehicleModelRepository VehicleModelRepository { get; set; }

        public int Complete()
        {
            return VehicleContext.SaveChanges();
        }

        public void Dispose()
        {
            VehicleContext.Dispose();
        }
    }
}
