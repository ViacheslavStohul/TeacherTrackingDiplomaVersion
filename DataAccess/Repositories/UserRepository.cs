using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<string> GetUserPasswordHashAsync(string login)
        {
            return await this.context.Users.Where(u => u.Login == login).Select(s => s.Password).FirstOrDefaultAsync();
        }

        public bool GetIsUserInBanAsync(string ip)
        {
            return this.context.BanLogs.Any(b => b.Ip == ip && b.BanEnded > DateTime.Now);
        }

        public UserInfo GetUserInfoByLogin(string login)
        {
            return this.context.UserInfos.Where(u => u.User.Login == login).Include(u => u.AccessLevel).FirstOrDefault();
        }

        public UserInfo GetFullUserInfo(int id)
        {
            return this.context.UserInfos
                .Where(u => u.IdUserInfo == id)
                .Include(u => u.Chair)
                .Include(u => u.Comission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Category)
                .Include(u => u.Rank)
                .Include(u => u.WorkType)
                .Include(u => u.OrganizationalWorks)
                .Include(u => u.Qualifications)
                .Include(u => u.MethodicalWorks)
                .FirstOrDefault();
        }

        public async Task<int> UpdateUserBasicInfo(int id, string firstName, string secondName, string middleName, string phone, string email)
        {
            UserInfo toUpdate = await this.context.UserInfos.Where(u => u.IdUserInfo == id).FirstOrDefaultAsync();

            toUpdate.FirstName = firstName;
            toUpdate.SecondName = secondName;
            toUpdate.MiddleName = middleName;
            toUpdate.Phone = phone;
            toUpdate.Email = email;
            return await this.context.SaveChangesAsync();
        }

        public async Task<List<UserInfo>> GetUsersAsync()
        {
            return await this.context.UserInfos
                .Include(u => u.Chair)
                .Include(u => u.Comission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Category)
                .Include(u => u.Rank)
                .Include(u => u.WorkType)
                .ToListAsync();
        }

        public async Task<List<UserInfo>> GetUsersByChairAsync(UserInfo user)
        {
            if (user.Chair != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Chair == user.Chair)
                    .Include(u => u.Chair)
                    .Include(u => u.Comission)
                    .Include(u => u.AccessLevel)
                    .Include(u => u.Category)
                    .Include(u => u.Rank)
                    .Include(u => u.WorkType)
                    .ToListAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserInfo>> GetUsersByDepartmentAsync(UserInfo user)
        {
            if (user.Chair != null && user.Comission != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Chair == user.Chair && u.Comission == user.Comission)
                    .Include(u => u.Chair)
                    .Include(u => u.Comission)
                    .Include(u => u.AccessLevel)
                    .Include(u => u.Category)
                    .Include(u => u.Rank)
                    .Include(u => u.WorkType)
                    .ToListAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserInfo>> GetUsersByComissionAsync(UserInfo user)
        {
            if (user.Comission != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Comission == user.Comission)
                    .Include(u => u.Chair)
                    .Include(u => u.Comission)
                    .Include(u => u.AccessLevel)
                    .Include(u => u.Category)
                    .Include(u => u.Rank)
                    .Include(u => u.WorkType)
                    .ToListAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            return await this.context.UserInfos
                .Where(u => u.IdUserInfo == id)
                .Include(u => u.Chair)
                .Include(u => u.Comission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Category)
                .Include(u => u.Rank)
                .Include(u => u.WorkType).FirstOrDefaultAsync();
        }
    }
}
