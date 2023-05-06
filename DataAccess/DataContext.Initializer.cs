using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class DataContext
    {
        public async Task<int> Initialize()
        {
            int updatedRowsCount = 0;

            List<CompleteMigration> completeMigrations = await this.CompleteMigrations.ToListAsync();

            using (var dbContextTransaction = await this.BeginTransactionAsync())
            {
                try
                {
                    updatedRowsCount += await InitialMigration(completeMigrations);
                    dbContextTransaction.Commit();
                    return updatedRowsCount;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        #region Migrations
        #endregion

        #region ServiceMethods
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion

        #region InitialMigratoins
        public async Task<int> InitialMigration(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "DF3520C1-4E48-4167-BF61-084C11A80944";
            int updatedRowsCount = 0;

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                updatedRowsCount += await SetAdminAccessLevel();
                updatedRowsCount += await SetAdminUserInfo();
                updatedRowsCount += await SetAdminUser();
                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<int> SetAdminAccessLevel()
        {
            this.AccessLevels.Add(new AccessLevel
            {
                Chair = true,
                Comission = true,
                Departament = true,
                User = true,
                Name = "Адміністратор"
            });
            return await this.SaveChangesAsync();
        }

        private async Task<int> SetAdminUserInfo()
        {
            this.UserInfos.Add(new UserInfo
            {
                FirstName = "Admin",
                SecondName = "Admin",
                MiddleName = "Admin",
                Email = "testAdmin@admin.com",
                Phone = "+380000000000",
                AccessLevel = this.AccessLevels.FirstOrDefault()
            });

            return await this.SaveChangesAsync();
        }

        private async Task<int> SetAdminUser()
        {
            this.Users.Add(new User
            {
                UserInfo = this.UserInfos.FirstOrDefault(),
                Login = "admin",
                Password = this.HashPassword("admin")
            });

            return await this.SaveChangesAsync();
        }
        #endregion
    }
}
