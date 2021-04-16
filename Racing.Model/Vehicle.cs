using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Racing.Model
{
    public class Vehicle
    {
        public static List<Vehicle> GetDefaultVehicleData()
        {
            var vehicles = new List<Vehicle>()
            {
                new Vehicle(){
                Id=Guid.NewGuid(),
                Name="Demo Car",
                Type=VehicleType.Car,
                Tire=5,
                Lift=5,
                isTow=true,
                Image="car.jpg"
                }
            };
            return vehicles;
        }
        public Guid Id { get; set; }

        [Display(Name = "Vehicle Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Vehicle Type")]
        public VehicleType Type { get; set; }

        [Display(Name = "Tow")]
        public bool isTow { get; set; }

        [Display(Name = "Lift")]
        public int Lift { get; set; }

        [Display(Name = "Tire")]
        public int? Tire { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
       
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
