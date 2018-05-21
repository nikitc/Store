using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class UserRepository : CommonRepository<User>, IUserRepository
    {
        private StoreContext _context { get; }
        public UserRepository(StoreContext context) : base (context.Users)
        {
            _context = context;
        }
        
        public new User GetById(int id)
        {
            return _context.Users
                          .Include(x => x.Orders)
                          .ThenInclude(x => x.Product2Orders)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Image)
                          .Include(x => x.Orders)
                          .ThenInclude(x => x.PaymentInfo)
                          .FirstOrDefault(x => x.Id == id);
        }
    }
}
