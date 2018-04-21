using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class UserInfo : IEntity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }

        public Staff Staff { get; set; }
    }
}
