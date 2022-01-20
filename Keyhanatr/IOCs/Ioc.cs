using Keyhanatr.Core.Interfaces.Discounts;
using Keyhanatr.Core.Interfaces.Message;
using Keyhanatr.Core.Interfaces.Products;
using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Core.Senders;
using Keyhanatr.Core.Services.Discounts;
using Keyhanatr.Core.Services.Products;
using Keyhanatr.Core.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyhanatr
{
    public static class Ioc
    {
        public static void IocFunctions(this IServiceCollection service) {
            service.AddScoped<IMessageSender, MessageSender>();
            service.AddScoped<IProductServices, ProductServices>();
            service.AddScoped<IProductFeatureServices, ProductFeatureServices>();
            service.AddScoped<IProductSelectedFeature, ProductSelectedFeatureServices>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped<IProductGallery, ProductGallery>();
            service.AddScoped<IProductColorServices, ProductColorServices>();
            service.AddScoped<IDiscountServices, DiscountServices>();

            service.AddScoped<IUserService, UserService>();
        }
    }
}
