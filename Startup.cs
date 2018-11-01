using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialPortal.Data;
using SocialPortal.Models;
using Microsoft.Owin;
using Owin;
using SocialPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Hosting;

[assembly: OwinStartup(typeof(SocialPortal.Startup))]
namespace SocialPortal
{
    public class Startup
    {
        public static int ID ;
        public static string What = String.Empty;
        public static string Who = String.Empty;
        public static string Type = String.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            //services.AddSignalRCore();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.CookieName = ".MyApplication";
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<QueryValueService>();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/event-stream" });
            });

            services.AddServerSentEvents();
           
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
           
            

            var hubConfiguration = new Microsoft.AspNet.SignalR.HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;

          

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //RolesData.SeedRoles(app.ApplicationServices).Wait();
        }

        //public static class RolesData
        //{
        //    private static readonly string[] Roles = new string[] { "Admin", "Recenzent", "User" };

        //    public static async Task SeedRoles(IServiceProvider serviceProvider)
        //    {
        //        using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //        {
        //            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


        //                await dbContext.Database.MigrateAsync();

        //                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityRole>>();

        //            foreach (var role in Roles)
        //                {
        //                    if (!await roleManager.RoleExistsAsync(role))
        //                    {
        //                        await roleManager.CreateAsync(new IdentityRole(role));
        //                    }
        //                }

        //            //var user2 = dbContext.Users.SingleOrDefault(x => x.Email == "allegro6@gazeta.pl");

        //            ////var role = db.Roles.SingleOrDefault(m => m.Name == "admin");
        //            //var user = await userManager.FindByIdAsync(user2.Id);
        //            //await userManager.AddToRoleAsync(user, "Admin");

        //        }
//    }
//}
    }
}
