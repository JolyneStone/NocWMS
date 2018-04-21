using AutoMapper;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.ApiService.Services
{
    public class InjectService<TCategoryName, TRepository>:IInjectService<TCategoryName, TRepository>
        where TCategoryName : Controller
        where TRepository : class
    {
        public IUnitOfWork UnitOfWork { get; }
        public TRepository Repository { get; }
        public IMapper Mapper { get; }
        public ILogger<TCategoryName> Logger { get; }

        public InjectService(IUnitOfWork unitOfWork, TRepository repository, IMapper mapper, ILogger<TCategoryName> logger)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
            Mapper = mapper;
            Logger = logger;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
