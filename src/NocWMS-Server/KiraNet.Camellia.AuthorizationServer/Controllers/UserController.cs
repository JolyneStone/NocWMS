using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KiraNet.Camellia.AuthorizationServer.Models;
using KiraNet.Camellia.AuthorizationServer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KiraNet.Camellia.AuthorizationServer.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManger;
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _logger = logger;
            _userManger = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManger.Users.AsNoTracking().ToListAsync();
            var userVms = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(userVms);
        }
    }
}
