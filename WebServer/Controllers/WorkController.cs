using BusinessCore.Models;
using BusinessCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("Work")]
    public class WorkController : BaseController
    {
        private readonly IUserServise _userServise;
        private readonly IWorkServise _workServise;

        public WorkController(IUserServise userServise, IWorkServise workServise)
            : base(userServise)
        {
            _workServise = workServise;
        }

        [Route("UpdateWork")]
        [HttpPost]
        public async Task<IActionResult> UpdateWorkAsync([FromBody] WorkTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Дані заповнені неправильно");
                }

                UserFullModel user = this.FullUser;

                if (user.User.IdUserInfo != model.User)
                {
                    return new UnauthorizedObjectResult("Ви не маєте право на цю дію");
                }

                return this.Ok(await _workServise.UpdateWorkAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("AddWork")]
        [HttpPost]
        public async Task<IActionResult> AddWorkAsync([FromBody] WorkTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Дані заповнені неправильно");
                }

                UserFullModel user = this.FullUser;

                if (user.User.IdUserInfo != model.User)
                {
                    return new UnauthorizedObjectResult("Ви не маєте право на цю дію");
                }

                return this.Ok(await _workServise.AddWorkAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("DeleteWork")]
        [HttpGet]
        public async Task<IActionResult> DeleteWorkAsync(int userId, int id)
        {
            try
            {
                UserFullModel user = this.FullUser;

                if (user.User.IdUserInfo != userId)
                {
                    return new UnauthorizedObjectResult("Ви не маєте право на цю дію");
                }

                return this.Ok(await _workServise.DeleteWorkAsync(id, userId, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
