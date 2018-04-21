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
    public class WarehouseController : ControllerBase<WarehouseController, IWarehouseRepository>
    {
        public WarehouseController(IInjectService<WarehouseController, IWarehouseRepository> service, ICategoryRepository categoryRepository) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseSimple(int id)
        {
            var data = new MessageDataViewModel();
            var warehouse = await _repository.GetByIdAsync(id);
            if(warehouse == null)
            {
                data.IsSuccess = false;
                data.Message = "无法找到该仓库";
                return Json(data);
            }

            data.Data = _mapper.Map<WarehouseSimpleViewModel>(warehouse);
            data.IsSuccess = data.Data == null ? false : true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseSimples()
        {
            var data = new MessageDataViewModel()
            {
                Data = (await _repository.GetAllAsync()).AsNoTracking()
                    .Select(x => _mapper.Map<WarehouseSimpleViewModel>(x))
                    .ToList()
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseCellSimples(int id)
        {
            var data = new MessageDataViewModel();
            var warehouse = await _repository.GetByIdAsync(id);
            if (warehouse == null)
            {
                data.IsSuccess = false;
                data.Message = "无法获取该仓库数据";
                return Json(data);
            }

            data.Data = _repository.LoadWarehouseCells(warehouse)
                .Cells
                .Select(x => _mapper.Map<WarehouseCellSimpleViewModel>(x))
                .ToList();

            data.IsSuccess = true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouse(int id)
        {
            var warehouse = _repository.LoadManager((await _repository.GetByIdAsync(id)));
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<WarehouseFormViewModel>(warehouse)
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWarehouse([FromBody] WarehouseFormViewModel warehouseForm)
        {
            var data = new MessageDataViewModel();
            if (warehouseForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该仓库信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var warehouse = await _repository.GetByIdAsync(warehouseForm.Id);
            if (warehouse == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该仓库信息";
                return Json(data);
            }
            else
            {
                await _repository.UpdatWarehouseAsync(warehouse, warehouseForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<WarehouseFormViewModel>(warehouse);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody] WarehouseFormViewModel warehouseForm)
        {
            var data = new MessageDataViewModel();
            if (warehouseForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该仓库信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, warehouse) = await _repository.TryAddWarehouseAsync(warehouseForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<WarehouseFormViewModel>(warehouse);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似仓库信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseList(int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetWarehouseListAsync(page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<WarehouseDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<WarehouseDisplayViewModel>>(list
                        .Select(x => _repository.LoadManager(x))
                        .ToList())
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetWarehouseListAsync(query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<WarehouseDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<WarehouseDisplayViewModel>>(list
                        .Select(x => _repository.LoadManager(x))
                        .ToList())
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteWarehouseAsync(id)
            };

            await _uf.SaveChangesAsync();
            if (!data.IsSuccess)
            {
                data.Message = "内部程序出现错误, 无法执行删除操作";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseMap()
        {
            var data = new MessageDataViewModel()
            {
                Data = _mapper.Map<IList<WarehouseProvinceViewModel>>((await _repository.GetAllAsync()).ToList())
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }
    }
}
