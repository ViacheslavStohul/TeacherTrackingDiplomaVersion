using BusinessCore.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCore.Services.Interfaces
{
    public interface IUserServise
    {
        Task<UserInfo> SignInAsync(string username, string password, string ip);

        UserFullModel GetFullUserInfo(UserInfo userInfo);

        Task<int> UpdateUserBasicAsync(UserBasicUpdateRequestModel model, string ip);

        Task<int> SignOutAsync(int id, string ip);

        Task<List<UserTableModel>> GetUsers(UserInfo user);

        Task<UserChangeResponseModel> GetDataToChanheUserPage(int id);

        Task<int> UpdateUserAdmin(UserFullModel mainUser, UserTableModel userModel, string ip);

        Task<int> CreateUser(UserFullModel mainUser, UserTableModel userModel, string ip);

        Task<int> DeleteUserAsync(int id, string ip, UserInfo user);

        Task<int> ActivateUserAsync(int id, string login, string password, string ip, UserInfo user);
    }
}
