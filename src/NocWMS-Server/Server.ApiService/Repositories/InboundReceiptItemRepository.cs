using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;

namespace Server.ApiService.Repositories
{
    public class InboundReceiptItemRepository : Repository<InboundReceiptItem>, IInboundReceiptItemRepository
    {
        private readonly IInventoryCellRepository _inventoryCellRepository;
        public InboundReceiptItemRepository(NocDbContext dbContext, IInventoryCellRepository inventoryCellRepository) : base(dbContext)
        {
            _inventoryCellRepository = inventoryCellRepository;
        }

        public InboundReceiptItem Load(InboundReceiptItem inboundReceiptItem)
        {
            this.Entry(inboundReceiptItem)
                .Reference(x => x.Category)
                .Load();

            this.Entry(inboundReceiptItem)
                .Reference(x => x.Product)
                .Load();

            this.Entry(inboundReceiptItem)
                .Reference(x => x.WarehouseCell)
                .Load();

            return inboundReceiptItem;
        }

        public async Task AddRangeAsync(string id, int warehouseId, InboundReceiptItemAddFormViewModel[] inboundReceiptItemAddForms)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return;
            }

            if (inboundReceiptItemAddForms == null && !inboundReceiptItemAddForms.Any())
            {
                return;
            }

            var receipts = inboundReceiptItemAddForms.Select(x => new InboundReceiptItem
            {
                CategoryId = x.CategoryId,
                InboundReceiptId = id,
                WarehouseCellId = x.WarehouseCellId,
                ProductId = x.ProductId,
                Num = x.Num,
                Price = x.Price
            });

            foreach(var receipt in receipts)
            {
                await _inventoryCellRepository.UpdateOrAddInboundAsync(warehouseId, receipt);
            }

            await this.InsertRangeAsync(receipts);
        }

        public async Task<IList<InboundReceiptItem>> GetInboundReceiptItemListAsync(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new List<InboundReceiptItem>();
            }

            return (await this.GetAllAsync(x => x.InboundReceiptId == id)).ToList();
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
