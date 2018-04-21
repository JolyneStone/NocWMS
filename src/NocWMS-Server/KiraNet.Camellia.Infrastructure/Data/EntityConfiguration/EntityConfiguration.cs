using System;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiraNet.Camellia.Infrastructure.Data.EntityConfiguration
{
    public abstract class EntityConfiguration<TEntity, TPrimaryKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public void Configure(EntityTypeBuilder<TEntity> entity)
        {
            // use: ModelBuilder.ApplyConfiguration(IEntityTypeConfiguration entity);
            ConfigureDerived(entity);
        }

        protected abstract void ConfigureDerived(EntityTypeBuilder<TEntity> entity);
    }

    public abstract class EntityConfiguration<TEntity> : EntityConfiguration<TEntity, string>
    where TEntity : class, IEntity
    {
    }

    public abstract class EntityCommonConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> entity)
        {
            // use: ModelBuilder.ApplyConfiguration(IEntityTypeConfiguration entity);
            ConfigureDerived(entity);
        }

        protected abstract void ConfigureDerived(EntityTypeBuilder<TEntity> entity);
    }
}
