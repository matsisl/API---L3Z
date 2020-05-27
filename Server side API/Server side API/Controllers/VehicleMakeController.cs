using Autofac;
using AutoMapper;
using Repository.Common;
using Server_side_API.Models;
using Server_side_API.Utils;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Server_side_API.Controllers
{
    public class VehicleMakeController : ApiController
    {
        private VehicleMakeService vehicleMakeService;
        private IMapper mapper;

        public VehicleMakeController()
        {
            vehicleMakeService = AutofacConfig.Container.Resolve<VehicleMakeService>();
            mapper = AutofacConfig.Container.Resolve<IMapper>();
        }

        [Route("api/vehiclemakes")]
        [HttpGet]
        public async Task<IEnumerable<VehicleMake>> GetAll()
        {
            IEnumerable<VehicleMakeServ> vehicleMakes = await vehicleMakeService.Get();
            List<VehicleMake> makes = new List<VehicleMake>();
            foreach (VehicleMakeServ item in vehicleMakes)
            {
                makes.Add(mapper.Map<VehicleMake>(item));
            }
            return makes;
        }

        [Route("api/vehiclemakes/id")]
        [HttpGet]
        public async Task<VehicleMake> GetById(int id)
        {
            VehicleMakeServ vehicleMake = await vehicleMakeService.GetById(id);
            return mapper.Map<VehicleMake>(vehicleMake);
        }

        [Route("api/vehiclemakes")]
        [HttpDelete]
        public async Task<bool> Delete(VehicleMake vehicleMake)
        {
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Delete(vehicleMakeServ);
            return provjera;
        }

        [Route("api/vehiclemakes/")]
        [HttpPost]
        public async Task<bool> Add(VehicleMake vehicleMake)
        {
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Add(vehicleMakeServ);
            return provjera;
        }

        [Route("api/vehiclemakes")]
        [HttpPut]
        public async Task<bool> Update(VehicleMake vehicleMake)
        {
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Update(vehicleMakeServ);
            return provjera;
        }

        [Route("api/vehiclemakes/models")]
        [HttpGet]
        public async Task<IEnumerable<VehicleModel>> GetModels(int id)
        {
            IEnumerable<VehicleModelServ> vehicleModelServs = await vehicleMakeService.GetModels(id);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            foreach (VehicleModelServ item in vehicleModelServs)
            {
                vehicleModels.Add(mapper.Map<VehicleModel>(item));
            }
            return vehicleModels;
        }
    }
}
