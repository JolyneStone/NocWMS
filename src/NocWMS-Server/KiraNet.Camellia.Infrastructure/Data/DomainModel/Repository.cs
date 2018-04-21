using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace KiraNet.Camellia.Infrastructure.DomainModel.Data
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
          where TEntity : class, IEntity<TPrimaryKey>, new()
          where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected readonly DbContext _dbContext;
        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public Repository(DbContext dbContext) => _dbContext = dbContext;
        public EntityEntry<TEntity> Entry(TEntity entity) => _dbContext.Entry<TEntity>(entity);
        public IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
            => Entities.Include(navigationPropertyPath);
        public TEntity GetById(TPrimaryKey id)
            => Entities.SingleOrDefault(x => id.Equals(x.Id));
        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
            => await Entities.SingleOrDefaultAsync(x => id.Equals((x.Id)));
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
            => predicate == null ? Entities.FirstOrDefault() : Entities.FirstOrDefault(predicate);
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
            => await (predicate == null ? Entities.FirstOrDefaultAsync() : Entities.FirstOrDefaultAsync(predicate));
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
            => predicate == null ? Entities.AsQueryable() : Entities.Where(predicate);
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
            => predicate == null ? Task.Factory.StartNew(() => Entities.AsQueryable()) : Task.Factory.StartNew(() => Entities.Where(predicate));
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? await Entities.SingleOrDefaultAsync(predicate) : throw new ArgumentNullException(nameof(predicate));
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? Entities.SingleOrDefault(predicate) : throw new ArgumentNullException(nameof(predicate));

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Add(entity);
        }
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Entities.AddAsync(entity);
        }
        public void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                throw new ArgumentNullException(nameof(entities));
            Entities.AddRange(entities);
        }
        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                throw new ArgumentNullException(nameof(entities));
            await Entities.AddRangeAsync(entities);
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Remove(entity);
        }
        public void Delete(TPrimaryKey id)
        {
            var entity = this.GetFromChangeTrackerOrNull(id);

            if (entity == null)
                entity = GetFirstOrDefault(x => Equals(x.Id, id));

            if (entity == null)
                return;

            Entities.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                throw new ArgumentNullException(nameof(entities));
            Entities.RemoveRange(entities);
        }
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                throw new ArgumentNullException(nameof(entities));
            Entities.UpdateRange(entities);
        }

        public void Attach(TEntity entity)
        {
            Entities.Attach(entity);
        }

        public void AttachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
            }
        }

        public async Task<int> CountAsync()
            => await Entities.CountAsync();
        public int Count()
            => Entities.Count();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
            => await Entities.CountAsync(predicate);
        public int Count(Expression<Func<TEntity, bool>> predicate)
            => Entities.Count(predicate);
        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? Entities.Any(predicate) : throw new ArgumentNullException(nameof(predicate));
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? await Entities.AnyAsync(predicate) : throw new ArgumentNullException(nameof(predicate));
        public bool IsAll(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? Entities.All(predicate) : throw new ArgumentNullException(nameof(predicate));
        public async Task<bool> IsAllAsync(Expression<Func<TEntity, bool>> predicate)
            => predicate != null ? await Entities.AllAsync(predicate) : throw new ArgumentNullException(nameof(predicate));

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entity = _dbContext.ChangeTracker.Entries<TEntity>()
                .FirstOrDefault(x => 
                    id.Equals(x.Entity.Id));

            return entity.Entity;
        }
    }

    public class Repository<TEntity> : Repository<TEntity, string>, IRepository<TEntity>
        where TEntity : class, IEntity<string>, new()
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
