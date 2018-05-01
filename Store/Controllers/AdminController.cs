using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //TODO Проверка на админа
            return View();
        }
    }
}