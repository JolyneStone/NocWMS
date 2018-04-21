using System;
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
    public class StaffController : ControllerBase<StaffController, IStaffRepository>
    {
        public StaffController(IInjectService<StaffController, IStaffRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffSimple()
        {
            var data = new MessageDataViewModel
            {
                Data = await _repository.GetByEmailAsync(User.Claims.First(x => x.Type == "email").Value)
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetStaff(string id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<StaffFormViewModel>(String.IsNullOrWhiteSpace(id) ? 
                    await _repository.GetByEmailAsync(User.Claims.First(x => x.Type == "email").Value):
                    await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaff([FromBody] StaffFormViewModel staffForm)
        {
            var data = new MessageDataViewModel();
            if (staffForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该职工信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var email = User.Claims.First(x => x.Type == "email").Value;
            var staff = await _repository.GetByEmailAsync(email);
            if (staff == null)
            {
                staff = await _repository.CreateStaffAsync(User.Claims.First(x => x.Type == "name").Value, email, staffForm);
            }
            else
            {
                _repository.UpdatStaff(staff, staffForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<StaffFormViewModel>(staff);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] StaffAddFormViewModel staffAddForm)
        {
            var data = new MessageDataViewModel();
            if (staffAddForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该职工信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, staff) = await _repository.TryAddStaffAsync(staffAddForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<StaffAddFormViewModel>(staff);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似职工信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffList(int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetStaffListAsync(page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<StaffDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<StaffDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetStaffListAsync(query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<StaffDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<StaffDisplayViewModel>>(list)
                },
                IsSuccess = true
            };
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteStaffAsync(id, User.Claims.First(x => x.Type == "email").Value)
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
