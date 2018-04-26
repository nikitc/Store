using System.Collections.Generic;
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
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Смартфон HONOR 9 lite Black", Price = 14990},
                new Product {Id = 2, Name = "Nokia", Price = 990}
            };

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
