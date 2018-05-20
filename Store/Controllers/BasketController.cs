using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Database.Entities;
using Store.Models;
using Store.Models.Basket;
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

            if (user.Orders == null || user.Orders.All(x => x.StateId != 1))
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

            return Json(new { IsSuccess = true });
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            if (!IsAuthorized())
                return Forbid();
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var order = user.Orders.FirstOrDefault(x => x.StateId == 1);
            if (order == null || !order.Product2Orders.Any())
                return View(null);
            var model = new BasketModel();
            model.SetModel(order);
            return View(model);
        }

        [HttpGet]
        public IActionResult SetProductsCount(int productId, int count)
        {
            if (!IsAuthorized())
                return BadRequest();
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var product = _dataManager.ProductRepository.GetById(productId);

            if (user == null || product == null)
            {
                return BadRequest();
            }

            var order = user.Orders.FirstOrDefault(x => x.StateId == 1);
            if (order == null || !order.Product2Orders.Any())
                return BadRequest();

            var product2Order = order.Product2Orders.FirstOrDefault(x => x.ProductId == productId);
            if (product2Order == null)
                return BadRequest();
            product2Order.ProductCount = count;
            _dataManager.SaveChanges();

            var fullPrice = order.Product2Orders.Sum(x => x.ProductCount * x.Product.Price);

            return Json(new { IsSuccess = true, FullPrice = fullPrice });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (!IsAuthorized())
                return Forbid();
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var order = user.Orders.FirstOrDefault(x => x.StateId == 1);
            if (order == null)
                return BadRequest();
            var model = new PaymentInfoModel(order.Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(PaymentInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var order = user.Orders.First(x => x.StateId == 1);
            order.StateId = 2;
            _dataManager.SaveChanges();
            return RedirectToAction("PaymentCompleted");
        }

        [HttpGet]
        public IActionResult PaymentCompleted()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RemoveProduct(int productId)
        {
            if (!IsAuthorized())
                return Forbid();
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var order = user.Orders.FirstOrDefault(x => x.StateId == 1);
            if (order == null)
                return BadRequest();
            var product2Order = order.Product2Orders.FirstOrDefault(x => x.ProductId == productId);
            if (product2Order == null)
                return BadRequest();
            order.Product2Orders.Remove(product2Order);
            _dataManager.SaveChanges();

            return RedirectToAction("GetProducts");
        }

        [HttpGet]
        public IActionResult GetCurrentCost()
        {
            if (!IsAuthorized())
                return Json(new { Cost = 0 });
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            var cost = 0;
            var order = user.Orders.FirstOrDefault(x => x.StateId == 1);
            if (order == null)
                return Json(new { Cost = cost });
            cost = (int)order.Product2Orders.Sum(product => product.Product.Price * product.ProductCount);
            return Json(new { Cost = cost });
        }

        private bool IsAuthorized()
        {
            if (UserPrincipal == null)
                return false;
            var user = _dataManager.UserRepository.GetById(UserPrincipal.UserId);
            return user != null;
        }
    }
}
