using Guardian.APIGateway.Models;
using Guardian.APIGateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task Invoke(HttpContext httpContext, IResourceService resourceService, IAuthorizationService authorizationService)
        {
            var resourceServiceRequest = new ResourceServiceRequest()
            {
                BasePath = httpContext.Request.Host.Value,
                Path = httpContext.Request.Path.Value,
            };

            var resource = await resourceService.GetResource(resourceServiceRequest);

            if (resource.IsAuthenticationRequired)
            {
                string token = httpContext.Request.Headers["token"];

                bool isValid  = await authorizationService.ValidateToken(token);

                if (!isValid)
                {
                    throw new Exception("Token is not valid");
                }
            }

            await _next(httpContext);
        }
    }
}
