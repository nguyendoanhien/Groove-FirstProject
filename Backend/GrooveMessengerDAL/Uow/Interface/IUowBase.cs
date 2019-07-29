using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GrooveMessengerDAL.Uow.Interface
{
    public interface IUowBase<TContext> where TContext : DbContext
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}