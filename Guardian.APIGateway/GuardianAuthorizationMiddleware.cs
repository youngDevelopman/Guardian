using Guardian.APIGateway.Services;
using Guardian.Shared.Models;
using Microsoft.AspNetCore.Http;
using System;
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
            var resourceServiceRequest = new GetResourceRequest()
            {
                Domain = httpContext.Request.Host.Value,
                RelativePath = httpContext.Request.Path.Value,
            };

            var resource = await resourceService.GetResource(resourceServiceRequest);

            if (resource.IsAuthenticationRequired)
            {
                string token = httpContext.Request.Headers["token"];

                var validationRequest = new TokenValidationRequest()
                {
                    AccessToken = token,
                    UserPoolId = resource.UserPoolId,
                };

                bool isValid  = await authorizationService.ValidateToken(validationRequest);

                if (!isValid)
                {
                    throw new Exception("Token is not valid");
                }
            }

            await _next(httpContext);
        }
    }
}
