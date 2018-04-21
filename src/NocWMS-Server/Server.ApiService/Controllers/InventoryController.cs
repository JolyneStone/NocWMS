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
    public class InventoryController : ControllerBase<InventoryController, IInventoryRepository>
    {
        public InventoryController(IInjectService<InventoryController, IInventoryRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory(int id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<InventoryDetailViewModel>(await _repository.Include().FirstOrDefaultAsync(x=>x.Id == id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInventory([FromBody] InventoryAddFormViewModel inventoryAddForm)
        {
            var data = new MessageDataViewModel();
            if (inventoryAddForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该库存信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            else
            {
                try
                {
                    await _repository.UpdatInventoryAsync(inventoryAddForm);
                    await _uf.SaveChangesAsync();
                    data.IsSuccess = true;
                }
                catch(InvalidOperationException ex)
                {
                    data.IsSuccess = false;
                    data.Message = ex.Message;
                }
            }

            return Ok(data);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddInventory([FromBody] InventoryFormViewModel inventoryForm)
        //{
        //    var data = new MessageDataViewModel();
        //    if (inventoryForm == null)
        //    {
        //        data.IsSuccess = false;
        //        data.Message = "无法得到该库存信息";
        //        return Json(data);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        data.IsSuccess = false;
        //        data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
        //        return Json(data);
        //    }

        //    var (result, inventory) = await _repository.TryAddInventoryAsync(inventoryForm);
        //    if (result)
        //    {
        //        await _uf.SaveChangesAsync();
        //        data.Data = _mapper.Map<InventoryFormViewModel>(inventory);
        //        data.IsSuccess = true;
        //    }
        //    else
        //    {
        //        data.IsSuccess = false;
        //        data.Message = "疑似库存信息重复, 请重新输入";
        //    }

        //    return Ok(data);
        //}

        [HttpGet]
        public async Task<IActionResult> GetInventoryList(int warehouseId, int categoryId, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetInventoryListAsync(warehouseId, categoryId, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<InventoryDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<InventoryDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(int warehouseId, int categoryId, string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetInventoryListAsync(warehouseId, categoryId,query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<InventoryDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<InventoryDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteInventoryAsync(id)
            };

            await _uf.SaveChangesAsync();
            if (!data.IsSuccess)
            {
                data.Message = "内部程序出现错误, 无法执行删除操作";
            }

            return Ok(data);
        }
    }
}
