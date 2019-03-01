using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebStore.DALNew.Context;
using MyWebStore.DomainEntities.Entities;
using MyWebStore.Infrastructure.Implementations;
using MyWebStore.Infrastructure.Interfaces;
using System;

namespace MyWebStore
{
    public class Startup
    {
        /// <summary>
        /// Свойство для доступа к конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// Конструктор, принимающий интерфейс IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Добавление сервисов для MVC
            services.AddMvc();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData > ();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();

            services.AddScoped<ICartService, CookieCartService>();

            services.AddScoped<IOrderService, SqlOrderService>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyWebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt=>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Expiration = TimeSpan.FromDays(150);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddDbContext<MyWebStoreContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Поддержка статических файлов
            app.UseStaticFiles();

            app.UseAuthentication();

            //Обработка запросов в mvc-формате 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                                name: "areas",
                                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                                name: "default", 
                                template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
