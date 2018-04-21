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
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        public StaffRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Staff> CreateStaffAsync(string userName, string email, StaffFormViewModel staffForm)
        {
            if(staffForm == null)
            {
                throw new ArgumentNullException("param staffForm is NULL in StaffRepository.CreateStaffAnsync");
            }

            var staff = new Staff
            {
                Id = GenerageNumber.GetStaffNumber(),
                Email = email,
                UserInfoId = (await _dbContext.Set<UserInfo>().FirstOrDefaultAsync(x => x.UserName == userName)).Id,
                StaffName = staffForm.StaffName,
                Telephone = staffForm.Telephone,
                Duty = staffForm.Duty,
                Gender = staffForm.Gender,
                Remarks = staffForm.Remarks,
                QQNumber = staffForm.QQNumber
            };

            await this.InsertAsync(staff);
            return staff;
        }

        public void UpdatStaff(Staff staff, StaffFormViewModel staffForm)
        {
            if (staff == null || staffForm == null)
            {
                return;
            }

            staff.StaffName = staffForm.StaffName;
            staff.Telephone = staffForm.Telephone;
            staff.QQNumber = staffForm.QQNumber;
            staff.Duty = staffForm.Duty;
            staff.Remarks = staffForm.Remarks;
            staff.Gender = staffForm.Gender;

            this.Update(staff);
        }

        public async Task<Staff> GetByUserInfoIdAsync(string userInfoId)
        {
            if (String.IsNullOrWhiteSpace(userInfoId))
            {
                return null;
            }

            return await this.GetFirstOrDefaultAsync(x => x.UserInfoId == userInfoId);
        }

        public async Task<Staff> GetByEmailAsync(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await this.GetFirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Staff> AddStaffAsync(StaffAddFormViewModel staffAddForm)
        {
            if(staffAddForm == null)
            {
                throw new ArgumentNullException("param staffAddForm is NULL in StaffRepository.AddStaffAnsync");
            }

            var staff = new Staff()
            {
                Id = GenerageNumber.GetStaffNumber(),
                StaffName = staffAddForm.StaffName,
                Duty = staffAddForm.Duty,
                Email = staffAddForm.Email,
                Gender = staffAddForm.Gender,
                QQNumber = staffAddForm.QQNumber,
                Remarks = staffAddForm.Remarks,
                Telephone = staffAddForm.Telephone
            };

            await this.InsertAsync(staff);
            return staff;
        }

        public async Task<ValueTuple<bool, Staff>> TryAddStaffAsync(StaffAddFormViewModel staffAddForm)
        {
            if (staffAddForm == null)
            {
                return (false, null);
            }

            if((await this.GetByEmailAsync(staffAddForm.Email)) != null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddStaffAsync(staffAddForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Staff>, int>> GetStaffListAsync(int page, int pageSize)
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

        public async Task<ValueTuple<IList<Staff>, int>> GetStaffListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.StaffName, $"%{query}%")))
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteStaffAsync(string id, string email)
        {
            var staff = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (staff != null)
            {
                if(staff.Email == email) // 不能删除自己
                {
                    return false;
                }

                this.Delete(staff);
                return true;
            }

            return false;
        }
    }
}
