using Autofac;
using AutoMapper;
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
using System.Web.UI.WebControls;

namespace Server_side_API.Controllers
{
    public class VehicleModelController : ApiController
    {
        private VehicleModelService VehicleModelService;
        private IMapper mapper;
        private const string baseRoute = "api/vehiclemodels";


        public VehicleModelController()
        {
            VehicleModelService = AutofacConfig.Container.Resolve<VehicleModelService>();
            mapper = AutofacConfig.Container.Resolve<IMapper>();
        }

        [Route(baseRoute)]
        [HttpGet]
        public async Task<IEnumerable<VehicleModel>> GetAll()
        {
            IEnumerable<VehicleModelServ> vehicleModels = await VehicleModelService.Get();
            List<VehicleModel> models = new List<VehicleModel>();
            foreach (VehicleModelServ item in vehicleModels)
            {
                models.Add(mapper.Map<VehicleModel>(item));
            }
            return models;
        }

        [Route(baseRoute+"/id")]
        [HttpGet]
        public async Task<VehicleModel> GetById(int id)
        {
            VehicleModelServ vehicleModel = await VehicleModelService.GetById(id);
            return mapper.Map<VehicleModel>(vehicleModel);
        }

        [Route(baseRoute)]
        [HttpDelete]
        public async Task<bool> Delete(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Delete(vehicleModelServ);
            return provjera;
        }

        [Route(baseRoute)]
        [HttpPost]
        public async Task<bool> Add(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Add(vehicleModelServ);
            return provjera;
        }

        [Route(baseRoute)]
        [HttpPut]
        public async Task<bool> Update(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Update(vehicleModelServ);
            return provjera;
        }

        [Route(baseRoute+"/make")]
        [HttpGet]
        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            VehicleMakeServ vehicleMake = await VehicleModelService.GetMake(id);
            return mapper.Map<VehicleMake>(vehicleMake); 
        }
    }
}
