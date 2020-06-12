using AutoMapper;
using Common;
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
                if (IsNotExist(vehicleModel) && IsMakeExist(vehicleModel.MakeId))
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
            vehicleModelsRepo = mapper.Map<List<VehicleModel>, List<VehicleModelRepo>>(vehicleModels);
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
                    if (IsNameNotExist(updatedVehicleModel.Id, vehicleModel.Name))
                    {
                        updatedVehicleModel.Name = vehicleModel.Name;
                    }
                    else
                    {
                        provjera = false;
                    }
                    if (IsAbrvNotExist(updatedVehicleModel.Id, vehicleModel.Abrv))
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        private bool IsNameNotExist(int id, string newName)
        {
            if (!String.IsNullOrWhiteSpace(newName))
            {
                newName = newName.ToLower();
                List<VehicleModel> vehicleModels = vehicleModelSet.Where(x => x.Name.ToLower().Equals(newName) && x.Id!=id ).ToList();
                if (vehicleModels.Count == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool IsAbrvNotExist(int id, string abrv)
        {
            if (!String.IsNullOrWhiteSpace(abrv))
            {
                abrv = abrv.ToLower();
                List<VehicleModel> vehicleModels = vehicleModelSet.Where(x => x.Abrv.ToLower().Equals(abrv) && x.Id!=id).ToList();
                if (vehicleModels.Count == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        private bool IsMakeExist(int id)
        {
            VehicleMake vehicleMake = Context.VehicleMakes.Find(id);
            if (vehicleMake == null)
                return false;
            else
                return true;
        }

        public async Task<IEnumerable<VehicleModelRepo>> Sort(TypeOfSorting typeOfSorting)
        {
            List<VehicleModelRepo> vehicleModels = new List<VehicleModelRepo>();
            List<VehicleModel> vehicleModeldb = new List<VehicleModel>();
            switch (typeOfSorting)
            {
                case (TypeOfSorting.Asc):
                    vehicleModeldb = await vehicleModelSet.OrderBy(x => x.Name).ToListAsync();
                    break;
                case (TypeOfSorting.Desc):
                    vehicleModeldb = await vehicleModelSet.OrderByDescending(x => x.Name).ToListAsync();
                    break;
            }
            vehicleModels = mapper.Map<List<VehicleModel>, List<VehicleModelRepo>>(vehicleModeldb);
            return vehicleModels;
        }

        public async Task<IEnumerable<VehicleModelRepo>> Paging(int pageSize, int pageIndex)
        {
            if (pageIndex >= 0 && pageSize > 0)
            {
                int skip = pageSize * pageIndex;
                List<VehicleModelRepo> vehicleModels = new List<VehicleModelRepo>();
                List<VehicleModel> models = await vehicleModelSet.OrderBy(x=>x.Id).Skip(skip).Take(pageSize).ToListAsync();
                vehicleModels = mapper.Map<List<VehicleModel>, List<VehicleModelRepo>>(models);
                return vehicleModels;
            }
            else
                return new List<VehicleModelRepo>();
        }

        public async Task<IEnumerable<VehicleModelRepo>> Filter(string filter)
        {
            List<VehicleModelRepo> vehicleModels = new List<VehicleModelRepo>();
            List<VehicleModel> vehicleMakesdb = await vehicleModelSet.Where(x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync();
            vehicleModels = mapper.Map<List<VehicleModel>, List<VehicleModelRepo>>(vehicleMakesdb);
            return vehicleModels;
        }

        public async Task<IEnumerable<VehicleModelRepo>> Get(Sorting sorting, Paging paging, Filtering filtering)
        {
            IQueryable<VehicleModel> vehicleModels;
            switch (sorting.TypeOfSorting)
            {
                case TypeOfSorting.Asc:
                    vehicleModels = vehicleModelSet.OrderBy(x => x.Name).Skip(paging.Offset()).Take(paging.PageSize);
                    break;
                case TypeOfSorting.Desc:
                    vehicleModels = vehicleModelSet.OrderByDescending(x => x.Name).Skip(paging.Offset()).Take(paging.PageSize);
                    break;
                default:
                    vehicleModels = vehicleModelSet.OrderBy(x => x.Id).Skip(paging.Offset()).Take(paging.PageSize);
                    break;
            }
            if (!String.IsNullOrWhiteSpace(filtering.Filter))
            {
                vehicleModels = vehicleModels.Where(x => x.Name.Contains(filtering.Filter) || x.Abrv.Contains(filtering.Filter));
            }
            List<VehicleModelRepo> vehicleModelRepos = new List<VehicleModelRepo>();
            vehicleModelRepos = mapper.Map<IEnumerable<VehicleModel>, List<VehicleModelRepo>>(await vehicleModels.ToListAsync());
            return vehicleModelRepos;
        }
    }
}
