using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace WebServer.Controllers
{
    public class MainController : BaseController
    {
        private readonly IUserServise _userService;
        private readonly ILogRepository _logRepository;
        public MainController(IUserServise userService)
            : base(userService)
        {
            _userService = userService;
        }
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

        public IActionResult Exit(int id)
        {
            throw new NotImplementedException();
            return Redirect("../");
        }
    }
}
