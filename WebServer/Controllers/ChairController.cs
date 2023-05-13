using BusinessCore.Models;
using BusinessCore.Services;
using BusinessCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("Chair")]
    public class ChairController : BaseController
    {
        private readonly IUserServise _userServise;
        private readonly IChairServise _chairServise;

        public ChairController(IUserServise userServise, IChairServise chairServise)
            :base(userServise)
        {
            _chairServise = chairServise;
        }

        [Route("DeleteChair")]
        [HttpGet]
        public async Task<IActionResult> DeleteChairAsync(int id)
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

                return this.Ok(await _chairServise.DeleteChairAsync(id, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("UpdateChair")]
        [HttpPost]
        public async Task<IActionResult> UpdateChairAsync([FromBody] ChairTableModel model)
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

                return this.Ok(await _chairServise.UpdateChairAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("AddChair")]
        [HttpPost]
        public async Task<IActionResult> AddChairAsync([FromBody] ChairTableModel model)
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

                return this.Ok(await _chairServise.AddChairAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
