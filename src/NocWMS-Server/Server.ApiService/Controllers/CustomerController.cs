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
    public class CustomerController : ControllerBase<CustomerController, ICustomerRepository>
    {
        public CustomerController(IInjectService<CustomerController, ICustomerRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<CustomerFormViewModel>(await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerFormViewModel customerForm)
        {
            var data = new MessageDataViewModel();
            if (customerForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该客户信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var customer = await _repository.GetByIdAsync(customerForm.Id);
            if (customer == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该客户信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatCustomer(customer, customerForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<CustomerFormViewModel>(customer);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerFormViewModel customerForm)
        {
            var data = new MessageDataViewModel();
            if (customerForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该客户信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, customer) = await _repository.TryAddCustomerAsync(customerForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<CustomerFormViewModel>(customer);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似客户信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerList(int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetCustomerListAsync(page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<CustomerDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<CustomerDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetCustomerListAsync(query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<CustomerDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<CustomerDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteCustomerAsync(id)
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
