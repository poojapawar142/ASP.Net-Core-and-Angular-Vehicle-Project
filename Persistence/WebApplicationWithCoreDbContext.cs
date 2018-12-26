
using Microsoft.EntityFrameworkCore;
using WebApplicationWithCore.Models;
namespace WebApplicationWithCore.Persistence
{
    public class WebApplicationWithCoreDbContext : DbContext
    {
         public DbSet<Make> Makes {get;set;}
         public DbSet<Vehicle> Vehicles {get;set;}
         public DbSet<Feature> Features { get; set; }
         public DbSet<Model> Models { get; set; }
        public WebApplicationWithCoreDbContext(DbContextOptions<WebApplicationWithCoreDbContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.VehicleId , vf.FeatureId});
        }
         
    }
}