using Racing.App.Models;
using Racing.Model;
using Racing.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Racing.App.Helper;

namespace Racing.App.Services
{
    public class RacingService : IRacingService
    {
        private readonly IVehicleRepo _vehicleRepository;
        private readonly int allowedVehicleOnTrack = 0;
        public RacingService(IVehicleRepo vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            allowedVehicleOnTrack = _vehicleRepository.allowedVehicleOnTrack == 0 ? Convert.ToInt32(Utility.GetConfigValue("TotalAllowedVehicleOnTrack"))
                : vehicleRepository.allowedVehicleOnTrack;
        }

        public ResponseMessage AddVehicles(VehicleDto vehicleModel)
        {
            try
            {
                //Business logic to verify track overload status
                bool checkTrackOverload = _vehicleRepository.FindAll().Count() >= allowedVehicleOnTrack ? true : false;

                //Business logic to check vehicle inspection status
                if (!VehicleInspection(vehicleModel))
                    return ResponseMessage.InspectionFail;

                Vehicle vehicle = new Vehicle();
                vehicle.Id = Guid.NewGuid();
                vehicle.Name = vehicleModel.Name;
                vehicle.Type = vehicleModel.Type;
                vehicle.isTow = vehicleModel.isTow;
                vehicle.Lift = vehicleModel.Lift;
                vehicle.Tire = vehicleModel.Tire;
                vehicle.Image = vehicleModel.Image;

                //Business logic for show track overload when track is full
                if (checkTrackOverload)
                    return ResponseMessage.Overloaded;
                //Business logic to save vehicle data in database
                _vehicleRepository.Add(vehicle);
                _vehicleRepository.Database.SaveChanges();
                return ResponseMessage.Inserted;
            }
            catch (Exception)
            {
                return ResponseMessage.None;
            }
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehicleRepository.FindAll();
        }

        public ResponseMessage DeleteVehicle(Guid vehicleId)
        {
            try
            {
                //Business logic to delete vehicle data
                _vehicleRepository.Delete(vehicleId);
                _vehicleRepository.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ResponseMessage.Deleted;
        }

        public bool VehicleInspection(VehicleDto vehicleDto)
        {
            //Business logic to check vehicle inspection 
            if (vehicleDto.isTow == true)
            {
                bool isValidVehicle = false;
                switch (vehicleDto.Type)
                {
                    case VehicleType.Truck:
                        isValidVehicle = (vehicleDto.Lift <= 5);
                        break;
                    case VehicleType.Car:
                        isValidVehicle = (vehicleDto.Tire < 85);
                        break;
                }
                return isValidVehicle;
            }
            return false;
        }
    }
}