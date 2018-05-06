using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Services;

namespace Store.Controllers
{
    public class BasketController : BaseStoreController
    {
        private readonly IDataManager _dataManager;

        public BasketController(IDataManager datamanager)
        {
            _dataManager = datamanager;
        }

        [HttpPost]
        public IActionResult AddProduct(int productId)
        {
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
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
                _dataManager.Product2OrderRepository.Add(
                    new Product2Order
                    {
                        Product = product,
                        Order = order,
                        ProductCount = 1
                    });
                _dataManager.OrderRepository.Create(order);
            }
            else
            {
                var basket = user.Orders.FirstOrDefault(b => b.StateId == 1);
                var product2Order = _dataManager.Product2OrderRepository.GetAll()
                    .FirstOrDefault(p => p.OrderId == basket.Id && p.ProductId == productId);

                if (product2Order != null)
                {
                    product2Order.ProductCount++;
                }
                else
                {
                    _dataManager.Product2OrderRepository.Add(
                        new Product2Order
                        {
                            Product = product,
                            Order = basket,
                            ProductCount = 1
                        });
                }
            }
            _dataManager.SaveChanges();

            return Json(new { Success = true });
        }
    }
}
