namespace DAL
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class VehicleContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public VehicleContext() : base("VehicleModel")
        {
        }
    }
}