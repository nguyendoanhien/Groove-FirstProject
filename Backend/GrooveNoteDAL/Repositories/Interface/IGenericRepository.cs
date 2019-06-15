using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GrooveNoteDAL.Entities;

namespace GrooveNoteDAL.Repositories.Interface
{
    public interface IGenericRepository<TEntity, TKey, TContext>
        where TEntity : BaseEntity<TKey> where TContext : DbContext
    {
        IQueryable<TEntity> GetAll();
        TEntity GetSingle(TKey entityId);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TKey entityId);
        void Edit(TEntity entity);
        bool CheckExistingById(TKey id);
    }
}
