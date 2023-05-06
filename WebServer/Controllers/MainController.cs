using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System.Web;

namespace WebServer.Controllers
{
    [Route("Main")]
    public class MainController : BaseController
    {
        private readonly IUserServise _userService;
        public MainController(IUserServise userService)
            : base(userService)
        {
            _userService = userService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.User = this.FullUser;
            }
            catch {}
            return View();
        }

        public IActionResult Users()
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User && !user.AccessLevel.Chair && !user.AccessLevel.Comission && !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }
            }
            catch { }
            return View();
        }

        public IActionResult Chairs()
        {

            if (Request.Cookies["chair_priv"] != "true")
                return new UnauthorizedResult();
            else
                return View();
        }

        public IActionResult Institutes()
        {
            if (Request.Cookies["inst_priv"] != "true")
                return new UnauthorizedResult();
            else
                return View();
        }

        public IActionResult Faculties()
        {
            if (Request.Cookies["fac_priv"] != "true")
                return new UnauthorizedResult();
            else
                return View();
        }

        [Route("Exit")]
        public async Task<IActionResult> ExitAsync(int id)
        {
            await _userService.SignOutAsync(id, this.Ip);
            return Redirect("../");
        }
    }
}
