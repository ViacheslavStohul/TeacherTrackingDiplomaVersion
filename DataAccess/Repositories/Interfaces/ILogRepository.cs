using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task LogEntryAsync(string ip, int result, UserInfo user);

        Task<int> GetUsersEntriesAmmount(UserInfo user, string Ip);

        Task SetToBanAsync(string ip);

        bool CheckIsIpInBan(string ip);
    }
}
