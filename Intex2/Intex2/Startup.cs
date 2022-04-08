using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Intex2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Intex2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.ML.OnnxRuntime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Intex2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDbContext<CrashesDbContext>(options =>
            {
                options.UseMySql(Environment.GetEnvironmentVariable("DBConnection"));
            });
            services.AddScoped<ICrashesRepository, EFCrashesRepository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
            services.AddSingleton<InferenceSession>(
             new InferenceSession("wwwroot/Model/crash_severity.onnx")
            );
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 5;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' cdn.datatables.net 10az.online.tableau.com cdn.jsdelivr.net udps.numetric.net fonts.googleapis.com fonts.gstatic.com unsplash.com images.unsplash.com;");
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' 10az.online.tableau.com udps.numetric.net fonts.googleapis.com fonts.gstatic.com; connect-src 'self' 10az.online.tableau.com; style-src 'self' 'unsafe-inline' 10az.online.tableau.com; style-src-elem * 'unsafe-inline'; script-src 'self' 'unsafe-inline' 10az.online.tableau.com maps.googleapis.com; script-src-elem * 'unsafe-inline'; connect-src https://maps.googleapis.com/; frame-src 'self' 10az.online.tableau.com udps.numetric.net public.tableau.com; img-src 'self' 10az.online.tableau.com www.csla-aapc.ca https://*.googleapis.com gstatic.com *.google.com  *.googleusercontent.com public.tableau.com data:");
                await next();
            });
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "paginationFilter",
                pattern: "Filter / Page{ pageNum}/ City{ city}/ County{ county}/ Severity{ crashSeverity}",
                defaults: new { Controller = "Home", action = "ViewCrashes", pageNum = 1, city = "", county = "", crashSeverity = "" });
            endpoints.MapControllerRoute(
                    name: "pagination",
                    pattern: "Crashes/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "ViewCrashes", pageNum = 1 });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
