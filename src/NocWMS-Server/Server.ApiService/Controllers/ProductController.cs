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
    public class ProductController : ControllerBase<ProductController, IProductRepository>
    {
        public ProductController(IInjectService<ProductController, IProductRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var data = new MessageDataViewModel();
            if (String.IsNullOrWhiteSpace(productName))
            {
                data.IsSuccess = false;
                data.Message = "产品名称为空";
                return Json(data);
            }

            var product = await _repository.GetFirstOrDefaultAsync(x => x.ProductName == productName);
            if(product == null)
            {
                data.IsSuccess = false;
                data.Message = "找不到该产品";
                return Json(data);
            }

            data.Data = _mapper.Map<ProductDisplayViewModel>(product);
            data.IsSuccess = true;
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<ProductFormViewModel>(await _repository.GetByIdAsync(id))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductFormViewModel productForm)
        {
            var data = new MessageDataViewModel();
            if (productForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该产品信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var product = await _repository.GetByIdAsync(productForm.Id);
            if (product == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该产品信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatProduct(product, productForm);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<ProductFormViewModel>(product);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductFormViewModel productForm)
        {
            var data = new MessageDataViewModel();
            if (productForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该产品信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var (result, product) = await _repository.TryAddProductAsync(productForm);
            if (result)
            {
                await _uf.SaveChangesAsync();
                data.Data = _mapper.Map<ProductFormViewModel>(product);
                data.IsSuccess = true;
            }
            else
            {
                data.IsSuccess = false;
                data.Message = "疑似产品信息重复, 请重新输入";
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList(int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetProductListAsync(page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<ProductDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<ProductDisplayViewModel>>(list)
                },
                IsSuccess = true
            };

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string query, int page, int pageSize = 5)
        {
            var (list, total) = await _repository.GetProductListAsync(query, page, pageSize);
            var data = new MessageDataViewModel
            {
                Data = new PageDataViewModel<ProductDisplayViewModel>()
                {
                    Total = total,
                    Data = _mapper.Map<IList<ProductDisplayViewModel>>(list)
                },
                IsSuccess = true
            };


            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteProductAsync(id)
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
