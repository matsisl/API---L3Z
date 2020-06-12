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

        [Route(baseRoute+"/{pageIndex?}/{pageSize?}/{sort?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int pageIndex = 1, int pageSize = 10, string filter = "", TypeOfSorting sort = TypeOfSorting.Asc)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            PageResult<VehicleModel> result = new PageResult<VehicleModel>(pageIndex, pageSize, sort, filter);
            if (result.Paging.Invalidete())
            {
                var url = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/" + baseRoute;
                result.GenerateNextPage(url);
                IEnumerable<VehicleModelServ> vehicleModels = await VehicleModelService.Get(result.Filtering, result.Paging, result.Sorting);
                result.Results = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModels);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route(baseRoute+"/id")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleModelServ vehicleModel = await VehicleModelService.GetById(id);
            VehicleModel model = mapper.Map<VehicleModel>(vehicleModel);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [Route(baseRoute)]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Delete(vehicleModelServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle model is successfully deleted!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle model is not deleted!");
        }

        [Route(baseRoute)]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Add(vehicleModelServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle model is successfully created!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle model is not created!");
        }

        [Route(baseRoute)]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleModelServ vehicleModelServ = mapper.Map<VehicleModelServ>(vehicleModel);
            bool provjera = await VehicleModelService.Update(vehicleModelServ);
            if (provjera)
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle model is successfully updated!");
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Vehicle model is not updated!");
        }

        [Route(baseRoute+"/make")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetVehicleMake(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            VehicleMakeServ vehicleMake = await VehicleModelService.GetMake(id);
            VehicleMake make = mapper.Map<VehicleMake>(vehicleMake);
            return Request.CreateResponse(HttpStatusCode.OK, make);
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
            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Sort(typeOfSorting);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleModels);
        }

        [Route(baseRoute+"/filter/{filter}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Filter(string filter)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Filter(filter);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleModels);
        }

        [Route(baseRoute+"/paging/{pageSize}/{pageIndex}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Paging(int pageSize, int pageIndex)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data");
            }

            IEnumerable<VehicleModelServ> vehicleModelServs = await VehicleModelService.Paging(pageSize, pageIndex);
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            vehicleModels = mapper.Map<IEnumerable<VehicleModelServ>, List<VehicleModel>>(vehicleModelServs);
            return Request.CreateResponse(HttpStatusCode.OK, vehicleModels);
        }
    }
}
