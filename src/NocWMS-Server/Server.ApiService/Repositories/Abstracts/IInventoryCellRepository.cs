using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore.Query;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IInventoryCellRepository: IRepository<InventoryCell, int>
    {
        IIncludableQueryable<InventoryCell, WarehouseCell> Include();
        void UpdatInventoryCell(InventoryCell inventoryCell, InventoryCellFormViewModel inventoryCellForm);
        Task<InventoryCell> AddInventoryCellAsync(InventoryCellFormViewModel inventoryCellForm);
        Task<ValueTuple<bool, InventoryCell>> TryAddInventoryCellAsync(InventoryCellFormViewModel inventoryCellForm);
        Task<IList<InventoryCell>> GetInventoryCellListAsync(int id, int page, int pageSize);
        Task<IList<InventoryCell>> GetInventoryCellListAsync(int id, string query, int page, int pageSize);
        Task UpdateOrAddInboundAsync(int warehouseId, InboundReceiptItem inboundReceiptItem);
        Task UpdateOrDeleteOutboundAsync(int warehouseId, OutboundReceiptItem outboundReceiptItem);
    }
}
