using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<string> GetUserPasswordHashAsync(string login);

        UserInfo GetUserInfoByLogin(string login);

        UserInfo GetFullUserInfo(int id);

        Task<int> UpdateUserBasicInfo(int id, string firstName, string secondName, string middleName, string phone, string email);
    }
}
