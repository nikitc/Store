using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class AdminController : BaseStoreController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            return View();
        }
    }
}