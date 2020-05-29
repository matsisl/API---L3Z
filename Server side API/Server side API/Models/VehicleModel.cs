using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_side_API.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }

        public VehicleModel(string name, string abrv, int makeId)
        {
            Name = name;
            Abrv = abrv;
            MakeId = makeId;
        }
    }
}