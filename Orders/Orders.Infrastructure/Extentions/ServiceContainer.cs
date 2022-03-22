using Microsoft.Extensions.DependencyInjection;
using Orders.Infrastructure.AutoMapper;
using Orders.Infrastructure.Services.Authintications;
using Orders.Infrastructure.Services.Categories;
using Orders.Infrastructure.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Extentions
{
    public static class ServiceContainer
    {
        public static IServiceCollection servicesRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthinticationService, AuthinticationService>();

            return services;
        }
    }
}
