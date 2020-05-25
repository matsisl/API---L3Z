using AutoMapper;
using DAL;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private DbSet<VehicleMake> vehiclesSet { get; }
        private IMapper mapper;

        public VehicleMakeRepository(VehicleContext vehicleContext,IMapper mapper)
        {
            this.vehiclesSet = vehicleContext.VehicleMakes;
            this.mapper = mapper;
        }

        public Task<bool> Add(VehicleMakeRepo entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(VehicleMakeRepo entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleMakeRepo>> GetAll()
        {
            List<VehicleMake> vehicleMakes = await vehiclesSet.ToListAsync<VehicleMake>();
            List<VehicleMakeRepo> vehicleMakesRepo = new List<VehicleMakeRepo>();
            foreach (VehicleMake item in vehicleMakes)
            {
                vehicleMakesRepo.Add(mapper.Map<VehicleMakeRepo>(item));
            }
            return vehicleMakesRepo;
        }

        public Task<VehicleMakeRepo> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehicleModelRepo>> GetVehicleModels()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(VehicleMakeRepo entity)
        {
            throw new NotImplementedException();
        }
    }
}
