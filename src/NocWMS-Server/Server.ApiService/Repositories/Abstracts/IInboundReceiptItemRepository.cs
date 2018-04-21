using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IInboundReceiptItemRepository: IRepository<InboundReceiptItem>
    {
        InboundReceiptItem Load(InboundReceiptItem inboundReceiptItem);
        Task AddRangeAsync(string id, int warehouseId, InboundReceiptItemAddFormViewModel[] inboundReceiptItemAddForms);

        Task<IList<InboundReceiptItem>> GetInboundReceiptItemListAsync(string id);
        Task<bool> DeleteRangeAsync(string[] ids);
    }
}
