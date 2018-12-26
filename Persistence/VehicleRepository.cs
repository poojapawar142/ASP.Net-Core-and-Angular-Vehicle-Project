using WebApplicationWithCore.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithCore.Core;
using WebApplicationWithCore.Persistence;
namespace WebApplicationWithCore.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly WebApplicationWithCoreDbContext context;
        public VehicleRepository(WebApplicationWithCoreDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id , bool includeRelated=true)
        {
            if(!includeRelated)
               return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
           .Include(v=>v.Features)
           .ThenInclude(vf=>vf.Feature)
           .Include(v=>v.model)
           .ThenInclude(m=>m.Make)
           .SingleOrDefaultAsync(v=>v.Id == id);  
        }
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

         public void Remove(Vehicle vehicle)
         {
             context.Remove(vehicle);
         }
    }
}