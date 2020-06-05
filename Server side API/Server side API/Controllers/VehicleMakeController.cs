using Autofac;
using AutoMapper;
using Common;
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
        private VehicleMakeService VehicleMakeService;
        private IMapper mapper;

        public VehicleMakeController()
        {
            VehicleMakeService = AutofacConfig.Container.Resolve<VehicleMakeService>();
            mapper = AutofacConfig.Container.Resolve<IMapper>();
        }

        [Route("api/vehiclemakes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            IEnumerable<VehicleMakeServ> vehicleMakes = await VehicleMakeService.Get();
            List<VehicleMake> makes = new List<VehicleMake>();
            makes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakes);
            return Ok(makes);
        }

        [Route("api/vehiclemakes/id")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            VehicleMakeServ vehicleMake = await VehicleMakeService.GetById(id);
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
            bool provjera = await VehicleMakeService.Delete(vehicleMakeServ);
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
            bool provjera = await VehicleMakeService.Add(vehicleMakeServ);
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
            bool provjera = await VehicleMakeService.Update(vehicleMakeServ);
            if (provjera)
                return Ok("Vehicle make is succssesfuly!");
            else
                return Ok("Vehicle make is not updated!");
        }

        [Route("api/vehiclemakes/models")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModels(int id)
        {
            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleMakeService.GetModels(id);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Ok(vehicleModels);
        }

        [Route("api/vehiclemakes/sort/{sort}")]
        [HttpGet]
        public async Task<IHttpActionResult> Sort(int sort)
        {
            TypeOfSorting typeOfSorting = TypeOfSorting.Asc;
            if (sort <= 0)
                typeOfSorting = TypeOfSorting.Desc;
            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Sort(typeOfSorting);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Ok(vehicleMakes);
        }

        [Route("api/vehiclemakes/filter/{filter}")]
        [HttpGet]
        public async Task<IHttpActionResult> Filter(string filter)
        {
            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Filter(filter);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Ok(vehicleMakes);
        }

        [Route("api/vehiclemakes/paging/{pageSize}/{pageIndex}")]
        [HttpGet]
        public async Task<IHttpActionResult> Paging(int pageSize, int pageIndex)
        {
            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Paging(pageSize, pageIndex);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Ok(vehicleMakes);
        }
    }
}
