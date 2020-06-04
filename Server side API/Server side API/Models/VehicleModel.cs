using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Server_side_API.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        [JsonIgnore]
        public VehicleMake VehicleMake { get; set; }
        public VehicleModel(string name, string abrv)
        {
            Name = name;
            Abrv = abrv;
        }
    }
}