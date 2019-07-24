using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Uow.Interface
{
    public interface IUowBase<TContext> where TContext : DbContext
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
