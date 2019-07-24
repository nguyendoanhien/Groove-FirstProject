using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GrooveMessengerDAL.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GrooveMessengerDAL.Repositories.Interface
{
    public interface IGenericRepository<TEntity, TKey, TContext>
       where TEntity : BaseEntity<TKey> where TContext : DbContext
    {
        IQueryable<TEntity> GetAll();

        Task<IQueryable<TEntity>> GetAllAsync();

        TEntity GetSingle(TKey entityId);

        Task<TEntity> GetSingleAsync(TKey entityId);

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        Task<IQueryable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindAll();

        Task<IQueryable<TEntity>> FindAllAsync();

        TEntity FindSingle(TKey entityId);

        Task<TEntity> FindSingleAsync(TKey entityId);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        EntityEntry<TEntity> Add(TEntity entity);

        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        void Delete(TKey entityId);

        Task DeleteAsync(TKey entityId);

        void Edit(TEntity entity);

        Task EditAsync(TEntity entity);

        bool CheckExistingById(TKey id);

        IQueryable<TEntity> ExecuteReturedStoredProcedure(string storedProcedureName, params SqlParameter[] parameters);

        int ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters);

        List<TResult> ExecuteReturedStoredProcedure<TResult>(string storedProcedureName, params SqlParameter[] parameters);

    }
}
