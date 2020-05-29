using Autofac;
using DAL;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeRepository>().As<IRepository<VehicleMakeRepo>>();
            builder.RegisterType<VehicleModelRepository>().As<IRepository<VehicleModelRepo>>();
            builder.RegisterType<IVehicleMakeRepository>();
            builder.RegisterType<IVehicleModelRepository>();
            builder.RegisterType<UnitOfWork>();
            builder.RegisterType<VehicleContext>();
        }
    }
}
