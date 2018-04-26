﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
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

        [Route("api/products/SearchProductName={name}")]
        public IActionResult SearchProductName(string name)
        {
            //Заглушка, на самом деле берем товары из базы
            var data = new List<Product> {new Product {Name = "test1"} };
            return Json (
                new
                {
                    IsSuccess = true,
                    Data = data
                });
        }

        [HttpGet]
        public IActionResult DisplayProduct(int id)
        {
            //Заглушка, на самом деле берем товары из базы
            var data = new List<Product> { new Product { Name = "test1" } };
            return Json(
                new
                {
                    IsSuccess = true,
                    Data = data
                });
        }
    }
}