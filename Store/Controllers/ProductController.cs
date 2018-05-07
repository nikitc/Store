using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class ProductController : BaseStoreController
    {
        private readonly IDataManager _dataManager;

        public ProductController(IDataManager datamanager)
        {
            _dataManager = datamanager;
        }

        public ActionResult SearchProductName(string name)
        {
            var names = name
                .Split()
                .Select(x => x.ToLower())
                .ToArray();

            var result = new List<Product>();
            foreach (var word in names)
            {
                result.AddRange(_dataManager.ProductRepository.GetAll()
                    .Where(x => x.Name.ToLower().StartsWith(word))
                    .ToList());
            }

            return Json(new { Data = result.Distinct() });
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

            var products = brand.Products
                .Where(x => x.BrandId == id)
                .Select(x => new ProductModel
                {
                    Id = x.Id,
                    ImageData = x.Image,
                    Name = x.Name,
                    Price = x.Price,
                    OldPrice = x.OldPrice,
                    Brand = x.Brand.Name
                })
                .ToList();
            ViewData["Brand"] = brand.Name;
            return View(products);
        }

        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _dataManager.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
            var model = new ProductModel();
            model.SetModel(product);

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            var model = new ProductModel();
            model.Fill(_dataManager);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                model.Fill(_dataManager);
                return View(model);
            }

            var product = new Product();
            model.SetProduct(product, _dataManager);

            _dataManager.ProductRepository.Create(product);
            _dataManager.SaveChanges();

            return Json(new { Created = true });
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            if (!UserPrincipal.IsAdmin)
                BadRequest();

            var product = _dataManager.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();

            _dataManager.ProductRepository.Delete(product);
            _dataManager.SaveChanges();

            return Json(new { Deleted = true });
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            if (!UserPrincipal.IsAdmin)
                BadRequest();

            var product = _dataManager.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();

            var model = new ProductModel();
            model.SetModel(product);
            model.Fill(_dataManager);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            if (!UserPrincipal.IsAdmin)
                BadRequest();

            if (!ModelState.IsValid)
            {
                model.Fill(_dataManager);
                return View(model);
            }

            var product = _dataManager.ProductRepository.GetById(model.Id);
            model.SetProduct(product, _dataManager);
            _dataManager.ProductRepository.Update(product);
            _dataManager.SaveChanges();

            return RedirectToAction("GetProduct", new { id = product.Id });
        }
    }
}