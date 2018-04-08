using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Store.Services;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
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
