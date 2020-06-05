using Autofac;
using AutoMapper;
using Common;
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
        public async Task<IHttpActionResult> GetAll()
        {
            IEnumerable<VehicleModelServ> vehicleModels = await VehicleModelService.Get();
            List<VehicleModel> models = new List<VehicleModel>();
            foreach (VehicleModelServ item in vehicleModels)
            {
                models.Add(mapper.Map<VehicleModel>(item));
            }
            return Ok(models);
        }

        [Route(baseRoute+"/id")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            VehicleModelServ vehicleModel = await VehicleModelService.GetById(id);
            VehicleModel model = mapper.Map<VehicleModel>(vehicleModel);
            return Ok(model);
        }

        [Route(baseRoute)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Delete(vehicleModelServ);
            if (provjera)
                return Ok("Vehicle model is successfully deleted!");
            else
                return Ok("Vehicle model is not deleted!");
        }

        [Route(baseRoute)]
        [HttpPost]
        public async Task<IHttpActionResult> Add(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Add(vehicleModelServ);
            if (provjera)
                return Ok("Vehicle model is successfully created!");
            else
                return Ok("Vehicle model is not created!");
        }

        [Route(baseRoute)]
        [HttpPut]
        public async Task<IHttpActionResult> Update(VehicleModel vehicleModel)
        {
            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Update(vehicleModelServ);
            if (provjera)
                return Ok("Vehicle model is successfully updated!");
            else
                return Ok("Vehicle model is not updated!");
        }

        [Route(baseRoute+"/make")]
        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleMake(int id)
        {
            VehicleMakeServ vehicleMake = await VehicleModelService.GetMake(id);
            VehicleMake make = mapper.Map<VehicleMake>(vehicleMake);
            return Ok(make);
        }

        [Route(baseRoute+"/sort/{sort}")]
        [HttpGet]
        public async Task<IHttpActionResult> Sort(int sort)
        {
            TypeOfSorting typeOfSorting = TypeOfSorting.Asc;
            if (sort <= 0)
                typeOfSorting = TypeOfSorting.Desc;
            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Sort(typeOfSorting);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Ok(vehicleModels);
        }

        [Route(baseRoute+"/filter/{filter}")]
        [HttpGet]
        public async Task<IHttpActionResult> Filter(string filter)
        {
            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Filter(filter);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Ok(vehicleModels);
        }

        [Route(baseRoute+"/paging/{pageSize}/{pageIndex}")]
        [HttpGet]
        public async Task<IHttpActionResult> Paging(int pageSize, int pageIndex)
        {
            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Paging(pageSize, pageIndex);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Ok(vehicleModels);
        }
    }
}
