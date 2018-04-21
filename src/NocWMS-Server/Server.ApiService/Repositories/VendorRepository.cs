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
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdatVendor(Vendor vendor, VendorFormViewModel vendorForm)
        {
            if (vendor == null || vendorForm == null)
            {
                return;
            }

            vendor.VendorName = vendorForm.VendorName;
            vendor.Telephone = vendorForm.Telephone;
            vendor.PostCode = vendorForm.PostCode;
            vendor.Email = vendor.Email;
            vendor.Fax = vendor.Fax;
            vendor.Contact = vendor.Contact;
            vendor.Duty = vendorForm.Duty;
            vendor.Remarks = vendorForm.Remarks;
            vendor.Gender = vendorForm.Gender;

            this.Update(vendor);
        }

        public async Task<Vendor> AddVendorAsync(VendorFormViewModel vendorForm)
        {
            if (vendorForm == null)
            {
                throw new ArgumentNullException("param vendorForm is NULL in VendorRepository.AddVendorAnsync");
            }

            var vendor = new Vendor()
            {
                Id = GenerageNumber.GetVendorNumber(),
                VendorName = vendorForm.VendorName,
                Duty = vendorForm.Duty,
                Email = vendorForm.Email,
                Gender = vendorForm.Gender,
                Address = vendorForm.Address,
                Contact = vendorForm.Contact,
                Fax = vendorForm.Fax,
                PostCode = vendorForm.PostCode,
                Remarks = vendorForm.Remarks,
                Telephone = vendorForm.Telephone
            };

            await this.InsertAsync(vendor);
            return vendor;
        }

        public async Task<ValueTuple<bool, Vendor>> TryAddVendorAsync(VendorFormViewModel vendorForm)
        {
            if (vendorForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddVendorAsync(vendorForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Vendor>, int>> GetVendorListAsync(int page, int pageSize)
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

        public async Task<ValueTuple<IList<Vendor>, int>> GetVendorListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.VendorName, $"%{query}%")))
                .AsNoTracking();
            return (await list 
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteVendorAsync(string id)
        {
            var vendor = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (vendor != null)
            {
                this.Delete(vendor);
                return true;
            }

            return false;
        }
    }
}
