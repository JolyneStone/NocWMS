using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IInboundReceiptRepository : IRepository<InboundReceipt>
    {
        InboundReceipt Load(InboundReceipt inboundReceipt);
        IIncludableQueryable<InboundReceipt, IList<InboundReceiptItem>> Include();
        void UpdatInboundReceipt(InboundReceipt inboundReceipt, bool isDone);
        Task<InboundReceipt> AddInboundReceiptAsync(string email, InboundReceiptFormViewModel inboundReceiptAddForm);
        Task<ValueTuple<bool, InboundReceipt>> TryAddInboundReceiptAsync(string email, InboundReceiptFormViewModel inboundReceiptAddForm);
        Task<ValueTuple<IList<InboundReceipt>, int>> GetInboundReceiptListAsync(ReceiptQueryViewModel receiptQuery);
        Task<bool> DeleteInboundReceiptAsync(string id);
    }
}
