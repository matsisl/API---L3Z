using AutoMapper;
using Common;
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
                if (IsNotExist(vehicleMake))
                {
                    vehicleMake.VehicleModels.Clear();
                    VehicleMake make = vehiclesSet.Add(vehicleMake);
                    if (make != null)
                    {
                        provjera = true;
                    }
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
                VehicleMake make = await vehiclesSet.FindAsync(vehicleMake.Id);                
                if (make != null && make.VehicleModels.Count == 0)
                {
                    vehiclesSet.Remove(make);
                    provjera = true;
                }
            }
            return provjera;
        }

        public async Task<IEnumerable<VehicleMakeRepo>> GetAll()
        {
            List<VehicleMake> vehicleMakes = await vehiclesSet.ToListAsync();
            List<VehicleMakeRepo> vehicleMakesRepo = new List<VehicleMakeRepo>();
            vehicleMakesRepo = mapper.Map<List<VehicleMake>, List<VehicleMakeRepo>>(vehicleMakes);
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
            vehicleModelsRepo = mapper.Map<ICollection<VehicleModel>, List<VehicleModelRepo>>(vehicleMakes.VehicleModels);
            return vehicleModelsRepo;
        }

        public async Task<bool> Update(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                var updatedVehicleMake = await vehiclesSet.FindAsync(entity.Id);
                if (updatedVehicleMake != null)
                {
                    if (IsNameNotExist(updatedVehicleMake.Id, vehicleMake.Name))
                    {
                        updatedVehicleMake.Name = vehicleMake.Name;
                        provjera = true;
                    }
                    if (IsAbrvNotExist(updatedVehicleMake.Id, vehicleMake.Abrv))
                    {
                        updatedVehicleMake.Abrv = vehicleMake.Abrv;
                        provjera = true;
                    }
                }                
                
            }
            return provjera;
        }

        private bool IsNotExist(VehicleMake vehicleMake)
        {
            if (!String.IsNullOrWhiteSpace(vehicleMake.Name) && !String.IsNullOrWhiteSpace(vehicleMake.Abrv))
            {
                string name = vehicleMake.Name.ToLower();
                string abrv = vehicleMake.Abrv.ToLower();
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Abrv.ToLower().Equals(abrv) || x.Name.ToLower().Equals(name)).ToList();
                if (vehicleMakes.Count == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool IsNameNotExist(int id, string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                name = name.ToLower();
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Name.ToLower().Equals(name) && x.Id==id).ToList();
                if (vehicleMakes.Count == 0)
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
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Abrv.ToLower().Equals(abrv) && x.Id==id).ToList();
                if (vehicleMakes.Count == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool IsModelsExist(List<VehicleModel> vehicleModels)
        {
            bool provjera = false;
            foreach (VehicleModel vehicleModel in vehicleModels)
            {
                provjera = vehiclesSet.Any<VehicleMake>(x => x.VehicleModels.Any<VehicleModel>(y => y.Name == vehicleModel.Name || y.Abrv == vehicleModel.Abrv) == false);
            }            
            return provjera;
        }

        public async Task<IEnumerable<VehicleMakeRepo>> Sort(TypeOfSorting typeOfSorting)
        {
            List<VehicleMakeRepo> vehicleMakes = new List<VehicleMakeRepo>();
            List<VehicleMake> vehicleMakesdb = new List<VehicleMake>();
            switch (typeOfSorting)
            {
                case (TypeOfSorting.Asc):
                    vehicleMakesdb = await vehiclesSet.OrderBy(x => x.Name).ToListAsync();
                    break;
                case (TypeOfSorting.Desc):
                    vehicleMakesdb = await vehiclesSet.OrderByDescending(x => x.Name).ToListAsync();
                    break;
            }
            vehicleMakes = mapper.Map<List<VehicleMake>, List<VehicleMakeRepo>>(vehicleMakesdb);
            return vehicleMakes;
        }

        public async Task<IEnumerable<VehicleMakeRepo>> Paging(int pageSize, int pageIndex)
        {
            if (pageIndex >= 0 && pageSize > 0)
            {
                int skip = pageSize * pageIndex;
                List<VehicleMakeRepo> vehicleMakes = new List<VehicleMakeRepo>();
                List<VehicleMake> makes = await vehiclesSet.OrderBy(x=>x.Id).Skip(skip).Take(pageSize).ToListAsync();
                vehicleMakes = mapper.Map<List<VehicleMake>, List<VehicleMakeRepo>>(makes);
                return vehicleMakes;
            }
            else
                return new List<VehicleMakeRepo>();
        }

        public async Task<IEnumerable<VehicleMakeRepo>> Filter(string filter)
        {
            List<VehicleMakeRepo> vehicleMakes = new List<VehicleMakeRepo>();
            List<VehicleMake> vehicleMakesdb = await vehiclesSet.Where(x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync();
            vehicleMakes = mapper.Map<List<VehicleMake>, List<VehicleMakeRepo>>(vehicleMakesdb);
            return vehicleMakes;
        }

        public async Task<IEnumerable<VehicleMakeRepo>> Get(Sorting sorting, Paging paging, Filtering filtering)
        {
            IQueryable<VehicleMake> vehicleMakes;
            if (sorting.TypeOfSorting == TypeOfSorting.Asc)
            {
                vehicleMakes = vehiclesSet.OrderBy(x => x.Name).Skip(paging.Offset()).Take(paging.PageSize);
            }
            else
            {
                vehicleMakes = vehiclesSet.OrderByDescending(x => x.Name).Skip(paging.Offset()).Take(paging.PageSize);
            }
            if (!String.IsNullOrWhiteSpace(filtering.Filter))
            {
                vehicleMakes = vehicleMakes.Where(x => x.Name.Contains(filtering.Filter) || x.Abrv.Contains(filtering.Filter));
            }
            List<VehicleMakeRepo> vehicleMakeRepos = new List<VehicleMakeRepo>();
            vehicleMakeRepos = mapper.Map<IEnumerable<VehicleMake>, List<VehicleMakeRepo>>(await vehicleMakes.ToListAsync());
            return vehicleMakeRepos;
        }
    }
}
