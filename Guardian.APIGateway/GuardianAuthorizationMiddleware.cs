using Guardian.APIGateway.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway
{
    public class GuardianAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public GuardianAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IResourceService resourceService)
        {
            var resource = await resourceService.GetResources(httpContext.Request.Path.Value);

            if (resource.IsAuthenticationRequired)
            {

            }

            await _next(httpContext);
        }
    }
}
