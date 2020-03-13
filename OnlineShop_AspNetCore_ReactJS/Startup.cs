using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Services;
using System;

namespace OnlineShop_AspNetCore_ReactJS
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

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddDbContext<OnlineShopContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPieService, PieService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>(sp => 
            {
                var dbContext = sp.GetRequiredService<OnlineShopContext>();
                var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

                string shoppingCartId = session.GetString("ShoppingCartId");
                if (string.IsNullOrWhiteSpace(shoppingCartId))
                {
                    shoppingCartId = Guid.NewGuid().ToString();
                    session.SetString("ShoppingCartId", shoppingCartId);
                }

                return new ShoppingCartService(dbContext, shoppingCartId);
            });
            services.AddScoped<IIterationService, IterationService>();
            services.AddScoped<IWorkItemService, WorkItemService>();

            services.AddAutoMapper(typeof(Startup).Assembly);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer(Configuration.GetValue<string>("ProxyToSpaDevelopmentServer"));
                }
            });
        }
    }
}
