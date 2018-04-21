using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<Staff> CreateStaffAsync(string userName, string email, StaffFormViewModel staffForm);
        void UpdatStaff(Staff staff, StaffFormViewModel staffForm);
        Task<Staff> GetByUserInfoIdAsync(string userInfo);
        Task<Staff> GetByEmailAsync(string email);
        Task<Staff> AddStaffAsync(StaffAddFormViewModel staffAddForm);
        Task<ValueTuple<bool, Staff>> TryAddStaffAsync(StaffAddFormViewModel staffAddForm);
        Task<ValueTuple<IList<Staff>, int>> GetStaffListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Staff>, int>> GetStaffListAsync(string query, int page, int pageSize);
        Task<bool> DeleteStaffAsync(string id, string email);
    }
}
