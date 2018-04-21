using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Server.ApiService.Repositories
{
    public class InventoryRepository : Repository<Inventory, int>, IInventoryRepository
    {
        public InventoryRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public IIncludableQueryable<Inventory, WarehouseCell> Include()
        {
            // 在ThenInclude中使用集合导航属性时, VS的提示是有错的, 可以忽略它
            // https://docs.microsoft.com/zh-cn/ef/core/querying/related-data
            // https://github.com/dotnet/roslyn/issues/8237
            return this.Entities
                .Include(x => x.Category)
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Include(x => x.InventoryCells)
                .ThenInclude(y => y.WarehouseCell);
        }

        public async Task UpdatInventoryAsync(InventoryAddFormViewModel inventoryAddForm)
        {
            if (inventoryAddForm == null)
            {
                return;
            }

            var inventory = await this.GetByIdAsync(inventoryAddForm.Id);
            if(inventory == null)
            {
                throw new InvalidOperationException("can't find InventoryRepository.UpdateInventoryAsync");
            }

            inventory.BookInventory = inventoryAddForm.BookInventory;
            inventory.RealInventory = inventoryAddForm.RealInventory;

            this.Update(inventory);
        }

        public async Task<ValueTuple<IList<Inventory>, int>> GetInventoryListAsync(int warehouseId, int categoryId, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = this.Include().Where(x =>
                (warehouseId == -1 ? true : x.WarehouseId == warehouseId) &&
                (categoryId == -1 ? true : x.CategoryId == categoryId));
            return (await list
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync(), list.Count());
        }

        public async Task<ValueTuple<IList<Inventory>, int>> GetInventoryListAsync(int warehouseId, int categoryId, string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = this.Include().Where(x =>
                warehouseId == -1 ? true : x.WarehouseId == warehouseId &&
                categoryId == -1 ? true : x.CategoryId == categoryId &&
                EF.Functions.Like(x.Product.ProductName, $"%{query}%"))
                .AsNoTracking();
            return (await (list
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync()), list.Count());
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var inventory = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (inventory != null)
            {
                var cells = this.Entry(inventory)
                            .Collection(x => x.InventoryCells)
                            .Query();
                (_dbContext as NocDbContext).InventoryCells.RemoveRange(cells);
                this.Delete(inventory);
                return true;
            }

            return false;
        }
    }
}
