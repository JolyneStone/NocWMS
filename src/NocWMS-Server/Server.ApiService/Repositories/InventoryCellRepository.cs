using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;

namespace Server.ApiService.Repositories
{
    public class InventoryCellRepository : Repository<InventoryCell, int>, IInventoryCellRepository
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryCellRepository(NocDbContext dbContext, IInventoryRepository inventoryRepository) : base(dbContext)
        {
            _inventoryRepository = inventoryRepository;
        }

        public IIncludableQueryable<InventoryCell, WarehouseCell> Include()
        {
            return this.Entities
                .Include(x => x.WarehouseCell);
        }

        public async Task UpdateOrAddInboundAsync(int warehouseId, InboundReceiptItem inboundReceiptItem)
        {
            if (inboundReceiptItem == null)
            {
                throw new ArgumentNullException(nameof(inboundReceiptItem));
            }

            var inventory = await _inventoryRepository.GetAsync(x => x.WarehouseId == warehouseId && x.ProductId == inboundReceiptItem.ProductId);
            if(inventory == null)
            {

                inventory = new Inventory
                {
                    CategoryId = inboundReceiptItem.CategoryId,
                    ProductId = inboundReceiptItem.ProductId,
                    WarehouseId = warehouseId,
                    RealInventory = 0,
                    BookInventory = 0
                };

                await _inventoryRepository.InsertAsync(inventory);
                await (_dbContext as NocDbContext).SaveChangesAsync();
            }
            var tmp = await this.GetFirstOrDefaultAsync(x => x.InventoryId == inventory.Id && x.Id == inboundReceiptItem.WarehouseCellId);
            if (tmp == null)
            {
                tmp = new InventoryCell
                {
                    Id = inboundReceiptItem.WarehouseCellId,
                    InventoryId = inventory.Id,
                    Num = inboundReceiptItem.Num,
                    ReceiptId = inboundReceiptItem.InboundReceiptId,
                    ReceiptType = ReceiptType.Inbound,
                    UpdateTime = DateTime.Now
                };

                await this.InsertAsync(tmp);
            }
            else
            {
                tmp.Num += inboundReceiptItem.Num;
                tmp.ReceiptType = ReceiptType.Inbound;
                tmp.UpdateTime = DateTime.Now;

                this.Update(tmp);
            }

            inventory.BookInventory += inboundReceiptItem.Num;
            inventory.RealInventory += inboundReceiptItem.Num;
            _inventoryRepository.Update(inventory);
        }

        public async Task UpdateOrDeleteOutboundAsync(int warehouseId, OutboundReceiptItem outboundReceiptItem)
        {
            if (outboundReceiptItem == null)
            {
                throw new ArgumentNullException(nameof(outboundReceiptItem));
            }

            var inventory = await _inventoryRepository.GetAsync(x => x.WarehouseId == warehouseId && x.ProductId == outboundReceiptItem.ProductId);
            if (inventory == null)
            {
                throw new InvalidOperationException("can't find the Inventory in InventoryCellRepository.UpdateOrAddInboundAsync");
            }

            var tmp = await this.GetFirstOrDefaultAsync(x => x.InventoryId == inventory.Id && x.Id == outboundReceiptItem.WarehouseCellId);
            if (tmp == null)
            {
                throw new InvalidOperationException("can't find the InventoryCell in InventoryCellRepository.UpdateOrDeleteOutboundAsync");
            }

            if (tmp.Num < outboundReceiptItem.Num)
            {
                throw new InvalidOperationException("the number of out libraries is greater than inventory");
            }

            if(tmp.Num == outboundReceiptItem.Num)
            {
                this.Delete(tmp);
            }
            else
            {
                tmp.Num -= outboundReceiptItem.Num;
                tmp.ReceiptId = outboundReceiptItem.OutboundReceiptId;
                tmp.ReceiptType = ReceiptType.Outbound;
                tmp.UpdateTime = DateTime.Now;
                this.Update(tmp);
            }

            inventory.BookInventory -= outboundReceiptItem.Num;
            inventory.RealInventory -= outboundReceiptItem.Num;
            _inventoryRepository.Update(inventory);
        }


        public void UpdatInventoryCell(InventoryCell inventoryCell, InventoryCellFormViewModel inventoryCellForm)
        {
            if (inventoryCell == null || inventoryCellForm == null)
            {
                return;
            }

            inventoryCell.Num = inventoryCellForm.Num;
            inventoryCell.UpdateTime = DateTime.Now;
            this.Update(inventoryCell);
        }

        public async Task<InventoryCell> AddInventoryCellAsync(InventoryCellFormViewModel inventoryCellForm)
        {
            if (inventoryCellForm == null)
            {
                throw new ArgumentNullException("param inventoryCellForm is NULL in InventoryCellRepository.AddInventoryCellAnsync");
            }

            var inventoryCell = new InventoryCell()
            {
                Id = inventoryCellForm.WarehouseCellId,
                InventoryId = inventoryCellForm.InventoryId,
                Num = inventoryCellForm.Num,
                ReceiptType = inventoryCellForm.ReceiptType,
                ReceiptId = inventoryCellForm.ReceiptId
            };

            await this.InsertAsync(inventoryCell);
            return inventoryCell;
        }

        public async Task<ValueTuple<bool, InventoryCell>> TryAddInventoryCellAsync(InventoryCellFormViewModel inventoryCellForm)
        {
            if (inventoryCellForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddInventoryCellAsync(inventoryCellForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<IList<InventoryCell>> GetInventoryCellListAsync(int id, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            return await (this.Include().Where(x=>x.InventoryId == id))
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IList<InventoryCell>> GetInventoryCellListAsync(int id, string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            return await (this.Include().Where(x => EF.Functions.Like(x.WarehouseCell.CellName, $"%{query}%")))
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> DeleteInventoryCellAsync(int id)
        {
            var inventoryCell = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (inventoryCell != null)
            {
                this.Delete(inventoryCell);
                return true;
            }

            return false;
        }
    }
}
