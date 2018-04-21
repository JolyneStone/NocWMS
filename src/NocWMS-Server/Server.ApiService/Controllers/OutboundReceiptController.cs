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
    public class OutboundReceiptController : ControllerBase<OutboundReceiptController, IOutboundReceiptRepository>
    {
        public OutboundReceiptController(IInjectService<OutboundReceiptController, IOutboundReceiptRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetOutboundReceipt(string id)
        {
            var data = new MessageDataViewModel
            {
                Data = _mapper.Map<OutboundReceiptDetailViewModel>(_repository.Load(await _repository.GetByIdAsync(id)))
            };

            data.IsSuccess = data.Data != null ? true : false;
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOutboundReceipt([FromBody] OutboundReceiptUpdateViewModel outboundReceiptUpdate)
        {
            var data = new MessageDataViewModel();
            if (outboundReceiptUpdate == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该出库单信息";
                return Json(data);
            }

            var outboundReceipt = await _repository.GetByIdAsync(outboundReceiptUpdate.Id);
            if (outboundReceipt == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该出库单信息";
                return Json(data);
            }
            else
            {
                _repository.UpdatOutboundReceipt(outboundReceipt, outboundReceiptUpdate.IsDone);
            }

            await _uf.SaveChangesAsync();
            data.IsSuccess = true;
            data.Data = _mapper.Map<OutboundReceiptFormViewModel>(outboundReceipt);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddOutboundReceipt([FromBody] OutboundReceiptFormViewModel outboundReceiptForm)
        {
            var data = new MessageDataViewModel();
            if (outboundReceiptForm == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该出库单信息";
                return Json(data);
            }

            if (!ModelState.IsValid)
            {
                data.IsSuccess = false;
                data.Message = ModelState.Values.First().Errors.FirstOrDefault()?.ErrorMessage;
                return Json(data);
            }
            if (outboundReceiptForm.OutboundReceiptItems == null || !outboundReceiptForm.OutboundReceiptItems.Any())
            {
                data.IsSuccess = false;
                data.Message = "出库项目不可为空";
                return Json(data);
            }
            try
            {
                var (result, outboundReceipt) = await _repository.TryAddOutboundReceiptAsync(User.Claims.First(x => x.Type == "email").Value, outboundReceiptForm);
                if (result)
                {
                    await _uf.SaveChangesAsync();
                    data.IsSuccess = true;
                }
                else
                {
                    data.IsSuccess = false;
                    data.Message = "疑似出库单信息重复, 请重新输入";
                }
            }
            catch (InvalidOperationException ex)
            {
                data.IsSuccess = false;
                data.Message = ex.Message;
            }

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOutboundReceiptList(ReceiptQueryViewModel receiptQuery)
        {
            var data = new MessageDataViewModel();
            if (receiptQuery == null)
            {
                data.IsSuccess = false;
                return Json(data);
            }

            var (list, total) = await _repository.GetOutboundReceiptListAsync(receiptQuery);
            data.Data = new PageDataViewModel<OutboundReceiptDisplayViewModel>()
            {
                Total = total,
                Data = _mapper.Map<IList<OutboundReceiptDisplayViewModel>>(list)
            };
            data.IsSuccess = data.Data == null ? false : true;
            return Json(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOutboundReceipt(string id)
        {
            var data = new MessageDataViewModel
            {
                IsSuccess = await _repository.DeleteOutboundReceiptAsync(id)
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
