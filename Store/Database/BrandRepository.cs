using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class BrandRepository : CommonRepository<Brand>, IBrandRepository
    {
        private readonly StoreContext _context;
        
        public BrandRepository(StoreContext context) : base(context.Brands)
        {
            _context = context;
        }

        public new Brand GetById(int id)
        {
            return _context.Brands.Include(x => x.Products).ThenInclude(a => a.Image).FirstOrDefault(x => x.Id == id);
        }
    }
}
