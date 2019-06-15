using Microsoft.EntityFrameworkCore;
using GrooveNoteDAL.Uow.Interface;

namespace GrooveNoteDAL.Uow
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

        public void SaveChangesAsync()
        {
            DbContext.SaveChangesAsync();
        }
    }
}
