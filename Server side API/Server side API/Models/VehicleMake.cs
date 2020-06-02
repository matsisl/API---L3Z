using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_side_API.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public List<VehicleModel> VehicleModels { get; set; }

        public VehicleMake(string name, string abrv, List<VehicleModel> vehicleModels)
        {
            Name = name;
            Abrv = abrv;
            VehicleModels = vehicleModels;
        }

        public VehicleMake()
        {
        }
    }
}