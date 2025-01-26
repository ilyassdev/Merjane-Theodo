using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merjane.Business.Implementations;
using Merjane.Business.Interfaces;
using Merjane.Business.Services;
using Merjane.DataAccess;
using Merjane.DataAccess.Context;
using Merjane.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Merjane.Business.DependencyExtension
{
    public static class DependencyInjectionResolver
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MerjaneDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //uow
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
