using Racing.App.Helper;
using Racing.App.Models;
using Racing.App.Services;
using Racing.Model;
using System;
using System.IO;
using System.Web.Mvc;

namespace Racing.App.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IRacingService iVehicleRaceService;

        public VehicleController(IRacingService vehicleRaceService)
        {
            iVehicleRaceService = vehicleRaceService;
        }

        /// <summary>
        /// GET Method: Create Vehicle
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST Method: Add Vehicle
        /// </summary>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleDto vehicleModel)
        {
            // Business logic to check  vehicle inspection
            if (!iVehicleRaceService.VehicleInspection(vehicleModel))
            {
                ModelState.AddModelError(nameof(vehicleModel.ResponseMessage), Utility.GetResourceValue("InspectionFail"));
            }
            if (ModelState.IsValid)
            {
                var image = vehicleModel.ImageFile;
                if (image?.ContentLength > 0)
                {
                    //Business logic for save vehicle image
                    string fileExtension = Path.GetExtension(image.FileName);
                    string fileName = Guid.NewGuid() + fileExtension;
                    string folderPath = Path.Combine(Server.MapPath(Utility.GetResourceValue("ImagePath")), fileName);
                    image.SaveAs(folderPath);
                    vehicleModel.Image = fileName;
                }

                //Business logic for save vehicle data 
                ResponseMessage response = iVehicleRaceService.AddVehicles(vehicleModel);
                if (response == ResponseMessage.Inserted)
                    ViewBag.Success = Utility.GetResourceValue("Success");
                else
                    ModelState.AddModelError(nameof(vehicleModel.ResponseMessage), Utility.GetResourceValue("TrackOverload"));
            }
            return View();
        }
    }
}
