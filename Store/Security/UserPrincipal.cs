using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Security
{
    public class UserPrincipal
    {
        [ThreadStatic]
        private static UserPrincipal _current;
        public static UserPrincipal Current
        {
            get
            {
                if (_current != null)
                    return _current;

                var httpContext = HttpContext.Current;
                if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
                    return null;
                
                return httpContext.RequestServices
                    .GetRequiredService<IMemoryCache>()
                    .Get<UserPrincipal>(httpContext.User.Identity.Name);
            }
        }

        public int UserId { get; set; }

        public UserPrincipal()
        {
            Metadata = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsAdmin { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}
