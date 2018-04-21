using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;
using Server.ApiService.Services;

namespace Server.ApiService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class InventoryCellController : ControllerBase<InventoryCellController, IInventoryCellRepository>
    {
        public InventoryCellController(IInjectService<InventoryCellController, IInventoryCellRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryCells(int id)
        {
            var data = new MessageDataViewModel()
            {
                Data = _mapper.Map<IList<InventoryCellDisplayViewModel>>(await _repository.Include().Where(x => x.InventoryId == id).ToListAsync())
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }
    }
}
