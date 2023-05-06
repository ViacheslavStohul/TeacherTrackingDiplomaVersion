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

        Task<int> UpdateUserBasic(UserBasicUpdateRequestModel model);
    }
}
