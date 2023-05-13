using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using System.Web;

namespace WebServer.Controllers
{
    public class MainController : BaseController
    {
        private readonly IUserServise _userService;
        private readonly ICommissionServise _commissionService;
        private readonly IChairServise _chairServise;
        private readonly IDepartmentServise _departmentServise;
        private readonly IWorkServise _workServise;
        public MainController(IUserServise userService, ICommissionServise commissionService, IChairServise chairServise, IDepartmentServise departmentServise, IWorkServise work)
            : base(userService)
        {
            _userService = userService;
            _commissionService = commissionService;
            _chairServise = chairServise;
            _departmentServise = departmentServise;
            _workServise = work;
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

        public async Task<IActionResult> Chairs()
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User || !user.AccessLevel.Chair || !user.AccessLevel.Comission || !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }

                ViewBag.User = this.FullUser;
                ViewBag.ChairTable = await _chairServise.GetChairTableAsync();
                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> ChangeChair(int id)
        {
            try
            {
                UserFullModel user = this.FullUser;

                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                ChairChangeResponseModel model = await _chairServise.GetDataToChangeCommisionPageAsync(id);

                ViewBag.User = user;

                ViewBag.Model = model;

                return View();
            }
            catch
            {
                return Redirect("../");
            }
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

        public async Task<IActionResult> Departments()
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User || !user.AccessLevel.Chair || !user.AccessLevel.Comission || !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }

                ViewBag.User = this.FullUser;
                ViewBag.DepartmentTable = await _departmentServise.GetDapartmentTableAsync();
                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> ChangeDepartment(int id)
        {
            try
            {
                UserFullModel user = this.FullUser;

                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                DepartmentChangeResponseModel model = await _departmentServise.GetDataToChangeDepartmentPageAsync(id);

                ViewBag.User = user;

                ViewBag.Model = model;

                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }

        public async Task<IActionResult> Works(int id)
        {
            try
            {
                UserInfo user = this.FullUser.User;

                if (!user.AccessLevel.User || !user.AccessLevel.Chair || !user.AccessLevel.Comission || !user.AccessLevel.Departament)
                {
                    return new UnauthorizedResult();
                }

                ViewBag.User = this.FullUser;
                ViewBag.WorkTable = await _workServise.GetWorkTableAsync(id);
                return View();
            }
            catch
            {
                return Redirect("../");
            }
        }



        public async Task<IActionResult> Exit(int id)
        {
            await _userService.SignOutAsync(id, this.Ip);
            return Redirect("../");
        }
    }
}
