using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System.Web;

namespace WebServer.Controllers
{
    public class MainController : BaseController
    {
        private readonly IUserServise _userService;
        private readonly ICommissionServise _commissionService;
        public MainController(IUserServise userService, ICommissionServise commissionService)
            : base(userService)
        {
            _userService = userService;
            _commissionService = commissionService; 
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.User = this.FullUser;
                return View();
            }
            catch {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> Users()
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User && !user.AccessLevel.Chair && !user.AccessLevel.Comission && !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }

                ViewBag.User = this.FullUser;
                ViewBag.UserTable = await _userService.GetUsers(user);
                return View();
            }
            catch {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> ChangeUser(int id)
        {
            try
            {
                UserFullModel user = this.FullUser;

                if(!user.User.AccessLevel.User && !user.User.AccessLevel.Chair && !user.User.AccessLevel.Departament && !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                UserChangeResponseModel model = await _userService.GetDataToChanheUserPage(id);

                ViewBag.User = user;

                ViewBag.Model = model;

                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public IActionResult Chairs()
        {

            if (Request.Cookies["chair_priv"] != "true")
                return new UnauthorizedResult();
            else
                return View();
        }

        public async Task<IActionResult> Commissions()
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User || !user.AccessLevel.Chair || !user.AccessLevel.Comission || !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }

                ViewBag.User = this.FullUser;
                ViewBag.CommissionsTable = await _commissionService.GetCommissionTableAsync();
                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> ChangeCommission(int id)
        {
            try
            {
                UserFullModel user = this.FullUser;

                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                CommissionChangeResponseModel model = await _commissionService.GetDataToChangeCommisionPageAsync(id);

                ViewBag.User = user;

                ViewBag.Model = model;

                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public IActionResult Faculties()
        {
            if (Request.Cookies["fac_priv"] != "true")
                return new UnauthorizedResult();
            else
                return View();
        }

        public async Task<IActionResult> Exit(int id)
        {
            await _userService.SignOutAsync(id, this.Ip);
            return Redirect("../");
        }
    }
}
