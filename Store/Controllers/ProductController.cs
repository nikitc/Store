using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDataManager _dataManager;

        public ProductController(IDataManager datamanager)
        {
            _dataManager = datamanager;
        }

        public ActionResult SearchProductName(string name)
        {
            //Заглушка, на самом деле берем товары из базы
            var data = new List<string> { "test1", "tes2", "tes2", "tessst2"};
            return Json(
                new
                {
                    IsSuccess = true,
                    Data = data
                });
        }

        [HttpGet]
        public IActionResult DisplayProduct(int id)
        {
            var product = _dataManager.ProductRepository.GetById(id);
            if (product == null)
            {
                return BadRequest();
            }

            var model = new ProductModel();
            model.SetModel(product);
            return View(model);
        }

        [HttpGet]
        public string Add()
        {
            return "123";
        }
    }
}