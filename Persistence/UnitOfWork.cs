using System.Threading.Tasks;
using WebApplicationWithCore.Core;
namespace WebApplicationWithCore.Persistence
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly WebApplicationWithCoreDbContext context;
        public UnitOfWork(WebApplicationWithCoreDbContext context)
        {
            this.context = context;

        }
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}