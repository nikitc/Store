using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class Product2OrderRepository : IProduct2OrderRepository
    {
        private StoreContext _context { get; set; }

        public Product2OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(Product2Order entity)
        {
            _context.Product2Orders.Add(entity);
        }
    }
}
