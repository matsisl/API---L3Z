using AutoMapper;
using DAL;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<bool> Add(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                VehicleMake make = vehiclesSet.Add(vehicleMake);
                if (make != null)
                {
                    provjera = true;
                }
            }
            return provjera;
        }

        public async Task<bool> Delete(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                vehiclesSet.Attach(vehicleMake);
                VehicleMake make = vehiclesSet.Remove(vehicleMake);
                if (make != null)
                {
                    provjera = true;
                }
            }
            return provjera;
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

        public async Task<VehicleMakeRepo> GetById(int id)
        {
            VehicleMake vehicleMake = await vehiclesSet.FindAsync(id);
            return mapper.Map<VehicleMakeRepo>(vehicleMake);
        }

        public async Task<IEnumerable<VehicleModelRepo>> GetVehicleModels(int id)
        {
            VehicleMake vehicleMakes = await vehiclesSet.FindAsync(id);
            List<VehicleModelRepo> vehicleModelsRepo = new List<VehicleModelRepo>();
            foreach (VehicleModel item in vehicleMakes.VehicleModels)
            {
                vehicleModelsRepo.Add(mapper.Map<VehicleModelRepo>(item));
            }
            return vehicleModelsRepo;
        }

        public async Task<bool> Update(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                var updatedVehicleMake = vehiclesSet.Find(entity.Id);
                if (updatedVehicleMake != null)
                {
                    updatedVehicleMake.Name = vehicleMake.Name;
                    updatedVehicleMake.Abrv = vehicleMake.Abrv;
                    provjera = true;
                }                
                
            }
            return provjera;
        }
    }
}
