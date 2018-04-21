using System;

namespace KiraNet.Camellia.Infrastructure.DomainModel.Data
{
    public interface IEntity<TPrimaryKey>
      where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}
