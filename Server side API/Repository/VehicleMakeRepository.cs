﻿using AutoMapper;
using DAL;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private DbSet<VehicleMake> vehiclesSet { get; }
        private IMapper mapper;
        private VehicleContext VehicleContext;

        public VehicleMakeRepository(VehicleContext vehicleContext,IMapper mapper)
        {
            this.vehiclesSet = vehicleContext.VehicleMakes;
            this.mapper = mapper;
            VehicleContext = vehicleContext;
        }

        public async Task<bool> Add(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                if (IsNotExist(vehicleMake))
                {
                    VehicleMake make = vehiclesSet.Add(vehicleMake);
                    if (make != null)
                    {
                        provjera = true;
                    }
                }
            }
            return provjera;
        }

        public async Task<bool> Delete(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if (entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                VehicleMake make = await vehiclesSet.FindAsync(vehicleMake.Id);                
                if (make != null && make.VehicleModels.Count == 0)
                {
                    vehiclesSet.Remove(make);
                    provjera = true;
                }
            }
            return provjera;
        }

        public async Task<IEnumerable<VehicleMakeRepo>> GetAll()
        {
            List<VehicleMake> vehicleMakes = await vehiclesSet.ToListAsync();
            List<VehicleMakeRepo> vehicleMakesRepo = new List<VehicleMakeRepo>();
            foreach (VehicleMake item in vehicleMakes)
            {
                vehicleMakesRepo.Add(mapper.Map<VehicleMakeRepo>(item));
            }
            return vehicleMakesRepo;
        }

        public async Task<VehicleMakeRepo> GetById(int id)
        {
            VehicleMake vehicleMake = await vehiclesSet.FindAsync(id);
            return mapper.Map<VehicleMakeRepo>(vehicleMake);
        }

        public async Task<IEnumerable<VehicleModelRepo>> GetVehicleModels(int id)
        {
            VehicleMake vehicleMakes = await vehiclesSet.FindAsync(id);
            List<VehicleModelRepo> vehicleModelsRepo = new List<VehicleModelRepo>();
            foreach (VehicleModel item in vehicleMakes.VehicleModels)
            {
                vehicleModelsRepo.Add(mapper.Map<VehicleModelRepo>(item));
            }
            return vehicleModelsRepo;
        }

        public async Task<bool> Update(VehicleMakeRepo entity)
        {
            bool provjera = false;
            if(entity != null)
            {
                VehicleMake vehicleMake = mapper.Map<VehicleMake>(entity);
                var updatedVehicleMake = await vehiclesSet.FindAsync(entity.Id);
                if (updatedVehicleMake != null)
                {
                    if (IsNameNotExist(updatedVehicleMake.Id, vehicleMake.Name))
                    {
                        updatedVehicleMake.Name = vehicleMake.Name;
                        provjera = true;
                    }
                    if (IsAbrvNotExist(updatedVehicleMake.Id, vehicleMake.Abrv))
                    {
                        updatedVehicleMake.Abrv = vehicleMake.Abrv;
                        provjera = true;
                    }
                }                
                
            }
            return provjera;
        }

        private bool IsNotExist(VehicleMake vehicleMake)
        {
            if (!String.IsNullOrWhiteSpace(vehicleMake.Name) && !String.IsNullOrWhiteSpace(vehicleMake.Abrv))
            {
                string name = vehicleMake.Name.ToLower();
                string abrv = vehicleMake.Abrv.ToLower();
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Abrv.ToLower().Equals(abrv) || x.Name.ToLower().Equals(name)).ToList();
                if (vehicleMakes.Count == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool IsNameNotExist(int id, string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                name = name.ToLower();
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Name.ToLower().Equals(name) && x.Id==id).ToList();
                if (vehicleMakes.Count == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool IsAbrvNotExist(int id, string abrv)
        {
            if (!String.IsNullOrWhiteSpace(abrv))
            {
                abrv = abrv.ToLower();
                List<VehicleMake> vehicleMakes = vehiclesSet.Where(x => x.Abrv.ToLower().Equals(abrv) && x.Id==id).ToList();
                if (vehicleMakes.Count == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
