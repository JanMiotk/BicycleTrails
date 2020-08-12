using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ninject;
using Application.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Storage;
using Models;
using Application.Policy.Models;
using Serializer.Interfaces;
using Storage.Interfaces;
using DirectoriesCreator.Interfaces;
using Application.Policy.Handlers;
using Application.Infrastructure.Application.Policy.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;

namespace TrasyRowerowe
{
    public class Startup
    {
        private readonly IKernel _kernel;
        private readonly string _msqlConnectionString;
        private readonly string _sqlServerConnectionString;
        private readonly string _clientID;
        private readonly string __clientSecret;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _kernel = new ApplicationConfig().GetKernel();
            Configuration = configuration;
            _sqlServerConnectionString = Configuration["ConnectionStrings:SQLServer"];
            _msqlConnectionString = Configuration["ConnectionStrings:Msql"];
            _clientID = Configuration["GoogleAuthentication:ClientId"];
            __clientSecret = Configuration["GoogleAuthentication:ClientSecret"];
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ReturnSingletons(ref services);
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle(options =>
            {
                options.ClientId = _clientID;
                options.ClientSecret = __clientSecret;
            })
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddDbContext<DataBaseIdentity>(options =>
             options.UseSqlServer(_sqlServerConnectionString, x => x.MigrationsAssembly("Application")));

            services.AddDbContext<DataBase>(options =>
             options.UseMySql(_msqlConnectionString, x => x.MigrationsAssembly("Application")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = default;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<DataBaseIdentity>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.Requirements.Add(new UserPolicyRequirement());
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new AdminPolicyRequirement());
                });
                
                options.AddPolicy("SuperAdmin", policy =>
                {
                    policy.Requirements.Add(new SuperAdminPolicyRequirement());
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            { 
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        IServiceCollection ReturnSingletons(ref IServiceCollection services)
        {
            services.AddSingleton(_kernel.Get<ISerializer>());
            services.AddSingleton(_kernel.Get<IDataBaseService>());
            services.AddSingleton(_kernel.Get<IFilePath>());
            services.AddSingleton(_kernel.Get<IDirectoryService>());
            services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();
            services.AddSingleton<IAuthorizationHandler, UserPolicyHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminPolicyHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminPolicyHandler>();

            return services;
        }
    }
}
