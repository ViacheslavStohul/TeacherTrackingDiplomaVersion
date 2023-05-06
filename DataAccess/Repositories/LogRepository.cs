using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext context;

        public LogRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task LogEntryAsync(string ip, int result, UserInfo user)
        {

            if (user == null)
            {
                this.context.Logs.Add(new Log
                {
                    Ip = ip,
                    Action = "Entry",
                    Result = result,
                    Time = DateTime.Now
                });
            }
            else
            {
                this.context.Logs.Add(new Log
                {
                    Ip = ip,
                    Action = "Entry",
                    Result = result,
                    User = user,
                    Time = DateTime.Now
                });
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<int> GetUsersEntriesAmmount(UserInfo user, string Ip)
        {
            return await this.context.Logs
                .Where(l => l.User == user && l.Action == "Entry" && l.Time.AddHours(1) > DateTime.Now && l.Result == 0).CountAsync();
        }

        public async Task SetToBanAsync(string ip)
        {
            this.context.BanLogs.Add(new BanLog
            {
                Ip = ip,
                BanStarter = DateTime.Now,
                BanEnded = DateTime.Now.AddHours(1),
            });

            await this.context.SaveChangesAsync();
        }

        public bool CheckIsIpInBan(string ip)
        {
            return this.context.BanLogs.Any(l => l.BanEnded.AddHours(1) > DateTime.Now && l.Ip == ip);
        }

        public async Task<int> LogData(UserInfo user, string action, string target, string objectTable, string ip, int result)
        {
            this.context.Logs.Add(new Log
            {
                User = user,
                Action = action,
                Target = target,
                ObjectTable = objectTable,
                Ip = ip,
                Result = result,
                Time = DateTime.Now,
            });

            return await this.context.SaveChangesAsync();
        }
    }
}
