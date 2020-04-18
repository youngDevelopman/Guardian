using System.Net.Http;
using Guardian.APIGateway.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Guardian.APIGateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IAuthorizationService, AuthorizationService>(client =>
            {
                client.BaseAddress = new System.Uri("https://localhost:5002");
            });
            services.AddHttpClient<IResourceService, ResourceService>(client => 
            {
                client.BaseAddress = new System.Uri("https://localhost:5003");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            
            app.UseMiddleware<GuardianAuthorizationMiddleware>();
            
            Router router = new Router("routes.json");
            app.Run(async (context) =>
            {
                var response = await router.RouteRequest(context.Request);
                var content = await response.Content.ReadAsStringAsync();
                await context.Response.WriteAsync(content);
            });
        }
    }
}
