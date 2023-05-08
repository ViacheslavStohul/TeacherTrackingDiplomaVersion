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
                    updatedRowsCount += await CreateInitialComissions(completeMigrations);
                    updatedRowsCount += await CreateInitialChairs(completeMigrations);
                    updatedRowsCount += await CreateInitalDepartments(completeMigrations);
                    updatedRowsCount += await CreateAccessLevels(completeMigrations);
                    updatedRowsCount += await CreateInitialRanks(completeMigrations);
                    updatedRowsCount += await CreateInitialTypes(completeMigrations);
                    updatedRowsCount += await CreateAdminAccount(completeMigrations);
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
        public async Task<int> CreateAdminAccount(List<CompleteMigration> completeMigrations)
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

        private async Task<int> CreateInitialComissions(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "A2015EA1-EB69-4D91-84B8-32FCB3ACF777";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.Commissions.Add(new Commission
                {
                    Name = "Відсутня",
                    Abbreviation = "-"
                });

                this.Commissions.Add(new Commission
                {
                    Name = "Комісія комп'ютерних технологій та програмної інженерії",
                    Abbreviation = "КТ та ПІ"
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<int> CreateInitialChairs(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "A6583DB2-4C80-401B-A807-F3EE05042F95";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.Chairs.Add(new Chair
                {
                    Name = "Відсутня",
                    Abbreviation = "-",
                });

                this.Chairs.Add(new Chair
                {
                    Name = "Кафедра комп'ютерної інженерії",
                    Abbreviation = "КІ",
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<int> CreateInitalDepartments(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "921540D6-F8ED-4518-A326-470CDDAC1D35";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.Departments.Add(new Department
                {
                    Name = "Відсутнє",
                    Abbreviatoin = "-",
                    Commissions = this.Commissions.Where(c => c.Abbreviation == "-").ToList(),
                    Chair = this.Chairs.Where(ch => ch.Name == "Відсутня").FirstOrDefault()
                });

                this.Departments.Add(new Department
                {
                    Name = "Відділення комп'ютерних систем",
                    Abbreviatoin = "КС",
                    Commissions = this.Commissions.Where(c => c.Abbreviation == "КТ та ПІ").ToList(),
                    Chair = this.Chairs.Where(ch => ch.Name == "Кафедра комп'ютерної інженерії").FirstOrDefault()
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<int> CreateAccessLevels(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "B8D6DBF6-209D-4EB3-AC6D-9F8B9F24D37B";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.AccessLevels.Add(new AccessLevel
                {
                    Name = "Стандартний"
                });

                this.AccessLevels.Add(new AccessLevel
                {
                    Name = "в. Кадрів",
                    User = true
                });

                this.AccessLevels.Add(new AccessLevel
                {
                    Name = "Відділ",
                    Departament = true
                });

                this.AccessLevels.Add(new AccessLevel
                {
                    Name = "Кафедра",
                    Chair = true
                });

                this.AccessLevels.Add(new AccessLevel
                {
                    Name = "Коміссія",
                    Comission = true
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> CreateInitialRanks(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "81416C46-0DC4-4DF1-9B12-19548F1944D7";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.Ranks.Add(new Rank
                {
                    Name = "Не вказано",
                    Abbreviation = "Н/В"
                });

                this.Ranks.Add(new Rank
                {
                    Name = "Викладач другої категорії",
                    Abbreviation = "2 К."
                });

                this.Ranks.Add(new Rank
                {
                    Name = "Викладач першої категорії",
                    Abbreviation = "1 К."
                });

                this.Ranks.Add(new Rank
                {
                    Name = "Викладач вищої категорії",
                    Abbreviation = "В. К."
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

        private async Task<int> CreateInitialTypes(List<CompleteMigration> completeMigrations)
        {
            string migrationId = "75AA89FE-455C-416F-8691-9BC842F92D9D";

            if (!completeMigrations.Any(cm => cm.CompleteMigrationId == migrationId))
            {
                this.WorkTypes.Add(new WorkType
                {
                    Name = "Не вказано",
                    Abbreviation = "Н/В"
                });

                this.WorkTypes.Add(new WorkType
                {
                    Name = "Штатний викладач",
                    Abbreviation = "Ш/В"
                });

                this.WorkTypes.Add(new WorkType
                {
                    Name = "Позаштатний викладач",
                    Abbreviation = "П/В"
                });

                this.CompleteMigrations.Add(new CompleteMigration { CompleteMigrationId = migrationId });

                return await this.SaveChangesAsync();
            }

            return 0;
        }

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
                Chair = this.Chairs.Where(ch => ch.Abbreviation == "-").FirstOrDefault(),
                Commission = this.Commissions.Where(c => c.Abbreviation == "-").FirstOrDefault(),
                WorkType = this.WorkTypes.Where(w => w.Name == "Не вказано").FirstOrDefault(),
                Rank = this.Ranks.Where(w => w.Name == "Не вказано").FirstOrDefault(),
                AccessLevel = this.AccessLevels.Where(a => a.Name == "Адміністратор").FirstOrDefault()
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

        #region InitialMigratoins

        #endregion
    }
}
