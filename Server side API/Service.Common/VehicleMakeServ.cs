using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public class VehicleMakeServ
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public List<VehicleModelServ> VehicleModels { get; set; }
    }
}
