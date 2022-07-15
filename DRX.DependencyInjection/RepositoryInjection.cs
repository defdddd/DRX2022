using DRX.DataAccess.SqlDataAcces;
using DRX.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection RepositoryConfiguration(this IServiceCollection services, IConfiguration Config)
        {
            services.AddSingleton<ISqlDataAccess>(new SqlDataAccess(Config.GetConnectionString("DataBase")));
            services.AddTransient<IRepositories, Repositories>();

            return services;
        }
    }
}
