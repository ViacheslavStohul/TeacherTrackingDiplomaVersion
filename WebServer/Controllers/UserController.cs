using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace WebServer.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly IUserServise _userService;
        private readonly ILogRepository _logRepository;

        public UserController(ILogger<HomeController> logger, IUserServise userService, ILogRepository log)
            : base(userService)
        {
            this._userService = userService;
            this._logRepository = log;
        }

        [Route("LogIn")]
        public async Task<IActionResult> LogInAsync(string login, string password)
        {
            try
            {
                return this.Ok(await this._userService.SignInAsync(login, password, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("ChangeUserBasic")]
        [HttpPost]
        public async Task<IActionResult> ChangeUserBasicAsync([FromBody] UserBasicUpdateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest("Помилка при оновленні. Зверніться до адміністратора");
            }

            model.Id = this.FullUser.User.IdUserInfo;

            try
            {
                return this.Ok(await _userService.UpdateUserBasicAsync(model, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
            
        }

        [Route("ChangeUserAdmin")]
        [HttpPost]
        public async Task<IActionResult> ChangeUserAdminAsync([FromBody] UserTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Помилка зберігання. Невірно заповненні дані");
                }

                UserFullModel user = this.FullUser;
                if (!user.User.AccessLevel.User && !user.User.AccessLevel.Chair && !user.User.AccessLevel.Departament && !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _userService.UpdateUserAdmin(user, model, this.Ip));
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Помилка зберігання. Невірно заповненні дані");
                }

                UserFullModel user = this.FullUser;
                if (!user.User.AccessLevel.User)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _userService.CreateUser(user, model, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("DeleteUser")]
        public async Task<IActionResult> DelteUserAsync(int id)
        {
            try
            {
                UserInfo user = this.FullUser.User;
                if (!user.AccessLevel.User)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _userService.DeleteUserAsync(id, this.Ip, user));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("ActivateUser")]
        public async Task<IActionResult> ActivateUserAsync(int id, string login, string password)
        {
            try
            {
                UserInfo user = this.FullUser.User;
                if (!user.AccessLevel.User)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _userService.ActivateUserAsync(id, login, password, this.Ip, user));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
