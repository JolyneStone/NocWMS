using System;
using AutoMapper;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.ApiService.Services
{
    public interface IInjectService<TCategoryName, TRepository> : IDisposable
        where TCategoryName : Controller
        where TRepository : class
    {
        IUnitOfWork UnitOfWork { get; }
        TRepository Repository { get; }
        IMapper Mapper { get; }
        ILogger<TCategoryName> Logger { get; }
    }
}
