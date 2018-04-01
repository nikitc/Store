using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base (context.Products)
        {
        }
    }
}
