using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class UserRepository : CommonRepository<User>, IUserRepository
    {
        private StoreContext _context { get; set; }
        public UserRepository(StoreContext context) : base (context.Users)
        {
            _context = context;
        }
        
        public User GetByName(string name)
        {
            return _context.Users.Include(x => x.Orders).FirstOrDefault(x => x.Login == name);
        }
    }
}
