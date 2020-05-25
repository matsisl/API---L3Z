using AutoMapper;
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
        private IUnitOfWork unitOfWork { get; }
        private IMapper mapper { get; }

        public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public Task<bool> Add(VehicleMakeServ entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(VehicleMakeServ entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleMakeServ>> Get()
        {
            IEnumerable<VehicleMakeRepo> vehicleMakesRepo = await unitOfWork.VehicleMakeRepository.GetAll();
            List<VehicleMakeServ> vehicleMakesServ = new List<VehicleMakeServ>();
            foreach (VehicleMakeRepo item in vehicleMakesRepo)
            {
                vehicleMakesServ.Add(mapper.Map<VehicleMakeServ>(item));
            }
            return vehicleMakesServ;
        }

        public Task<bool> Update(VehicleMakeServ entity)
        {
            throw new NotImplementedException();
        }
    }
}
