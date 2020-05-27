using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IVehicleMakeRepository : IRepository<VehicleMakeRepo>
    {
        Task<IEnumerable<VehicleModelRepo>> GetVehicleModels(int id);
    }
}
