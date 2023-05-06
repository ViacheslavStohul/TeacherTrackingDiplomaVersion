using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebServer.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly IUserServise _userService;

        public UserController(ILogger<HomeController> logger, IUserServise userService)
            : base(userService)
        {
            this._userService = userService;
        }

        [Route("LogIn")]
        public async Task<IActionResult> LogInAsync(string login, string password)
        {
            try
            {
                return this.Ok(await this._userService.SignInAsync(login, password, Request.HttpContext.Connection.RemoteIpAddress.ToString()));
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

            model.Id = JsonConvert.DeserializeObject<UserInfo>(Request.Cookies["user"]).IdUserInfo;

            try
            {
                return this.Ok(await _userService.UpdateUserBasic(model));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
            
        }
    }
}
