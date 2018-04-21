using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IOutboundReceiptItemRepository: IRepository<OutboundReceiptItem>
    {
        OutboundReceiptItem Load(OutboundReceiptItem outboundReceiptItem);
        Task AddRangeAsync(string id, int warehouseId, OutboundReceiptItemAddFormViewModel[] outboundReceiptItemAddForms);

        Task<IList<OutboundReceiptItem>> GetOutboundReceiptItemListAsync(string id);
        Task<bool> DeleteRangeAsync(string[] ids);
    }
}
