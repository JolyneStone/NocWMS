using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KiraNet.Camellia.Infrastructure.DomainModel.Data
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWorkBase(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int SaveChanges() => _dbContext.SaveChanges();

        public async Task<int> SaveChangesAsync()
        {
            int v = await _dbContext.SaveChangesAsync();
            return v;
        }

        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();

        public int SaveChanges(bool acceptAllChangesOnSuccess) => _dbContext.SaveChanges(acceptAllChangesOnSuccess);

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) => await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => await _dbContext.SaveChangesAsync(cancellationToken);

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool Save(bool acceptAllChangesOnSuccess)
        {
            return _dbContext.SaveChanges(acceptAllChangesOnSuccess) > 0;
        }

        public async Task<bool> SaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) > 0;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }


        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _dbContext.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
