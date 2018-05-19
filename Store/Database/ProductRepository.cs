using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context) : base (context.Products)
        {
            _context = context;
        }

        public new Product GetById(int id)
        {
            return _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Image)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
