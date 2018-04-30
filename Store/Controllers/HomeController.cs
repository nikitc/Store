using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IDataManager dataManager)
        {
            var products = dataManager.ProductRepository.GetAll();   
            var model = new MainPageModel();
            model.Fill(products);

            return View(model);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
