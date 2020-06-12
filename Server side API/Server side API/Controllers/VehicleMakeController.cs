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
using System.Web.Http.Results;

namespace Server_side_API.Controllers
{
    public class VehicleMakeController : ApiController
    {
        private VehicleMakeService VehicleMakeService;
        private IMapper mapper;
        private const string baseRoute="api/vehiclemakes";

        public VehicleMakeController()
        {
            VehicleMakeService = AutofacConfig.Container.Resolve<VehicleMakeService>();
            mapper = AutofacConfig.Container.Resolve<IMapper>();
        }

        [Route(baseRoute+"/{pageIndex?}/{pageSize?}/{sort?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int pageIndex=1, int pageSize=10, string filter="", TypeOfSorting sort = TypeOfSorting.Asc)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            PageResult<VehicleMake> result = new PageResult<VehicleMake>(pageIndex, pageSize, sort, filter);            
            if (result.Paging.Invalidete())
            {
                var url = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/" + baseRoute;
                result.GenerateNextPage(url);
                IEnumerable<VehicleMakeServ> vehicleMakes = await VehicleMakeService.Get(result.Filtering, result.Paging, result.Sorting);
                result.Results = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakes);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route(baseRoute)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleMakeServ vehicleMake = await VehicleMakeService.GetById(id);
            VehicleMake make = mapper.Map<VehicleMake>(vehicleMake);
            return Request.CreateResponse(HttpStatusCode.OK, make);
        }

        [Route(baseRoute)]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await VehicleMakeService.Delete(vehicleMakeServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make is successfully deleted!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle make is not deleted!");
        }

        [Route(baseRoute)]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await VehicleMakeService.Add(vehicleMakeServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make is successfuly created!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle make is not created!");
        }

        [Route(baseRoute)]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleMakeServ vehicleMakeServ = mapper.Map<VehicleMakeServ>(vehicleMake);
            bool provjera = await VehicleMakeService.Update(vehicleMakeServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make is succssesfuly updated!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle make is not updated!");
        }

        [Route(baseRoute+"/models")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetModels(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleMakeService.GetModels(id);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleModels);
        }

        [Route(baseRoute+"/sort/{sort}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Sort(int sort)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            TypeOfSorting typeOfSorting = TypeOfSorting.Asc;
            if (sort <= 0)
                typeOfSorting = TypeOfSorting.Desc;
            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Sort(typeOfSorting);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleMakes);
        }

        [Route(baseRoute+"/filter/{filter}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Filter(string filter)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Filter(filter);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleMakes);
        }

        [Route(baseRoute+"/paging/{pageSize}/{pageIndex}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Paging(int pageSize, int pageIndex)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            IEnumerable<VehicleMakeServ> vehicleMakeServs = await VehicleMakeService.Paging(pageSize, pageIndex);
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            vehicleMakes = mapper.Map<IEnumerable<VehicleMakeServ>, List<VehicleMake>>(vehicleMakeServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleMakes);
        }
    }
}
