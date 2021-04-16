using Racing.App.Services;
using Racing.Model;
using System;
using System.Web.Mvc;

namespace Racing.App.Controllers
{
    public class TrackController : Controller
    {
        private readonly IRacingService iVehicleRaceService;
        public TrackController(IRacingService vehicleRaceService)
        {
            iVehicleRaceService = vehicleRaceService;
        }
       
        [HttpGet]
        public ActionResult Index()
        {
            return View(iVehicleRaceService.GetVehicles());
        }

        [HttpGet]
        public PartialViewResult GetVehicleList()
        {
            return PartialView("Vehicle", iVehicleRaceService.GetVehicles());
        }

        [HttpPost]
        public ResponseMessage RemoveVehicleFromTrack(Guid vehicleId)
        {
            return iVehicleRaceService.DeleteVehicle(vehicleId);
        }
    }
}