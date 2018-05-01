using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    public class BrandController : Controller
    {
        private readonly IDataManager _dataManager;

        public BrandController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult AddBrand()
        {
            var brandModel = new BrandModel();
            return View(brandModel);
        }

        [HttpPost]
        public IActionResult AddBrand(BrandModel model)
        {
            string content;
            using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
            {
                var imageData = binaryReader.ReadBytes((int)model.Image.Length);
                content = Convert.ToBase64String(imageData);
            }

            var image = new Image
            {
                Name = model.Image.FileName,
                Content = content
            };
            var brand = new Brand
            {
                Name = model.Name,
                Image = image
            };
            _dataManager.BrandRepository.Create(brand);
            _dataManager.SaveChanges();
            return RedirectToAction("AddBrand", "Brand");
        }
    }
}