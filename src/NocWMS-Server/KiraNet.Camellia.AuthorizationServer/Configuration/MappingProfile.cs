using AutoMapper;
using KiraNet.Camellia.AuthorizationServer.Models;
using KiraNet.Camellia.AuthorizationServer.Models.ViewModels;

namespace KiraNet.Camellia.AuthorizationServer.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
