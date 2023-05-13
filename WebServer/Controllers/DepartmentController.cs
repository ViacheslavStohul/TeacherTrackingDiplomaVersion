using BusinessCore.Models;
using BusinessCore.Services;
using BusinessCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("Department")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentServise _departmentServise;
        private readonly IUserServise _userServise;
        public DepartmentController(IUserServise userServise, IDepartmentServise departmentServise)
            :base(userServise)
        {
            _departmentServise = departmentServise;
        }

        [Route("DeleteDepartment")]
        [HttpGet]
        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            try
            {
                UserFullModel user = this.FullUser;
                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _departmentServise.DeleteDepartmentAsync(id, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("UpdateDepartment")]
        [HttpPost]
        public async Task<IActionResult> UpdateDepartmentAsync([FromBody] DepartmentTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Помилка зберігання. Невірно заповненні дані");
                }

                UserFullModel user = this.FullUser;
                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _departmentServise.UpdateDepartmentAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("AddDepartment")]
        [HttpPost]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] DepartmentTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Помилка зберігання. Невірно заповненні дані");
                }

                UserFullModel user = this.FullUser;
                if (!user.User.AccessLevel.User || !user.User.AccessLevel.Chair || !user.User.AccessLevel.Departament || !user.User.AccessLevel.Comission)
                {
                    return new UnauthorizedResult();
                }

                return this.Ok(await _departmentServise.CreateDepartmentAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
