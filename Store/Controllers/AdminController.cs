using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            var orders = _dataManager.OrderRepository
                .GetAll()
                .Where(x => x.StateId == (int)OrderStates.ProcessState);
            return View();
        }
    }
}