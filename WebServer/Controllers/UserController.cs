using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    }
}
