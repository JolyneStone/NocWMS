using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;

namespace Server.ApiService.Repositories
{
    public class OutboundReceiptItemRepository : Repository<OutboundReceiptItem>, IOutboundReceiptItemRepository
    {
        private readonly IInventoryCellRepository _inventoryCellRepository;
        public OutboundReceiptItemRepository(NocDbContext dbContext, IInventoryCellRepository inventoryCellRepository) : base(dbContext)
        {
            _inventoryCellRepository = inventoryCellRepository;
        }

        public OutboundReceiptItem Load(OutboundReceiptItem outboundReceiptItem)
        {
            this.Entry(outboundReceiptItem)
                .Reference(x => x.Category)
                .Load();

            this.Entry(outboundReceiptItem)
                .Reference(x => x.Product)
                .Load();

            this.Entry(outboundReceiptItem)
                .Reference(x => x.WarehouseCell)
                .Load();

            return outboundReceiptItem;
        }

        public async Task AddRangeAsync(string id, int warehouseId, OutboundReceiptItemAddFormViewModel[] outboundReceiptItemAddForms)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return;
            }

            if (outboundReceiptItemAddForms == null && !outboundReceiptItemAddForms.Any())
            {
                return;
            }

            var receipts = outboundReceiptItemAddForms.Select(x => new OutboundReceiptItem
            {
                CategoryId = x.CategoryId,
                OutboundReceiptId = id,
                WarehouseCellId = x.WarehouseCellId,
                ProductId = x.ProductId,
                Num = x.Num,
                Price = x.Price
            });

            foreach (var receipt in receipts)
            {
                await _inventoryCellRepository.UpdateOrDeleteOutboundAsync(warehouseId, receipt);
            }

            await this.InsertRangeAsync(receipts);
        }

        public async Task<IList<OutboundReceiptItem>> GetOutboundReceiptItemListAsync(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new List<OutboundReceiptItem>();
            }

            return (await this.GetAllAsync(x => x.OutboundReceiptId == id)).ToList();
        }

        public async Task<bool> DeleteRangeAsync(string[] ids)
        {
            if (ids == null && !ids.Any())
            {
                return false;
            }

            var items = await this.GetAllAsync(x => ids.Any(y => y == x.Id));
            if (items != null && items.Any())
            {
                this.DeleteRange(items);
                return true;
            }

            return false;
        }
    }
}
