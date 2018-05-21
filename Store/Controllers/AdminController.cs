using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Basket;
using Store.Models.Order;
using Store.Services;

namespace Store.Controllers
{
    public class AdminController : BaseStoreController
    {
        private readonly IDataManager _dataManager;

        public AdminController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            return View();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            var orders = _dataManager.OrderRepository
                .GetAll()
                .Where(x => x.StateId == (int)OrderStates.ProcessState);
            var payments = new List<PaymentInfoModel>();
            foreach (var order in orders)
            {
                var payment = new PaymentInfoModel(order.Id);
                payment.SetModel(order.PaymentInfo);
                payments.Add(payment);
            }
            return View(payments);
        }

        [HttpGet]
        public IActionResult OrderIsCompleted(int orderId)
        {
            if (!UserPrincipal.IsAdmin)
                return BadRequest();

            var order = _dataManager.OrderRepository.GetById(orderId);
            order.StateId = (int) OrderStates.CompleteState;
            _dataManager.SaveChanges();
            return RedirectToAction("GetOrders");
        }
    }
}