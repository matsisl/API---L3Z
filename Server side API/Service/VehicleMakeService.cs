using AutoMapper;
using Common;
using Repository;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VehicleMakeService : IService<VehicleMakeServ>
    {
        private UnitOfWork unitOfWork { get; }
        private IMapper mapper { get; }

        public VehicleMakeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Add(VehicleMakeServ entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMakeRepo vehicleMake = mapper.Map<VehicleMakeRepo>(entity);
                provjera = await unitOfWork.VehicleMakeRepository.Add(vehicleMake);
                if (provjera)
                {
                    int i = unitOfWork.Complete();
                }
            }
            return provjera;
        }

        public async Task<bool> Delete(VehicleMakeServ entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMakeRepo vehicleMakeRepo = mapper.Map<VehicleMakeRepo>(entity);
                provjera = await unitOfWork.VehicleMakeRepository.Delete(vehicleMakeRepo);
                if(provjera)
                    unitOfWork.Complete();
            }
            return provjera;
        }

        public async Task<IEnumerable<VehicleMakeServ>> Get()
        {
            IEnumerable<VehicleMakeRepo> vehicleMakesRepo = await unitOfWork.VehicleMakeRepository.GetAll();
            List<VehicleMakeServ> vehicleMakesServ = new List<VehicleMakeServ>();
            vehicleMakesServ = mapper.Map<IEnumerable<VehicleMakeRepo>, List<VehicleMakeServ>>(vehicleMakesRepo);
            return vehicleMakesServ;
        }

        public async Task<bool> Update(VehicleMakeServ entity)
        {
            bool provjera = false;
            if (entity != null){
                VehicleMakeRepo vehicleMakeRepo = mapper.Map<VehicleMakeRepo>(entity);
                provjera = await unitOfWork.VehicleMakeRepository.Update(vehicleMakeRepo);
                if (provjera)
                    unitOfWork.Complete();
            }
            return provjera;
        }

        public async Task<VehicleMakeServ> GetById(int id)
        {
            VehicleMakeRepo vehicleMakeRepo = await unitOfWork.VehicleMakeRepository.GetById(id);
            return mapper.Map<VehicleMakeServ>(vehicleMakeRepo);
        }

        public async Task<IEnumerable<VehicleModelServ>> GetModels(int id)
        {
            IEnumerable<VehicleModelRepo> vehicleModelRepos = await unitOfWork.VehicleMakeRepository.GetVehicleModels(id);
            List<VehicleModelServ> vehicleModelServs = new List<VehicleModelServ>();
            vehicleModelServs = mapper.Map<IEnumerable<VehicleModelRepo>, List<VehicleModelServ>>(vehicleModelRepos);
            return vehicleModelServs;
        }

        public async Task<IEnumerable<VehicleMakeServ>> Sort(TypeOfSorting typeOfSorting)
        {
            List<VehicleMakeServ> vehicleMakeServs = new List<VehicleMakeServ>();
            IEnumerable<VehicleMakeRepo> vehicleMakeRepos = await unitOfWork.VehicleMakeRepository.Sort(typeOfSorting);
            vehicleMakeServs = mapper.Map<IEnumerable<VehicleMakeRepo>, List<VehicleMakeServ>>(vehicleMakeRepos);
            return vehicleMakeServs;
        }

        public async Task<IEnumerable<VehicleMakeServ>> Filter(string filter)
        {
            List<VehicleMakeServ> vehicleMakeServs = new List<VehicleMakeServ>();
            IEnumerable<VehicleMakeRepo> vehicleMakeRepos = await unitOfWork.VehicleMakeRepository.Filter(filter);
            vehicleMakeServs = mapper.Map<IEnumerable<VehicleMakeRepo>, List<VehicleMakeServ>>(vehicleMakeRepos);
            return vehicleMakeServs;
        }

        public async Task<IEnumerable<VehicleMakeServ>> Paging(int pageSize, int pageIndex)
        {
            List<VehicleMakeServ> vehicleMakeServs = new List<VehicleMakeServ>();
            IEnumerable<VehicleMakeRepo> vehicleMakeRepos = await unitOfWork.VehicleMakeRepository.Paging(pageSize, pageIndex);
            vehicleMakeServs = mapper.Map<IEnumerable<VehicleMakeRepo>, List<VehicleMakeServ>>(vehicleMakeRepos);
            return vehicleMakeServs;
        }
    }
}
