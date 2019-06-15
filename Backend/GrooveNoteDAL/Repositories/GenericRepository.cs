using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using GrooveNoteDAL.Entities;
using GrooveNoteDAL.Repositories.Interface;
using GrooveNoteDAL.Services.Interface;

namespace GrooveNoteDAL.Repositories
{
    public class GenericRepository<TEntity, TKey, TContext> : IGenericRepository<TEntity, TKey, TContext>
        where TEntity : BaseEntity<TKey> where TContext : DbContext
    {
        protected readonly DbContext DbContext;
        protected readonly IUserResolverService UserResolverService;

        private DbSet<TEntity> _entity;

        protected DbSet<TEntity> Entity => _entity ?? (_entity = DbContext.Set<TEntity>());

        public GenericRepository(TContext dbContext, IUserResolverService userResolverService)
        {
            DbContext = dbContext;
            UserResolverService = userResolverService;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entity.AsNoTracking().Where(x => (x.Deleted == null || !x.Deleted.Value) &&
                                                    x.CreatedBy == UserResolverService.CurrentUserName());
        }

        public TEntity GetSingle(TKey entityId)
        {
            return Entity.AsNoTracking().FirstOrDefault(x =>
                x.Id.Equals(entityId) && (x.Deleted == null || !x.Deleted.Value) &&
                x.CreatedBy == UserResolverService.CurrentUserName());
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Entity.AsNoTracking().Where(x => (x.Deleted == null || !x.Deleted.Value) &&
                                                    x.CreatedBy == UserResolverService.CurrentUserName())
                .Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Entity.Add(entity);
        }

        public Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            var result = Entity.AddAsync(entity);
            return result;
        }

        public void Delete(TKey entityId)
        {
            var storedEntity = Entity.AsNoTracking().FirstOrDefault(x =>
                x.Id.Equals(entityId) && (x.Deleted == null || !x.Deleted.Value) &&
                x.CreatedBy == UserResolverService.CurrentUserName());
            if (storedEntity == null) return;
            storedEntity.Deleted = true;
            Entity.Attach(storedEntity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int entityId)
        {
            var storedEntity = await Entity.FirstOrDefaultAsync(x =>
                x.Id.Equals(entityId) && (x.Deleted == null || !x.Deleted.Value) &&
                x.CreatedBy == UserResolverService.CurrentUserName());
            if (storedEntity == null) return;
            storedEntity.Deleted = true;
            Entity.Attach(storedEntity).State = EntityState.Modified;
        }

        public void Edit(TEntity entity)
        {
            var storedEntity = Entity.Attach(entity);
            storedEntity.State = EntityState.Modified;
        }

        public bool CheckExistingById(TKey id)
        {
            var result = Entity.AsNoTracking().Any(x => x.Id.Equals(id));
            return result;
        }
    }
}
