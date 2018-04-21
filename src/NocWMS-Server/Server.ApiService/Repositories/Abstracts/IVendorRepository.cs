using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IVendorRepository: IRepository<Vendor>
    {
        void UpdatVendor(Vendor vendor, VendorFormViewModel vendorForm);
        Task<Vendor> AddVendorAsync(VendorFormViewModel vendorAddForm);
        Task<ValueTuple<bool, Vendor>> TryAddVendorAsync(VendorFormViewModel vendorAddForm);
        Task<ValueTuple<IList<Vendor>, int>> GetVendorListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Vendor>, int>> GetVendorListAsync(string query, int page, int pageSize);
        Task<bool> DeleteVendorAsync(string id);
    }
}
