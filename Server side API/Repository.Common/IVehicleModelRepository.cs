using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IVehicleModelRepository : IRepository<VehicleModelRepo>
    {
        Task<VehicleMakeRepo> GetVehicleMake(int id);
    }
}
