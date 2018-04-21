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
    public class WarehouseRepository : Repository<Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public Warehouse LoadManager(Warehouse warehouse)
        {
            this.Entry(warehouse)
                .Reference(x => x.Staff)
                .Load();
            
            return warehouse;
        }

        public Warehouse LoadWarehouseCells(Warehouse warehouse)
        {
            this.Entry(warehouse) // 显示加载
                .Collection(x => x.Cells)
                .Load();
            return warehouse;
        }

        public async Task UpdatWarehouseAsync(Warehouse warehouse, WarehouseFormViewModel warehouseForm)
        {
            if (warehouse == null || warehouseForm == null)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(warehouseForm.ManagerName))
            {
                throw new InvalidOperationException("WarehouseFormViewModel.ManagerName is not NULL");
            }

            var staff = await (_dbContext as NocDbContext).Staffs.FirstOrDefaultAsync(x => x.StaffName == warehouseForm.ManagerName);
            if (staff == null)
            {
                throw new InvalidOperationException("Can't find the staff");
            }

            warehouse.WarehouseName = warehouseForm.WarehouseName;
            warehouse.Province = warehouseForm.Province;
            warehouse.Remarks = warehouseForm.Remarks;
            warehouse.Address = warehouseForm.Address;
            warehouse.StaffId = staff.Id;
            this.Update(warehouse);
        }

        public async Task<Warehouse> AddWarehouseAsync(WarehouseFormViewModel warehouseForm)
        {
            if (warehouseForm == null)
            {
                throw new ArgumentNullException("param warehouseAddForm is NULL in WarehouseRepository.AddWarehouseAnsync");
            }

            if (String.IsNullOrWhiteSpace(warehouseForm.ManagerName))
            {
                throw new InvalidOperationException("WarehouseFormViewModel.ManagerName is not NULL");
            }

            var staff = await (_dbContext as NocDbContext).Staffs.FirstOrDefaultAsync(x => x.StaffName == warehouseForm.ManagerName);
            if (staff == null)
            {
                throw new InvalidOperationException("Can't find the staff");
            }


            var warehouse = new Warehouse
            {
                WarehouseName = warehouseForm.WarehouseName,
                Remarks = warehouseForm.Remarks,
                Address = warehouseForm.Address,
                Province = warehouseForm.Province,
                StaffId = staff.Id
            };
            await this.InsertAsync(warehouse);
            return warehouse;
        }

        public async Task<ValueTuple<bool, Warehouse>> TryAddWarehouseAsync(WarehouseFormViewModel warehouseForm)
        {
            if (warehouseForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddWarehouseAsync(warehouseForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Warehouse>, int>> GetWarehouseListAsync(int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync())
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<ValueTuple<IList<Warehouse>, int>> GetWarehouseListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.WarehouseName, $"%{query}%")))
                .OrderBy(x => x.CreateTime);
            return (await list 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteWarehouseAsync(int id)
        {
            var warehouse = await this.GetByIdAsync(id);
            if (warehouse != null)
            {
                var db = _dbContext as NocDbContext;
                var cells = await db.WarehouseCells.Where(x => x.WarehouseId == id).ToArrayAsync();
                db.WarehouseCells.RemoveRange(cells);
                this.Delete(warehouse);
                return true;
            }

            return false;
        }
    }
}
