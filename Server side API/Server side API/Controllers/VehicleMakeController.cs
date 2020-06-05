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
        public async Task<IHttpActionResult> GetAll()
        {
            IEnumerable<VehicleMakeServ> vehicleMakes = await vehicleMakeService.Get();
            List<VehicleMake> makes = new List<VehicleMake>();
            foreach (VehicleMakeServ item in vehicleMakes)
            {
                makes.Add(mapper.Map<VehicleMake>(item));
            }
            return Ok(makes);
        }

        [Route("api/vehiclemakes/id")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            VehicleMakeServ vehicleMake = await vehicleMakeService.GetById(id);
            VehicleMake make = mapper.Map<VehicleMake>(vehicleMake);
            return Ok(make);
        }

        [Route("api/vehiclemakes")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Delete(vehicleMakeServ);
            if (provjera)
                return Ok("Vehicle make is successfully deleted!");
            else
                return Ok("Vehicle make is not deleted!");
        }

        [Route("api/vehiclemakes/")]
        [HttpPost]
        public async Task<IHttpActionResult> Add(VehicleMake vehicleMake)
        {
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Add(vehicleMakeServ);
            if (provjera)
                return Ok("Vehicle make is successfuly created!");
            else
                return Ok("Vehicle make is not created!");
        }

        [Route("api/vehiclemakes")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(VehicleMake vehicleMake)
        {
            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await vehicleMakeService.Update(vehicleMakeServ);
            if (provjera)
                return Ok("Vehicle make is succssesfuly!");
            else
                return Ok("Vehicle make is not updated!");
        }

        [Route("api/vehiclemakes/models")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModels(int id)
        {
            IEnumerable<VehicleModelServ> vehicleModelServs = await vehicleMakeService.GetModels(id);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            foreach (VehicleModelServ item in vehicleModelServs)
            {
                vehicleModels.Add(mapper.Map<VehicleModel>(item));
            }
            return Ok(vehicleModels);
        }
    }
}
