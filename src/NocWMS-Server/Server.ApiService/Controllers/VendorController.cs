using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;
using Server.ApiService.Services;

namespace Server.ApiService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class VendorController : ControllerBase<VendorController, IVendorRepository>
    {
        public VendorController(IInjectService<VendorController, IVendorRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor(string id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<VendorFormViewModel>(await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVendor([FromBody] VendorFormViewModel vendorForm)
        {
            var data = new MessageDataViewModel();
            if (vendorForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该供应商信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var vendor = await _repository.GetByIdAsync(vendorForm.Id);
            if (vendor == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该供应商信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatVendor(vendor, vendorForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<VendorFormViewModel>(vendor);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor([FromBody] VendorFormViewModel vendorForm)
        {
            var data = new MessageDataViewModel();
            if (vendorForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该供应商信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, vendor) = await _repository.TryAddVendorAsync(vendorForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<VendorFormViewModel>(vendor);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似供应商信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendorList(int page, int pageSize = 5)
        {
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<VendorDisplayViewModel>()
                {
                    Total = await _repository.CountAsync(),
                    Data = _mapper.Map<IList<VendorDisplayViewModel>>(await _repository.GetVendorListAsync(page, pageSize))
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<VendorDisplayViewModel>()
                {
                    Total = await _repository.CountAsync(),
                    Data = _mapper.Map<IList<VendorDisplayViewModel>>(await _repository.GetVendorListAsync(query, page, pageSize))
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVendor(string id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteVendorAsync(id)
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
