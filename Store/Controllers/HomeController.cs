using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IDataManager dataManager)
        {
            var brands = dataManager.BrandRepository.GetAll()
                .Take(5)
                .Select(x => new BrandModel
                {
                    Name = x.Name,
                    ImageData = x.Image
                });
            return View(brands);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
