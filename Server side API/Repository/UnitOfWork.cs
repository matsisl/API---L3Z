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

        public UnitOfWork(VehicleContext vehicleContext,IMapper mapper)
        {
            VehicleContext = vehicleContext;

            //pozvati maper iz DI
            this.mapper = mapper; 
            VehicleMakeRepository = new VehicleMakeRepository(vehicleContext, mapper);
        }

        public IVehicleMakeRepository VehicleMakeRepository { get; }

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
