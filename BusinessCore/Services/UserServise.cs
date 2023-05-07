using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

namespace BusinessCore.Services
{
    public class UserServise : IUserServise
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IServiseRepository _serviseRepository;
        private readonly ICommissionRepository _commissionRepository;
        private readonly IChairRepository _chairRepository;

        public UserServise(IUserRepository user, ILogRepository logRepository, IDepartmentRepository department, IServiseRepository servise, ICommissionRepository commission, IChairRepository chair)
        {
            _userRepository = user;
            _logRepository = logRepository;
            _departmentRepository = department;
            _serviseRepository = servise;
            _commissionRepository = commission;
            _chairRepository = chair;
        }

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

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] enteredBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < enteredBytes.Length; i++)
                {
                    builder.Append(enteredBytes[i].ToString("x2"));
                }
                string enteredPasswordHash = builder.ToString();

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                return comparer.Compare(storedPasswordHash, enteredPasswordHash) == 0;
            }
        }

        private void CheckUserModel<IUserModel>(IUserModel user)
            where IUserModel : IUserValidation
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Regex phoneRegex = new Regex(@"^\+380\d{9}$");
            Regex nameRegex = new Regex("^[a-zA-Zа-яА-ЯіІїЇєЄ'-]+$");

            if (!emailRegex.IsMatch(user.Email))
            {
                throw new Exception("Невірний формат електронної пошти");
            }
            else if (!phoneRegex.IsMatch(user.Phone))
            {
                throw new Exception("Невірний формат номеру телефона");
            }
            else if (!nameRegex.IsMatch(user.FirstName) || !nameRegex.IsMatch(user.SecondName) && !nameRegex.IsMatch(user.MiddleName))
            {
                throw new Exception("Невірний формат імені, призвища чи по-батькові");
            }
        }

        public async Task<UserInfo> SignInAsync(string username, string password, string ip)
        {
            string storedPassword = await _userRepository.GetUserPasswordHashAsync(username);

            UserInfo user = _userRepository.GetUserInfoByLogin(username);
            if (_logRepository.CheckIsIpInBan(ip))
            {
                throw new Exception("Спробуйте пізніше");
            }
            else if (await _logRepository.GetUsersEntriesAmmountAsync(user, ip) >= 5)
            {
                await _logRepository.SetToBanAsync(ip);
                throw new Exception("Спробуйте пізніше");
            }

            if (storedPassword == null || password == null)
            {
                await _logRepository.LogEntryAsync(ip, 0, _userRepository.GetUserInfoByLogin(username));
                throw new Exception("Неправильний логін або пароль");
            }
            if (VerifyPassword(password, storedPassword))
            {
                await _logRepository.LogEntryAsync(ip, 1, user);

                return user;
            }
            else
            {
                await _logRepository.LogEntryAsync(ip, 0, _userRepository.GetUserInfoByLogin(username));
                throw new Exception("Неправильний логін або пароль");
            }
        }

        public UserFullModel GetFullUserInfo(UserInfo userInfo)
        {
            return new UserFullModel
            {
                User = _userRepository.GetFullUserInfo(userInfo.IdUserInfo),
                Department = _departmentRepository.GetDepartmentByChairAndComission(userInfo.Chair, userInfo.Comission)
            };
        }

        public async Task<int> UpdateUserBasicAsync(UserBasicUpdateRequestModel model, string ip)
        {
            try
            {
                CheckUserModel(model);
                await _logRepository.LogDataAsync(_userRepository.GetFullUserInfo(model.Id), "altered", model.Id.ToString(), "user_infos", ip, 1);
                return await _userRepository.UpdateUserBasicInfo(model.Id, model.FirstName, model.SecondName, model.MiddleName, model.Phone, model.Email);
            }
            catch
            {
                await _logRepository.LogDataAsync(_userRepository.GetFullUserInfo(model.Id), "altered", model.Id.ToString(), "user_infos", ip, 0);
                throw;
            }
        }

        public async Task<int> SignOutAsync(int id, string ip)
        {
            return await _logRepository.LogDataAsync(_userRepository.GetFullUserInfo(id), "unlogged", null, null, ip, 1);
        }

        public async Task<List<UserTableModel>> GetUsers(UserInfo user)
        {
            List<UserTableModel> list = new List<UserTableModel>();
            if (user.AccessLevel.User)
            {
                foreach (UserInfo item in await _userRepository.GetUsersAsync())
                {
                    UserTableModel model = new UserTableModel();
                    model.ToModel(user, _departmentRepository.GetDepartmentByChairAndComission(user.Chair, user.Comission)?.Abbreviatoin);
                    list.Add(model);
                }
                return list;
            }

            if (user.AccessLevel.Departament)
            {
                foreach (UserInfo item in await _userRepository.GetUsersByDepartmentAsync(user))
                {
                    UserTableModel model = new UserTableModel();
                    model.ToModel(user, _departmentRepository.GetDepartmentByChairAndComission(user.Chair, user.Comission)?.Abbreviatoin);
                    list.Add(model);
                }
            }

            if (user.AccessLevel.Chair)
            {
                foreach (UserInfo item in await _userRepository.GetUsersByChairAsync(user))
                {
                    UserTableModel model = new UserTableModel();
                    model.ToModel(user, _departmentRepository.GetDepartmentByChairAndComission(user.Chair, user.Comission)?.Abbreviatoin);
                    list.Add(model);
                }
            }

            if(user.AccessLevel.Comission)
            {
                foreach (UserInfo item in await _userRepository.GetUsersByComissionAsync(user))
                {
                    UserTableModel model = new UserTableModel();
                    model.ToModel(user, _departmentRepository.GetDepartmentByChairAndComission(user.Chair, user.Comission)?.Abbreviatoin);
                    list.Add(model);
                }
            }

            return list.Distinct().ToList();
        }

        public async Task<UserChangeResponseModel> GetDataToChanheUserPage(int id)
        {
            UserTableModel model = new UserTableModel();
            UserInfo userInfo = await _userRepository.GetUserInfoByIdAsync(id);
            if (userInfo != null)
            {
                model.ToModel(userInfo, _departmentRepository.GetDepartmentByChairAndComission(userInfo.Chair, userInfo.Comission)?.Abbreviatoin);
            }

            return new UserChangeResponseModel
            {
                User = model,
                Ranks = (await _serviseRepository.GetRankNamesAsync()).Where(s => s != model?.Rank).ToList(),
                Commissions = (await _commissionRepository.GetCommissionAbbreviationsAsync()).Where(s => s != model?.Comission).ToList(),
                Chairs = (await _chairRepository.GetChairAbbreviationsAsync()).Where(s => s != model?.Chair).ToList(),
                WorkTypes = (await _serviseRepository.GetWorkTypeNamesAsync()).Where(s => s != model?.WorkType).ToList(),
                AccessLevels = (await _serviseRepository.GetAccessLevelNamesAsync()).Where(s => s != model.AccessLevel).ToList(),
            };
        }
    }
}
