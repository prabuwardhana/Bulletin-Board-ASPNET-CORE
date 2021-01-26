using AutoMapper;
using BulletinBoard.Extensions;
using BulletinBoard.Infrastructures;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

namespace BulletinBoard
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddRazorRuntimeCompilation()
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSignalR();
            services.ConfigureSqliteContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureRepositoryService();
            services.AddAutoMapper(typeof(MappingProfile));
            services.ConfigureIdentityUser();
            services.ConfigureIdentityOptions();
            services.ConfigureCookie();
            services.ConfigureAzureBlobService(Configuration);
            services.ConfigureEmailService(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("pagination", "Forum/Page_{pageNumber}",
                    new { Controller = "Forum", action = "Index" });
                
                endpoints.MapHub<SignalServer>("/signalServer");
            });
        }
    }
}
