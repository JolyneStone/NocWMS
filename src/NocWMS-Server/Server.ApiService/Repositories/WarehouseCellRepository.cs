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
    public class WarehouseCellRepository : Repository<WarehouseCell, int>, IWarehouseCellRepository
    {
        public WarehouseCellRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdatWarehouseCell(WarehouseCell warehouseCell, WarehouseCellFormViewModel warehouseCellForm)
        {
            if (warehouseCell == null || warehouseCellForm == null)
            {
                return;
            }

            warehouseCell.Volume = warehouseCellForm.Volume;
            warehouseCell.RemainderVolume = warehouseCellForm.RemainderVolume;
            warehouseCell.Status = warehouseCell.Status;
            warehouseCell.CellName = warehouseCellForm.CellName;
            this.Update(warehouseCell);
        }

        public async Task<WarehouseCell> AddWarehouseCellAsync(WarehouseCellFormViewModel warehouseCellForm)
        {
            if (warehouseCellForm == null)
            {
                throw new ArgumentNullException("param warehouseCellAddForm is NULL in WarehouseCellRepository.AddWarehouseCellAnsync");
            }

            var warehouseCell = new WarehouseCell
            {
                CellName = warehouseCellForm.CellName,
                WarehouseId = warehouseCellForm.WarehouseId,
                Status = warehouseCellForm.Status,
                Volume = warehouseCellForm.Volume,
                RemainderVolume = warehouseCellForm.RemainderVolume
            };
            await this.InsertAsync(warehouseCell);
            return warehouseCell;
        }

        public async Task<ValueTuple<bool, WarehouseCell>> TryAddWarehouseCellAsync(WarehouseCellFormViewModel warehouseCellForm)
        {
            if (warehouseCellForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddWarehouseCellAsync(warehouseCellForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<WarehouseCell>, int>> GetWarehouseCellListAsync(int id, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => x.WarehouseId == id))
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<ValueTuple<IList<WarehouseCell>, int>> GetWarehouseCellListAsync(int id, string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => x.WarehouseId == id && EF.Functions.Like(x.CellName, $"%{query}%")))
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteWarehouseCellAsync(int id)
        {
            var warehouseCell = await this.GetByIdAsync(id);
            if (warehouseCell != null)
            {
                this.Delete(warehouseCell);
                return true;
            }

            return false;
        }
    }
}
