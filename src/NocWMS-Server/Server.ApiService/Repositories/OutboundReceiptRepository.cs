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
    public class OutboundReceiptRepository : Repository<OutboundReceipt>, IOutboundReceiptRepository
    {
        private readonly IOutboundReceiptItemRepository _itemRepository;
        public OutboundReceiptRepository(NocDbContext dbContext, IOutboundReceiptItemRepository outboundReceiptItemRepository) : base(dbContext)
        {
            _itemRepository = outboundReceiptItemRepository;
        }

        public OutboundReceipt Load(OutboundReceipt outboundReceipt)
        {
            this.Entry(outboundReceipt)
                .Reference(x => x.Customer)
                .Load();

            this.Entry(outboundReceipt)
                .Reference(x => x.Warehouse)
                .Load();

            this.Entry(outboundReceipt)
                .Reference(x => x.Staff)
                .Load();

            this.Entry(outboundReceipt)
                .Collection(x => x.OutboundReceiptItems)
                .Load();

            foreach (var item in outboundReceipt.OutboundReceiptItems)
            {
                _itemRepository.Load(item);
            }

            return outboundReceipt;
        }


        public IIncludableQueryable<OutboundReceipt, IList<OutboundReceiptItem>> Include()
        {
            return this.Include(x => x.Warehouse)
                .Include(x => x.Customer)
                .Include(x => x.OutboundReceiptItems);
        }

        public void UpdatOutboundReceipt(OutboundReceipt outboundReceipt, bool isDone)
        {
            if (outboundReceipt == null)
            {
                return;
            }

            outboundReceipt.IsDone = isDone;
            this.Update(outboundReceipt);
        }

        public async Task<OutboundReceipt> AddOutboundReceiptAsync(string email, OutboundReceiptFormViewModel outboundReceiptAddForm)
        {
            if (outboundReceiptAddForm == null)
            {
                throw new ArgumentNullException("param outboundReceiptAddForm is NULL in OutboundReceiptRepository.AddOutboundReceiptAnsync");
            }

            var db = (_dbContext as NocDbContext);
            var staff = await db.Staffs.FirstOrDefaultAsync(x => x.Email == email);
            if (staff == null)
            {
                throw new InvalidOperationException("can't find staff in OutboundReceiptRepository.AddOutboundReceiptAsync");
            }

            var customer = await db.Customers.FirstOrDefaultAsync(x => x.CustomerName == outboundReceiptAddForm.CustomerName);
            if (customer == null)
            {
                throw new InvalidOperationException("can't find customer in OutboundReceiptRepository.AddOutboundReceiptAsync");
            }

            var outboundReceipt = new OutboundReceipt()
            {
                Id = GenerageNumber.GetOutboundReceiptNumber(),
                StaffId = staff.Id,
                CustomerId = customer.Id,
                WarehouseId = outboundReceiptAddForm.WarehouseId,
                WaybillNo = outboundReceiptAddForm.WaybillNo,
                Acceptor = outboundReceiptAddForm.Acceptor,
                Deliveryman = outboundReceiptAddForm.Deliveryman,
                IsDone = outboundReceiptAddForm.IsDone,
                HandlerName = staff.StaffName
            };

            await this.InsertAsync(outboundReceipt);
            await _itemRepository.AddRangeAsync(outboundReceipt.Id, outboundReceiptAddForm.WarehouseId, outboundReceiptAddForm.OutboundReceiptItems);

            return outboundReceipt;
        }

        public async Task<ValueTuple<bool, OutboundReceipt>> TryAddOutboundReceiptAsync(string email, OutboundReceiptFormViewModel outboundReceiptAddForm)
        {
            if (outboundReceiptAddForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddOutboundReceiptAsync(email, outboundReceiptAddForm));
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

        public async Task<ValueTuple<IList<OutboundReceipt>, int>> GetOutboundReceiptListAsync(ReceiptQueryViewModel receiptQuery)
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
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteOutboundReceiptAsync(string id)
        {
            var outboundReceipt = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (outboundReceipt != null)
            {
                this.Delete(outboundReceipt);
                return true;
            }

            return false;
        }
    }
}
