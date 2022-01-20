using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Core.Services.Products;
using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keyhanatr.Core.Services.Users;
using Keyhanatr.Core.Senders;
using Keyhanatr.Core.Interfaces.Message;
using Keyhanatr.Core.Interfaces.Sliders;
using Keyhanatr.Core.Services.Sliders;
using Keyhanatr.Core.Interfaces.Brands;
using Keyhanatr.Core.Services.Brands;
using Keyhanatr.Core.Interfaces.Orders;
using Keyhanatr.Core.Services.Orders;

namespace Keyhanatr
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
            
            //services.AddMvc(options =>
            //{
            //    options.EnableEndpointRouting = false;
            //});

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(option =>
                {
                    option.LoginPath = "/Login";
                    option.LogoutPath = "/Logout";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                });


            #endregion

            #region DB Context

            services.AddDbContext<KeyhanatrContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Keyhanatr"));
            });

            #endregion

            #region IoC
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IProductFeatureServices, ProductFeatureServices>();
            services.AddTransient<IProductSelectedFeature, ProductSelectedFeatureServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IProductGallery, ProductGallery>();
            services.AddTransient<IProductColorServices, ProductColorServices>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISliderServices, SliderService>();
            services.AddScoped<IBrandService, BrandServices>();
            services.AddScoped<IShopingCardService, ShopingCardService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //// Very Important
            using (var scope =
                app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<KeyhanatrContext>())
                context.Database.Migrate();
            //////
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
            //app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=home}/{action=index}/{id?}"
            );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
      
        }
    }
}
