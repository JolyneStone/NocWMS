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
    public class InboundReceiptRepository : Repository<InboundReceipt>, IInboundReceiptRepository
    {
        private readonly IInboundReceiptItemRepository _itemRepository;
        public InboundReceiptRepository(NocDbContext dbContext, IInboundReceiptItemRepository inboundReceiptItemRepository) : base(dbContext)
        {
            _itemRepository = inboundReceiptItemRepository;
        }

        public InboundReceipt Load(InboundReceipt inboundReceipt)
        {
            this.Entry(inboundReceipt)
                .Reference(x => x.Vendor)
                .Load();

            this.Entry(inboundReceipt)
                .Reference(x => x.Warehouse)
                .Load();

            this.Entry(inboundReceipt)
                .Reference(x => x.Staff)
                .Load();

            this.Entry(inboundReceipt)
                .Collection(x => x.InboundReceiptItems)
                .Load();

            foreach(var item in inboundReceipt.InboundReceiptItems)
            {
                _itemRepository.Load(item);
            }

            return inboundReceipt;
        }

        public IIncludableQueryable<InboundReceipt, IList<InboundReceiptItem>> Include()
        {
            return this.Include(x => x.Warehouse)
                .Include(x => x.Vendor)
                .Include(x => x.InboundReceiptItems);
        }

        public void UpdatInboundReceipt(InboundReceipt inboundReceipt, bool isDone)
        {
            if (inboundReceipt == null)
            {
                return;
            }

            inboundReceipt.IsDone = isDone;

            this.Update(inboundReceipt);
        }

        public async Task<InboundReceipt> AddInboundReceiptAsync(string email, InboundReceiptFormViewModel inboundReceiptAddForm)
        {
            if (inboundReceiptAddForm == null)
            {
                throw new ArgumentNullException("param inboundReceiptAddForm is NULL in InboundReceiptRepository.AddInboundReceiptAnsync");
            }

            var db = (_dbContext as NocDbContext);
            var staff = await db.Staffs.FirstOrDefaultAsync(x => x.Email == email);
            if (staff == null)
            {
                throw new InvalidOperationException("can't find staff in InboundReceiptRepository.AddInboundReceiptAsync");
            }

            var vendor = await db.Vendors.FirstOrDefaultAsync(x => x.VendorName == inboundReceiptAddForm.VendorName);
            if (vendor == null)
            {
                throw new InvalidOperationException("can't find vendor in InboundReceiptRepository.AddInboundReceiptAsync");
            }

            var inboundReceipt = new InboundReceipt()
            {
                Id = GenerageNumber.GetInboundReceiptNumber(),
                StaffId = staff.Id,
                VendorId = vendor.Id,
                WarehouseId = inboundReceiptAddForm.WarehouseId,
                WaybillNo = inboundReceiptAddForm.WaybillNo,
                Acceptor = inboundReceiptAddForm.Acceptor,
                Deliveryman = inboundReceiptAddForm.Deliveryman,
                IsDone = inboundReceiptAddForm.IsDone,
                HandlerName = staff.StaffName
            };

            await this.InsertAsync(inboundReceipt);
            await _itemRepository.AddRangeAsync(inboundReceipt.Id, inboundReceiptAddForm.WarehouseId, inboundReceiptAddForm.InboundReceiptItems);

            return inboundReceipt;
        }

        public async Task<ValueTuple<bool, InboundReceipt>> TryAddInboundReceiptAsync(string email, InboundReceiptFormViewModel inboundReceiptAddForm)
        {
            if (inboundReceiptAddForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddInboundReceiptAsync(email, inboundReceiptAddForm));
            }
            catch (ArgumentNullException)
            {
                return (false, null);
            }
            catch (InvalidOperationException ex)
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<InboundReceipt>, int>> GetInboundReceiptListAsync(ReceiptQueryViewModel receiptQuery)
        {
            var page = receiptQuery.Page > 0 ? receiptQuery.Page : 1;
            var pageSize = receiptQuery.PageSize > 0 ? receiptQuery.PageSize : 5;
            var list = this.Include().Where(x =>
                     (receiptQuery.WarehouseId == -1 ? true : x.WarehouseId == receiptQuery.WarehouseId) &&
                    x.CreateTime >= receiptQuery.StartDate &&
                    x.CreateTime <= receiptQuery.EndDate
                );
            return (await list 
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(),list.Count());
        }

        public async Task<bool> DeleteInboundReceiptAsync(string id)
        {
            var inboundReceipt = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (inboundReceipt != null)
            {
                this.Delete(inboundReceipt);
                return true;
            }

            return false;
        }
    }
}
