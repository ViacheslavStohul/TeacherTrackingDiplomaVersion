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

        Task<List<UserInfo>> GetUsersAsync();

        Task<List<UserInfo>> GetUsersByChairAsync(UserInfo user);

        Task<List<UserInfo>> GetUsersByDepartmentAsync(UserInfo user);

        Task<List<UserInfo>> GetUsersByComissionAsync(UserInfo user);

        Task<UserInfo> GetUserInfoByIdAsync(int id);

        Task<int> UpdateUserAdminAsync(UserInfo user);

        Task<UserInfo> CreateUserAsync(UserInfo userInfo);

        Task<int> DeleteUserAsync(int id);

        Task<int> RestoreUserAsync(int id, string login, string password);

        Task<int> UpdateUsersCommission(Commission commission);

        Task<int> SetCommissionHead(string name, Commission commission);

        Task<int> RemoveCommissionHead(string name, Commission commission);

        Task<int> SetChairHead(string name, Chair chair);

        Task<int> RemoveChairHead(string name, Chair chair);

        Task<int> DeleteAllUsersFromChairAsync(Chair chair);

        Task<UserInfo> GetUserByNameAndSurnameAsync(string name);

    }
}
