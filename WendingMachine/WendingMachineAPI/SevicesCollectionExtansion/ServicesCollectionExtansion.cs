using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WendingMachineAPI.AppServices.Interfaces;
using WendingMachineAPI.AppServices.Services;
using WendingMachineDAL.EF;
using WendingMachineDAL.Repositories;
using WendingMachineDAL.RepositoryInterfaces;

namespace WendingMachineAPI.SevicesCollectionExtansion
{
    public static class ServicesCollectionExtansion
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, string connectionString)
        {
            var str = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={connectionString};Integrated Security=True;Connect Timeout=30";
            services.AddDbContext<WendingDbContext>(options => options.UseSqlServer(str));
            services.AddTransient<IDrinkService, DrinkService>();
            services.AddTransient<IDrinksRepository, DrinksRepository>();
            services.AddTransient<IWendingMachineRepository, WendingMachineRepository>();
            services.AddTransient<IWendingMachineService, WendingMachineService>();
            services.AddTransient<ICoinRepository, CoinRepository>();
            services.AddTransient<IHelpService, HelpService>();
            //services.AddTransient<ICoinStorageRepository, CoinStorageRepository>();

            return services;
        }
    }
}
