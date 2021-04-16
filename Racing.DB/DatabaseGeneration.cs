using Racing.Model;

namespace Racing.DB
{
    public class DatabaseGeneration : System.Data.Entity.DropCreateDatabaseAlways<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            var defaultVehicleList = Vehicle.GetDefaultVehicleData();
            defaultVehicleList.ForEach(vehicle => context.Vehicles.Add(vehicle));
            base.Seed(context);
        }
    }
}
