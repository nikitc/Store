using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Store.Security
{
    internal class HttpExceptionMiddleware
    {
        private readonly RequestDelegate _reqDelegate;

        public HttpExceptionMiddleware(RequestDelegate reqDelegate)
        {
            _reqDelegate = reqDelegate;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            try
            {
                await _reqDelegate.Invoke(context);
            }
            catch (System.Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
