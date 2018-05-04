using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Services;

namespace Store.Security
{
    public class PermissionMapFilter : AuthorizeFilter
    {
        public PermissionMapFilter() : base(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())
        {
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var dataManager = httpContext.RequestServices.GetRequiredService<IDataManager>();
            var user = new CurrentUserService().GetPrincipal(httpContext, dataManager);
               
            return Task.FromResult(0);
        }
    }
}
