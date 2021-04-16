using Racing.DB;
using Racing.Model;

namespace Racing.Repo
{
    public class VehicleRepo : BaseRepo<Vehicle>, IVehicleRepo
    {
        public int allowedVehicleOnTrack { get; set; }
        public EFContext Database { get; }
        public VehicleRepo(EFContext _database) : base(_database)
        {
            Database = _database;
        }
    }
}
