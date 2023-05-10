using BusinessCore.Models;
using BusinessCore.Services;
using BusinessCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [Route("Commission")]
    public class CommissionController : BaseController
    {
        private readonly ICommissionServise _commissionServise;

        public CommissionController(IUserServise _userServise, ICommissionServise commissionServise)
            : base(_userServise)
        {
            _commissionServise = commissionServise;
        }

        [Route("DeleteCommission")]
        public async Task<IActionResult> DeleteCommisionAsync(int id)
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

                return this.Ok(await _commissionServise.DeleteCommissionAsync(id, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("UpdateCommission")]
        public async Task<IActionResult> UpdateCommissionAsync([FromBody] CommissionTableModel model) 
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

                return this.Ok(await _commissionServise.UpdateCommissionAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [Route("AddCommission")]
        public async Task<IActionResult> AddCommission([FromBody] CommissionTableModel model)
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

                return this.Ok(await _commissionServise.CreateCommissionAsync(model, user.User, this.Ip));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
