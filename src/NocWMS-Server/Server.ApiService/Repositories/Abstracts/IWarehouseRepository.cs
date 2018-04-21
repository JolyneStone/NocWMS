using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IWarehouseRepository: IRepository<Warehouse, int>
    {
        Task<ValueTuple<IList<Warehouse>, int>> GetWarehouseListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Warehouse>, int>> GetWarehouseListAsync(string query, int page, int pageSize);
        Task UpdatWarehouseAsync(Warehouse warehouse, WarehouseFormViewModel warehouseForm);
        Task<Warehouse> AddWarehouseAsync(WarehouseFormViewModel warehouseAddForm);
        Task<ValueTuple<bool, Warehouse>> TryAddWarehouseAsync(WarehouseFormViewModel warehouseForm);
        Task<bool> DeleteWarehouseAsync(int id);
        Warehouse LoadManager(Warehouse warehouse);
        Warehouse LoadWarehouseCells(Warehouse warehouse);
    }
}
