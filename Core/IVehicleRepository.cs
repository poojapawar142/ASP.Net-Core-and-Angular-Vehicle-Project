using System.Threading.Tasks;
using WebApplicationWithCore.Models;
namespace WebApplicationWithCore.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id , bool includeRelated = true);
        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}