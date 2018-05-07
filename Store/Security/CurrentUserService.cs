using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Store.Database.Entities;
using Store.Services;

namespace Store.Security
{
    public class CurrentUserService
    {
        public UserPrincipal GetPrincipal(Microsoft.AspNetCore.Http.HttpContext httpContext, IDataManager dataManager)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            var userId = httpContext.User.Identity.Name;
            var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
            var principal = cache.Get<UserPrincipal>(userId);
            if (principal == null)
            {
                var person = dataManager.UserRepository.GetById(int.Parse(userId));
                if (person == null)
                {
                    return null;
                }
                principal = CreatePrincipal(person);
                cache.Set(userId, principal, new TimeSpan(0, 30, 0));
            }
            return principal;
        }

        private static UserPrincipal CreatePrincipal(User person)
        {
            var principal = new UserPrincipal
            {
                Name = person.Login,
                UserId = person.Id,
                IsAuthenticated = true,
                IsAdmin = person.IsAdmin
            };
            return principal;
        }

        public void DeletePrincipal(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
                var userId = httpContext.User.Identity.Name;
                cache.Remove(userId);
            }
        }

        public static void DeletePrincipal(Microsoft.AspNetCore.Http.HttpContext httpContext, User user)
        {
            var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
            var key = user.Id.ToString();
            cache.Remove(key);
        }

        public static void UpdatePrincipal(Microsoft.AspNetCore.Http.HttpContext httpContext, User user)
        {
            var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
            var key = user.Id.ToString();
            UserPrincipal _;
            if (cache.TryGetValue(key, out _))
                cache.Set(key, CreatePrincipal(user), new TimeSpan(0, 30, 0));
        }
    }
}
