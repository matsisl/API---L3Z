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
    }
}
