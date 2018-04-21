using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Common
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(NocDbContext context) : base(context)
        {
        }
    }
}
