using AutoMapper;
using DAL;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private DbSet<VehicleModel> vehicleModelSet { get; }
        private IMapper mapper;
        private VehicleContext Context;

        public VehicleModelRepository(VehicleContext context, IMapper mapper)
        {
            this.Context = context;
            this.vehicleModelSet = context.VehicleModels;
            this.mapper = mapper;
        }

        public async Task<bool> Add(VehicleModelRepo entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleModel vehicleModel = mapper.Map<VehicleModel>(entity);
                vehicleModel.MakeId = vehicleModel.VehicleMake.Id;
                VehicleModel model = vehicleModelSet.Add(vehicleModel);
                if (model != null)
                {
                    provjera = true;
                }
            }
            return provjera;
        }

        public async Task<bool> Delete(VehicleModelRepo entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleModel vehicleModel = mapper.Map<VehicleModel>(entity);
                VehicleModel model = await vehicleModelSet.FindAsync(vehicleModel.Id);
                if (model != null)
                {
                    vehicleModelSet.Remove(model);
                    provjera = true;
                }
            }
            return provjera;
        }

        public async Task<IEnumerable<VehicleModelRepo>> GetAll()
        {
            List<VehicleModel> vehicleModels = await vehicleModelSet.ToListAsync();
            List<VehicleModelRepo> vehicleModelsRepo = new List<VehicleModelRepo>();
            foreach (VehicleModel item in vehicleModels)
            {
                vehicleModelsRepo.Add(mapper.Map<VehicleModelRepo>(item));
            }
            return vehicleModelsRepo;
        }

        public async Task<VehicleModelRepo> GetById(int id)
        {
            VehicleModel vehicleModel = await vehicleModelSet.FindAsync(id);
            return mapper.Map<VehicleModelRepo>(vehicleModel);
        }

        public async Task<VehicleMakeRepo> GetVehicleMake(int id)
        {
            VehicleModel vehicleModel = await vehicleModelSet.FindAsync(id);
            if(vehicleModel != null)
            {
                vehicleModelSet.Attach(vehicleModel);
                return mapper.Map<VehicleMakeRepo>(vehicleModel.VehicleMake);
            }
            else
            {
                return null;
            }
            
        }

        public async Task<bool> Update(VehicleModelRepo entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleModel vehicleModel = mapper.Map<VehicleModel>(entity);
                var updatedVehicleModel = await vehicleModelSet.FindAsync(entity.Id);
                if (updatedVehicleModel != null)
                {
                    updatedVehicleModel.Name = vehicleModel.Name;
                    updatedVehicleModel.Abrv = vehicleModel.Abrv;
                    provjera = true;
                }

            }
            return provjera;
        }
    }
}
