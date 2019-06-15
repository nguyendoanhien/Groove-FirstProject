using Microsoft.EntityFrameworkCore;

namespace GrooveNoteDAL.Uow.Interface
{
    public interface IUowBase<TContext> where TContext : DbContext
    {
        void SaveChanges();
        void SaveChangesAsync();
    }
}
