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
                if (!IsNotExist(vehicleModel) && IsMakeExist(vehicleModel.MakeId))
                {
                    VehicleModel model = vehicleModelSet.Add(vehicleModel);
                    if (model != null)
                    {
                        provjera = true;
                    }
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
            bool provjera = true;
            if (entity != null)
            {
                VehicleModel vehicleModel = mapper.Map<VehicleModel>(entity);
                var updatedVehicleModel = await vehicleModelSet.FindAsync(vehicleModel.Id);
                if (updatedVehicleModel != null )
                {
                    if (!IsNameNotExist(updatedVehicleModel.Id, vehicleModel.Name))
                    {
                        updatedVehicleModel.Name = vehicleModel.Name;
                    }
                    else
                    {
                        provjera = false;
                    }
                    if (!IsAbrvNotExist(updatedVehicleModel.Id, vehicleModel.Abrv))
                    {
                        updatedVehicleModel.Abrv = vehicleModel.Abrv;
                    }
                    else
                    {
                        provjera = false;
                    }
                    if (IsMakeExist(vehicleModel.MakeId))
                    { 
                        if(vehicleModel.MakeId!=0)
                            updatedVehicleModel.MakeId = vehicleModel.MakeId;
                    }
                    else
                    {
                        provjera = false;
                    }
                }
            }
            else
            {
                provjera = false;
            }
            return provjera;
        }

        private bool IsNotExist(VehicleModel vehicleModel)
        {
            if (!String.IsNullOrWhiteSpace(vehicleModel.Name) && !String.IsNullOrWhiteSpace(vehicleModel.Abrv))
            {
                string name = vehicleModel.Name.ToLower();
                string abrv = vehicleModel.Abrv.ToLower();
                List<VehicleModel> vehicleModels = vehicleModelSet.Where(x => x.Name.ToLower().Equals(name) || x.Abrv.ToLower().Equals(abrv)).ToList();
                if (vehicleModels.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }
        private bool IsNameNotExist(int id, string newName)
        {
            if (!String.IsNullOrWhiteSpace(newName))
            {
                newName = newName.ToLower();
                List<VehicleModel> vehicleModels = vehicleModelSet.Where(x => x.Name.ToLower().Equals(newName) && x.Id!=id ).ToList();
                if (vehicleModels.Count == 0)
                    return false;
                else
                    return true;
            }
            return true;
        }

        private bool IsAbrvNotExist(int id, string abrv)
        {
            if (!String.IsNullOrWhiteSpace(abrv))
            {
                abrv = abrv.ToLower();
                List<VehicleModel> vehicleModels = vehicleModelSet.Where(x => x.Abrv.ToLower().Equals(abrv) && x.Id!=id).ToList();
                if (vehicleModels.Count == 0)
                    return false;
                else
                    return true;
            }
            return true;
        }
        private bool IsMakeExist(int id)
        {
            VehicleMake vehicleMake = Context.VehicleMakes.Find(id);
            if (vehicleMake == null)
                return false;
            else
                return true;
        }
    }
}
