using Microsoft.EntityFrameworkCore;
using GrooveMessengerDAL.Uow.Interface;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Uow
{
    public class UowBase<TContext> : IUowBase<TContext> where TContext:DbContext
    {
        protected readonly DbContext DbContext;
        public UowBase(TContext dbContext)
        {
            DbContext = dbContext;
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
