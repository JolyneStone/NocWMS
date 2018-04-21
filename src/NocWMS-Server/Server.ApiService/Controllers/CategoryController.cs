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
    public class CategoryController : ControllerBase<CategoryController, ICategoryRepository>
    {
        public CategoryController(IInjectService<CategoryController, ICategoryRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<CategoryFormViewModel>(await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryFormViewModel categoryForm)
        {
            var data = new MessageDataViewModel();
            if (categoryForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该类别信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var category = await _repository.GetByIdAsync(categoryForm.Id);
            if (category == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该类别信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatCategory(category, categoryForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<CategoryFormViewModel>(category);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryFormViewModel categoryForm)
        {
            var data = new MessageDataViewModel();
            if (categoryForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该类别信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, category) = await _repository.TryAddCategoryAsync(categoryForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<CategoryFormViewModel>(category);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似类别信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList(int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetCategoryListAsync(page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<CategoryDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<CategoryDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetCategoryListAsync(query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<CategoryDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<CategoryDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteCategoryAsync(id)
            };

            await _uf.SaveChangesAsync();
            if (!data.IsSuccess)
            {
                data.Message = "内部程序出现错误, 无法执行删除操作";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorySimples()
        {
            var data = new MessageDataViewModel()
            {
                Data = _mapper.Map<IList<CategorySimpleViewModel>>((await _repository.GetAllAsync()).ToList())
            };

            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }
    }
}
