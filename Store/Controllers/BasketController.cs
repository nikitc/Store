using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Services;

namespace Store.Controllers
{
    public class BasketController : Controller
    {
        private readonly IDataManager _dataManager;

        public BasketController(IDataManager datamanager)
        {
            _dataManager = datamanager;
        }

        [HttpPost]
        public IActionResult AddProduct(int productId)
        {
            var user = _dataManager.UserRepository.GetByName(User.Identity.Name);
            var product = _dataManager.ProductRepository.GetById(productId);

            if (user == null || product == null)
            {
                return BadRequest();
            }

            if (user.Orders == null)
            {
                var order = new Order
                {
                    StateId = 1,
                    UserId = user.Id,
                    User = user
                };
                
                _dataManager.Product2OrderRepository.Add(new Product2Order { Product = product, Order = order });
                _dataManager.OrderRepository.Create(order);
            }
            else
            {
                var basket = user.Orders.FirstOrDefault(b => b.StateId == 1);

                _dataManager.Product2OrderRepository.Add(new Product2Order { Product = product, Order = basket });
            }
            _dataManager.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
