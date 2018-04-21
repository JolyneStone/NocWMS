using System;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Repositories.Abstracts;

namespace Server.ApiService.Repositories
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserInfo> GetByNameAsync(string userName)
        {
            return await this.GetFirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<UserInfo> CreateUserInfoAsync(string userName, string role = null)
        {
            var userInfo = new UserInfo
            {
                Avatar = "user_default.jpg",
                Role = String.IsNullOrWhiteSpace(role) ? "user" : role,
                UserName = userName
            };

            await this.InsertAsync(userInfo);
            return userInfo;
        }

        public async Task<bool> IsCompletedAsync(string id, string email)
        {
            var context = this._dbContext as NocDbContext;
            var result = String.IsNullOrWhiteSpace(id) ? false : await context.Staffs.AnyAsync(x => x.UserInfoId == id);
            if (!result && !String.IsNullOrWhiteSpace(email))
            {
                var staff = await context.Staffs.FirstOrDefaultAsync(x => x.Email == email);
                if (staff != null && String.IsNullOrWhiteSpace(staff.UserInfoId))
                {
                    staff.UserInfoId = id;
                    context.Staffs.Update(staff);
                    await context.SaveChangesAsync();
                    result = true;
                }
            }

            return result;
        }
    }
}
