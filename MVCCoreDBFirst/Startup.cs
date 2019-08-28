using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCCoreDBFirst.Data;
using MVCCoreDBFirst.IoC;

namespace MVCCoreDBFirst
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<ApplicationDBContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>()

                // Add the UserStore and the RoleStore from the ApplicationDBContext
                .AddEntityFrameworkStores<ApplicationDBContext>()

                // Add a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes etc...
                .AddDefaultTokenProviders();

            // Change login URL
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";


                // Change cookie timeout
                options.ExpireTimeSpan = TimeSpan.FromSeconds(15);
            });

           


            // change password poliy
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(50);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //options.User.
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider prov)
        {
            //ServiceProvider prov = (ServiceProvider)services;
            //IoCContainer.provider =     (ServiceProvider)prov;
            //IoCContainer.provider = (ServiceProvider)app.ApplicationServices;

            
            var userStore = prov.GetService<UserManager<ApplicationUser>>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // Setup Identity
            app.UseAuthentication();


            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
