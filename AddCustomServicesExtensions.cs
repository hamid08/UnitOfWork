using Microsoft.Extensions.DependencyInjection;
using RohaniShop.Data.Contracts;
using RohaniShop.Data.Repositories;
using RohaniShop.Data.UnitOfWork;
using RohaniShop.Services;
using RohaniShop.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RohaniShop.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
           
            return services;
        }
    }
}
