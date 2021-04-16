using Racing.Model;
using System.Web;

namespace Racing.App.Models
{
    public class VehicleDto : Vehicle
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ResponseMessage { get; set; }
    }
}