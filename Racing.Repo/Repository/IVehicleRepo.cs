using Racing.Model;

namespace Racing.Repo
{
    public interface IVehicleRepo : IBaseRepo<Vehicle>
    {
        int allowedVehicleOnTrack { get; set; }
    }
}
