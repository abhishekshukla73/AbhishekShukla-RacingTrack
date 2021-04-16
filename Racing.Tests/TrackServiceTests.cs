using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Racing.App.Models;
using Racing.App.Services;
using Racing.Model;
using Racing.Repo;
using System;
using System.Collections.Generic;

namespace Racing.Tests
{
    [TestClass]
    public class TrackServiceTests
    {
        #region(Global Variable Declaration)
      
        private IList<VehicleDto> mockData;
        private IRacingService mockRacingService;
        private Mock<IVehicleRepo> mockVehicleRepo;

        #endregion

        #region(Setup)

        private void Setup()
        {
            mockData = new List<VehicleDto>()
            {
                new VehicleDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Swift",
                    isTow = true,
                    Lift = 5,
                    Type=VehicleType.Car,
                    Image= "car.jpg",
                    CreatedDate=DateTime.Now
                },
            };

            //Mock repository
            mockVehicleRepo = new Mock<IVehicleRepo>();
            mockVehicleRepo.Setup(x => x.allowedVehicleOnTrack).Returns(() => 5);
            mockVehicleRepo.Setup(d => d.Add(It.IsAny<Vehicle>()));
            mockRacingService = new RacingService(mockVehicleRepo.Object);
        }
        #endregion

        [TestMethod]
        public void Should_Get_All_Vehicle_List()
        {
            //Arrange
            Setup();

            //Act
             var allVehicleData = mockRacingService.GetVehicles();

            //Assert
            Assert.IsNotNull(allVehicleData);
        }

        [TestMethod]
        public void Should_Inspection_Fail_WIthout_Tow()
        {
            //Arrange
            Setup();
            var mockVehicleDto = mockData[0];
            mockVehicleDto.isTow = false;

            //Act
            var actualResult = mockRacingService.AddVehicles(mockVehicleDto);

            //Assert
            Assert.AreEqual(ResponseMessage.InspectionFail, actualResult);
        }

        [TestMethod]
        public void Should_Inspection_Fail_For_Lift()
        {
            // Arrange
            Setup();
            var mockVehicleDto = mockData[0];
            mockVehicleDto.Lift = 6;

            //Act
            var actualResult = mockRacingService.AddVehicles(mockVehicleDto);

            //Assert
            Assert.AreEqual(ResponseMessage.InspectionFail, actualResult);
        }

        [TestMethod]
        public void Should_Inspection_Fail_For_Tire()
        {
            // Arrange
            Setup();
            var mockVehicleDto = mockData[0];
            mockVehicleDto.Tire = 86;

            //Act
            var actualResult = mockRacingService.AddVehicles(mockVehicleDto);

            //Assert
            Assert.AreEqual(ResponseMessage.InspectionFail, actualResult);
        }
    }
}
