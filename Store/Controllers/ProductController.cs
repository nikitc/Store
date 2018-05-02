using System;
using System.Collections.Generic;
using System.IO;
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
            var names = name
                .Split()
                .Select(x => x.ToLower())
                .ToArray();
            var data = _dataManager.ProductRepository.GetAll()
                .Where(x => names.All(x.Name.Contains))
                .ToList();

            return Json(new { Data = data });
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

            var product = new Product();
            model.SetProduct(product, _dataManager);

            _dataManager.ProductRepository.Create(product);
            _dataManager.SaveChanges();

            return Json(new { Created = true });
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            //TODO Для админа
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
            //TODO Для админа
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
            //TODO Для админа
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