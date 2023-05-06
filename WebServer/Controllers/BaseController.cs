using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebServer.Controllers
{
    public class BaseController : Controller
    {

        private readonly IUserServise _userService;
        public BaseController(IUserServise userService)
        {
            _userService = userService;
        }

        protected UserFullModel FullUser
        {
            get
            {
                try
                {
                    return this._userService.GetFullUserInfo(JsonConvert.DeserializeObject<UserInfo>(Request.Cookies["user"]));
                }
                catch
                {
                    return null;
                }
            }
        }

        protected string Ip
        {
            get
            {
                return Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
    }
}
