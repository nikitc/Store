using Microsoft.AspNetCore.Mvc;
using Store.Security;

namespace Store.Controllers
{
    public abstract class BaseStoreController : Controller
    {
        private UserPrincipal _userPrincipal;
        protected UserPrincipal UserPrincipal
        {
            get => _userPrincipal ?? (_userPrincipal = UserPrincipal.Current);
            set => _userPrincipal = value;
        }
    }
}
