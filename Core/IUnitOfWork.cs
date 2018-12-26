using System.Threading.Tasks;
namespace WebApplicationWithCore.Core
{

    public interface IUnitOfWork
    {
        Task Complete();
    }
}