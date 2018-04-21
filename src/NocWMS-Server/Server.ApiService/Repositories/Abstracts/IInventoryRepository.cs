using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IInventoryRepository: IRepository<Inventory, int>
    {
        IIncludableQueryable<Inventory, WarehouseCell> Include();
        Task UpdatInventoryAsync(InventoryAddFormViewModel inventoryForm);
        Task<ValueTuple<IList<Inventory>, int>> GetInventoryListAsync(int warehouseId, int categoryId, int page, int pageSize);
        Task<ValueTuple<IList<Inventory>, int>> GetInventoryListAsync(int warehouseId, int categoryId, string query, int page, int pageSize);
        Task<bool> DeleteInventoryAsync(int id);
    }
}
