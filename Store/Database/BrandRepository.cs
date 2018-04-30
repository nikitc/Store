using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class BrandRepository : CommonRepository<Brand>, IBrandRepository
    {
        public BrandRepository(StoreContext context) : base(context.Brands)
        {
        }
    }
}
