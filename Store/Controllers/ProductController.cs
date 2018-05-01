using System.Collections.Generic;
using System.Linq;
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
            var data = new List<string> { "test1", "tes2", "tes2", "tessst2" };
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
                return BadRequest();

            var model = new ProductModel();
            model.SetModel(product);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetProductsByBrand(int id)
        {
            var brand = _dataManager.BrandRepository.GetById(id);
            if (brand == null)
                return BadRequest();

            var products = _dataManager.ProductRepository.GetAll()
                .Where(x => x.BrandId == id)
                .ToList();

            return null;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            //TODO Для админа
            var model = new ProductModel();
            model.Fill(_dataManager);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            //TODO Для админа
            if (!ModelState.IsValid)
            {
                model.Fill(_dataManager);
                return View(model);
            }

            return Json(new { Created = true });
        }
    }
}