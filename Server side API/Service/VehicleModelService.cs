using AutoMapper;
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
            foreach (VehicleModelRepo item in vehicleModelRepo)
            {
                vehicleModelServ.Add(mapper.Map<VehicleModelServ>(item));
            }
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
    }
}
