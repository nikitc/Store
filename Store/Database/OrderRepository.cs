using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext context) : base(context.Orders)
        {
        }
    }
}
