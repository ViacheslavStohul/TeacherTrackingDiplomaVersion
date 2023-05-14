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
            return this.context.UserInfos.Where(u => u.User.Login == login).Include(u => u.AccessLevel).Include(u => u.OrganizationalWorks).FirstOrDefault();
        }

        public UserInfo GetFullUserInfo(int id)
        {
            return this.context.UserInfos
                .Where(u => u.IdUserInfo == id)
                .Include(u => u.Chair)
                .Include(u => u.Commission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Rank)
                .Include(u => u.WorkType)
                .Include(u => u.OrganizationalWorks)
                .ThenInclude(w => w.OrganizationType)
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
            var a = await this.context.UserInfos
                .Include(u => u.Chair)
                .Include(u => u.Commission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Rank)
                .Include(u => u.WorkType)
                .ToListAsync();
            return a;
        }

        public async Task<List<UserInfo>> GetUsersByChairAsync(UserInfo user)
        {
            if (user.Chair != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Chair == user.Chair)
                    .Include(u => u.Chair)
                    .Include(u => u.Commission)
                    .Include(u => u.AccessLevel)
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
            if (user.Chair != null && user.Commission != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Chair == user.Chair && this.context.Departments.Any(d => d.Commissions.Contains(user.Commission) && d.Commissions.Contains(u.Commission)))
                    .Include(u => u.Chair)
                    .Include(u => u.Commission)
                    .Include(u => u.AccessLevel)
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
            if (user.Commission != null)
            {
                return await this.context.UserInfos
                    .Where(u => u.Commission == user.Commission)
                    .Include(u => u.Chair)
                    .Include(u => u.Commission)
                    .Include(u => u.AccessLevel)
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
                .Include(u => u.Commission)
                .Include(u => u.AccessLevel)
                .Include(u => u.Rank)
                .Include(u => u.OrganizationalWorks)
                .Include(u => u.WorkType).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateUserAdminAsync(UserInfo user)
        {
            UserInfo toUpdate = this.context.UserInfos.Where(u => u.IdUserInfo == user.IdUserInfo).FirstOrDefault();

            if (toUpdate == null)
            {
                throw new Exception("Помилка оновлення. Користувача не знайдено");
            }

            toUpdate.AccessLevel = user.AccessLevel;
            toUpdate.Chair = user.Chair;
            toUpdate.Commission = user.Commission;
            toUpdate.FirstName = user.FirstName;
            toUpdate.SecondName = user.SecondName;
            toUpdate.MiddleName = user.MiddleName;
            toUpdate.Phone = user.Phone;
            toUpdate.Email = user.Email;
            toUpdate.Rank = user.Rank;
            toUpdate.WorkType = user.WorkType;

            return await this.context.SaveChangesAsync();
        }

        public async Task<UserInfo> CreateUserAsync(UserInfo userInfo)
        {
            if(this.context.UserInfos.Any(u => u.FirstName.ToLower() == userInfo.FirstName.ToLower() && u.SecondName.ToLower() == userInfo.SecondName.ToLower() && userInfo.MiddleName.ToLower() == userInfo.MiddleName.ToLower()))
            {
                throw new Exception("Такий користувач вже існує");
            }
            userInfo.DeletionDate = DateTime.Now;
            this.context.UserInfos.Add(userInfo);

            await this.context.SaveChangesAsync();

            return userInfo;
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            UserInfo toDelete = this.context.UserInfos.Where(u => u.IdUserInfo == id).FirstOrDefault();

            if (toDelete == null)
            {
                throw new Exception("Такого користувача не існує");
            }

            User toRemove = this.context.Users.Where(u => u.IdUser == id).FirstOrDefault();

            this.context.Users.Remove(toRemove);

            toDelete.DeletionDate = DateTime.Now;

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> RestoreUserAsync(int id, string login, string password)
        {
            if (this.context.Users.Any(u => u.Login == login))
            {
                throw new Exception("Користувач з таким логіном існує");
            }

            UserInfo info = this.context.UserInfos.Where(u => u.IdUserInfo == id).FirstOrDefault();

            if (info == null)
            {
                throw new Exception("Такого користувача не існує");
            }

            this.context.Users.Add(new User
            {
                IdUser = id,
                Login = login,
                Password = password
            });

            info.DeletionDate = null;

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateUsersCommission(Commission commission)
        {
            IQueryable<UserInfo> users = this.context.UserInfos.Where(u => u.Commission == commission);

            foreach(UserInfo user in users)
            {
                user.Chair = this.context.Departments.Where(d => d.Commissions.Contains(commission)).Select(d => d.Chair).FirstOrDefault();
            }

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> SetCommissionHead(string name, Commission commission)
        {
            UserInfo user = this.context.UserInfos.Where(u => u.SecondName + " " + u.FirstName == name).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Такого користувача не існує");
            }

            CommissionHead commissionHead = this.context.ComissionHeads.Where(c => c.Comission == commission).FirstOrDefault();

            if (commissionHead == null)
            {
                this.context.ComissionHeads.Add(new CommissionHead
                {
                    Comission = commission,
                    Head = user
                });
            }

            else
            {
                commissionHead.Head = user;
            }

            return await this.context.SaveChangesAsync();

        }

        public async Task<int> RemoveCommissionHead(string name, Commission commission)
        {
            CommissionHead commissionHead = this.context.ComissionHeads.Where(c => c.Comission == commission).FirstOrDefault();

            if (commissionHead != null)
            {
                this.context.ComissionHeads.Remove(commissionHead);
            }

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> SetChairHead(string name, Chair chair)
        {
            UserInfo user = this.context.UserInfos.Where(u => u.SecondName + " " + u.FirstName == name).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Такого користувача не існує");
            }

            ChairHead chairHead = this.context.ChairHeads.Where(c => c.Chair == chair).FirstOrDefault();

            if (chairHead == null)
            {
                this.context.ChairHeads.Add(new ChairHead
                {
                    Chair = chair,
                    Head = user
                });
            }

            else
            {
                chairHead.Head = user;
            }

            return await this.context.SaveChangesAsync();

        }

        public async Task<int> RemoveChairHead(string name, Chair chair)
        {
            ChairHead chairHead = this.context.ChairHeads.Where(c => c.Chair == chair).FirstOrDefault();

            if (chairHead != null)
            {
                this.context.ChairHeads.Remove(chairHead);
            }

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteAllUsersFromChairAsync(Chair chair)
        {
            await this.context.UserInfos.Where(u => u.Chair == chair)
                .ForEachAsync(u => 
                {
                    u.Chair = this.context.Chairs.Where(ch => ch.ChairId == 1).FirstOrDefault();
                });

            return await this.context.SaveChangesAsync();
        }

        public async Task<UserInfo> GetUserByNameAndSurnameAsync(string name)
        {
            return await this.context.UserInfos
                .Where(u => u.SecondName + " " + u.FirstName == name).FirstOrDefaultAsync();
        }
    }
}
