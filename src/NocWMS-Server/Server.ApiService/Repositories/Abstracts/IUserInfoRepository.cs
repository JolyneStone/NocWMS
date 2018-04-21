using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        Task<UserInfo> GetByNameAsync(string userName);

        Task<UserInfo> CreateUserInfoAsync(string userName, string role = null);
        Task<bool> IsCompletedAsync(string id, string email);
    }
}
