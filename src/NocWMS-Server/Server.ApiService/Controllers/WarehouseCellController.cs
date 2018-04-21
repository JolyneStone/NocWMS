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
    public class WarehouseCellController : ControllerBase<WarehouseCellController, IWarehouseCellRepository>
    {
        public WarehouseCellController(IInjectService<WarehouseCellController, IWarehouseCellRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseCellSimples(int id)
        {
            var data = new MessageDataViewModel()
            {
                Data = (await _repository.GetAllAsync(x => x.WarehouseId == id)).AsNoTracking()
                    .Select(x => _mapper.Map<WarehouseCellSimpleViewModel>(x))
                    .ToList()
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseCellSimple(int id)
        {
            var data = new MessageDataViewModel();
            var warehouseCell = await _repository.GetByIdAsync(id);
            data.Data = _mapper.Map<WarehouseCellSimpleViewModel>(warehouseCell);
            data.IsSuccess = true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseCellSimpleByName(int id, string cellName)
        {
            var data = new MessageDataViewModel();
            var warehouseCell = await _repository.GetFirstOrDefaultAsync(x=>x.WarehouseId == id && x.CellName == cellName);
            if (warehouseCell == null)
            {
                data.IsSuccess = false;
                data.Message = "找不到该库位";
                return Json(data);
            }

            data.Data = _mapper.Map<WarehouseCellSimpleViewModel>(warehouseCell);
            data.IsSuccess = true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseCell(int id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<WarehouseCellDisplayViewModel>(await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWarehouseCell([FromBody] WarehouseCellFormViewModel warehouseCellForm)
        {
            var data = new MessageDataViewModel();
            if (warehouseCellForm == null)
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
            if (warehouseCellForm.RemainderVolume > warehouseCellForm.Volume)
            {
                data.IsSuccess = false;
                data.Message = "剩余容量不能大于总容量";
            }

            var warehouseCell = await _repository.GetByIdAsync(warehouseCellForm.Id);
            if (warehouseCell == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该库位信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatWarehouseCell(warehouseCell, warehouseCellForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<WarehouseCellFormViewModel>(warehouseCell);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouseCell([FromBody] WarehouseCellFormViewModel warehouseCellForm)
        {
            var data = new MessageDataViewModel();
            if (warehouseCellForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该库位信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, warehouseCell) = await _repository.TryAddWarehouseCellAsync(warehouseCellForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<WarehouseCellFormViewModel>(warehouseCell);
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
        public async Task<IActionResult> GetWarehouseCellList(int id, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetWarehouseCellListAsync(id, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<WarehouseCellDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<WarehouseCellDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(int id, string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetWarehouseCellListAsync(id, query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<WarehouseCellDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<WarehouseCellDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouseCell(int id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteWarehouseCellAsync(id)
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
