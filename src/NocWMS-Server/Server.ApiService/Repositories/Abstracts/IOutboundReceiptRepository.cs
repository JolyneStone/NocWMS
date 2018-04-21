using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IOutboundReceiptRepository: IRepository<OutboundReceipt>
    {
        OutboundReceipt Load(OutboundReceipt outboundReceipt);
        IIncludableQueryable<OutboundReceipt, IList<OutboundReceiptItem>> Include();
        void UpdatOutboundReceipt(OutboundReceipt outboundReceipt, bool isDone);
        Task<OutboundReceipt> AddOutboundReceiptAsync(string email, OutboundReceiptFormViewModel outboundReceiptAddForm);
        Task<ValueTuple<bool, OutboundReceipt>> TryAddOutboundReceiptAsync(string email, OutboundReceiptFormViewModel outboundReceiptAddForm);
        Task<ValueTuple<IList<OutboundReceipt>, int>> GetOutboundReceiptListAsync(ReceiptQueryViewModel receiptQuery);
        Task<bool> DeleteOutboundReceiptAsync(string id);
    }
}
