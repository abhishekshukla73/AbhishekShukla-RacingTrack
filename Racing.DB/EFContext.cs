using Racing.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Racing.DB
{
    public class EFContext : DbContext
    {
        public EFContext() : base("dbConnectionString")
        { }
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
