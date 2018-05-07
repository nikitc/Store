using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class UserTokenRepository : CommonRepository<UserToken>, IUserTokenRepository
    {
        private StoreContext _context { get; set; }
        
        public UserTokenRepository(StoreContext context) : base(context.UserTokens)
        {
            _context = context;
        }

        public new IQueryable<UserToken> GetAll()
        {
            return _context.UserTokens.Include(x => x.User);
        }
    }
}