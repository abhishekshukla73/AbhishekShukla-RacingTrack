using Racing.App.Models;
using Racing.Model;
using System;
using System.Collections.Generic;

namespace Racing.App.Services
{
    public interface IRacingService
    {
        IEnumerable<Vehicle> GetVehicles();
        ResponseMessage AddVehicles(VehicleDto vehicle);
        ResponseMessage DeleteVehicle(Guid vehicleId);
        bool VehicleInspection(VehicleDto vehicleDto);
    }
}