using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IWarehouseCellRepository : IRepository<WarehouseCell, int>
    {
        Task<ValueTuple<IList<WarehouseCell>, int>> GetWarehouseCellListAsync(int id, int page, int pageSize);
        Task<ValueTuple<IList<WarehouseCell>, int>> GetWarehouseCellListAsync(int id, string query, int page, int pageSize);
        void UpdatWarehouseCell(WarehouseCell warehouseCell, WarehouseCellFormViewModel warehouseCellForm);
        Task<WarehouseCell> AddWarehouseCellAsync(WarehouseCellFormViewModel warehouseCellAddForm);
        Task<ValueTuple<bool, WarehouseCell>> TryAddWarehouseCellAsync(WarehouseCellFormViewModel warehouseCellAddForm);
        Task<bool> DeleteWarehouseCellAsync(int id);
    }
}
