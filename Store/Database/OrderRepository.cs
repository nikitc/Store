using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        private StoreContext _context { get; }
        public OrderRepository(StoreContext context) : base(context.Orders)
        {
            _context = context;
        }

        public new IQueryable<Order> GetAll()
        {
            return _context.Orders
                .Include(x => x.PaymentInfo)
                .Include(x => x.Product2Orders)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Image);
        }
    }
}
