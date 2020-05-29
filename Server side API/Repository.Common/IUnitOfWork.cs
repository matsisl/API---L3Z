using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWork:IDisposable
    {
        IVehicleMakeRepository VehicleMakeRepository { get; }
        IVehicleModelRepository VehicleModelRepository { get; }
        int Complete();
        void Dispose();
    }
}
