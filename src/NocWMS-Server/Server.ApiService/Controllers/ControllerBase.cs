using AutoMapper;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.ApiService.Services;

namespace Server.ApiService.Controllers
{
    public abstract class ControllerBase<TCategoryName, TRepository> : Controller
        where TCategoryName : Controller
        where TRepository : class
    {
        protected readonly IUnitOfWork _uf;
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        protected readonly ILogger _logger;

        public ControllerBase(IInjectService<TCategoryName, TRepository> service)
        {
            _uf = service.UnitOfWork;
            _repository = service.Repository;
            _logger = service.Logger;
            _mapper = service.Mapper;
        }

    }
}
