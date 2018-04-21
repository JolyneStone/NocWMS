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
    public class InboundReceiptController : ControllerBase<InboundReceiptController, IInboundReceiptRepository>
    {
        private IInboundReceiptItemRepository repository;
        public InboundReceiptController(IInjectService<InboundReceiptController, IInboundReceiptRepository> service, IInboundReceiptItemRepository repository) : base(service)
        {
            this.repository = repository;   
        }

        [HttpGet]
        public async Task<IActionResult> GetInboundReceipt(string id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<InboundReceiptDetailViewModel>(_repository.Load(await _repository.GetByIdAsync(id)))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateInboundReceipt([FromBody] InboundReceiptUpdateViewModel inboundReceiptUpdate)
        {
            var data = new MessageDataViewModel();
            if (inboundReceiptUpdate == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该入库单信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }

            var inboundReceipt = await _repository.GetByIdAsync(inboundReceiptUpdate.Id);
            if (inboundReceipt == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该入库单信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatInboundReceipt(inboundReceipt, inboundReceiptUpdate.IsDone);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddInboundReceipt([FromBody] InboundReceiptFormViewModel inboundReceiptForm)
        {
            var data = new MessageDataViewModel();
            if (inboundReceiptForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该入库单信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }
            if(inboundReceiptForm.InboundReceiptItems == null || !inboundReceiptForm.InboundReceiptItems.Any())
            {
                data.IsSuccess = false;
                data.Message = "入库项目不可为空";
                return Json(data);
            }
            try
            {
                var (result, inboundReceipt) = await _repository.TryAddInboundReceiptAsync(User.Claims.First(x => x.Type == "email").Value, inboundReceiptForm);
                if (result)
                {
                    await _uf.SaveChangesAsync();
                    data.IsSuccess = true;
                }
                else
                {
                    data.IsSuccess = false;
                    data.Message = "疑似入库单信息重复, 请重新输入";
                }
            }
            catch(InvalidOperationException ex)
            {
                data.IsSuccess = false;
                data.Message = ex.Message;
            }

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetInboundReceiptList(ReceiptQueryViewModel receiptQuery)
        {
            var data = new MessageDataViewModel();
            if (receiptQuery == null)
            {
                data.IsSuccess = false;
                return Json(data);
            }

            var (list, total) = await _repository.GetInboundReceiptListAsync(receiptQuery);
            data.Data = new PageDataViewModel<InboundReceiptDisplayViewModel>()
            {
                Total = total,
                Data = _mapper.Map<IList<InboundReceiptDisplayViewModel>>(list)
            };
            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInboundReceipt(string id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteInboundReceiptAsync(id)
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
