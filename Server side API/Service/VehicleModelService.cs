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
    public class VehicleModelService : IService<VehicleModelServ>
    {
        private UnitOfWork unitOfWork { get; }
        private IMapper mapper { get; }

        public VehicleModelService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Add(VehicleModelServ entity)
        {
            bool provjera = false;
            if (entity != null || entity.MakeId!=0)
            {
                VehicleModelRepo vehicleModel = mapper.Map<VehicleModelRepo>(entity);
                provjera = await unitOfWork.VehicleModelRepository.Add(vehicleModel);
                if (provjera)
                {
                    unitOfWork.Complete();
                }
            }
            return provjera;
        }

        public async Task<bool> Delete(VehicleModelServ entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleModelRepo vehicleModelRepo = mapper.Map<VehicleModelRepo>(entity);
                provjera = await unitOfWork.VehicleModelRepository.Delete(vehicleModelRepo);
                if (provjera)
                    unitOfWork.Complete();
            }
            return provjera;
        }

        public async Task<IEnumerable<VehicleModelServ>> Get()
        {
            IEnumerable<VehicleModelRepo> vehicleModelRepo = await unitOfWork.VehicleModelRepository.GetAll();
            List<VehicleModelServ> vehicleModelServ = new List<VehicleModelServ>();
            vehicleModelServ = mapper.Map<IEnumerable<VehicleModelRepo>, List<VehicleModelServ>>(vehicleModelRepo);
            return vehicleModelServ;
        }

        public async Task<VehicleModelServ> GetById(int id)
        {
            VehicleModelRepo vehicleModelRepo = await unitOfWork.VehicleModelRepository.GetById(id);
            return mapper.Map<VehicleModelServ>(vehicleModelRepo);
        }

        public async Task<bool> Update(VehicleModelServ entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleModelRepo vehicleModelRepo = mapper.Map<VehicleModelRepo>(entity);
                provjera = await unitOfWork.VehicleModelRepository.Update(vehicleModelRepo);
                if (provjera)
                    unitOfWork.Complete();
            }
            return provjera;
        }
        public async Task<VehicleMakeServ> GetMake(int id)
        {
            VehicleMakeRepo vehicleMakeRepo = await unitOfWork.VehicleModelRepository.GetVehicleMake(id);
            return mapper.Map<VehicleMakeServ>(vehicleMakeRepo);
        }

        public async Task<IEnumerable<VehicleModelServ>> Sort(TypeOfSorting typeOfSorting)
        {
            List<VehicleModelServ> vehicleModelServs = new List<VehicleModelServ>();
            IEnumerable<VehicleModelRepo> vehicleModelRepos = await unitOfWork.VehicleModelRepository.Sort(typeOfSorting);
            vehicleModelServs = mapper.Map<IEnumerable<VehicleModelRepo>, List<VehicleModelServ>>(vehicleModelRepos);
            return vehicleModelServs;
        }

        public async Task<IEnumerable<VehicleModelServ>> Filter(string filter)
        {
            List<VehicleModelServ> vehicleModelServs = new List<VehicleModelServ>();
            IEnumerable<VehicleModelRepo> vehicleModelRepos = await unitOfWork.VehicleModelRepository.Filter(filter);
            vehicleModelServs = mapper.Map<IEnumerable<VehicleModelRepo>, List<VehicleModelServ>>(vehicleModelRepos);
            return vehicleModelServs;
        }

        public async Task<IEnumerable<VehicleModelServ>> Paging(int pageSize, int pageIndex)
        {
            List<VehicleModelServ> vehicleModelServs = new List<VehicleModelServ>();
            IEnumerable<VehicleModelRepo> vehicleModelRepos = await unitOfWork.VehicleModelRepository.Paging(pageSize,pageIndex);
            vehicleModelServs = mapper.Map<IEnumerable<VehicleModelRepo>, List<VehicleModelServ>>(vehicleModelRepos);
            return vehicleModelServs;
        }
    }
}
