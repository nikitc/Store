using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Models.Account;
using Store.Services;

namespace Store.Controllers
{
    public class AjaxValidatorController : Controller
    {
        private readonly IDataManager _dataManager;

        public AjaxValidatorController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost]
        public async Task<ActionResult> CheckLoginModel(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = AccountController.GetSha256Hash(model.Password);
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user != null && user.PasswordHash == passwordHash)
                    return Json(new { IsSuccess = true });
            }
            return Json(new { IsSuccess = false });
        }

        [HttpPost]
        public async Task<ActionResult> CheckRegisterModel(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _dataManager.UserRepository.GetAll().FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                    return Json(new { IsSuccess = true });
            }
            return Json(new { IsSuccess = false });
        }
    }
}
