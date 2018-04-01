using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index([FromServices] IDataManager dm)
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
