using AutoMapper;
using DRX.DataAccess.UnitOfWork;
using DRX.Services.AuthService;
using DRX.Services.EmailService;
using DRX.Services.ModelServices;
using DRX.Services.ModelServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceConfiguration(this IServiceCollection services, IConfiguration Config)
        {
            //AuthService
            services.AddSingleton<IAuthService>
            (
                provider => new AuthService(provider.GetService<IRepositories>(),
                    Config.GetConnectionString("MySecretKey"),
                    provider.GetService<IMapper>())
            );

            //EmailService
            services.AddSingleton<IEmailService>
                (
                    x =>
                       new EmailService(x.GetService<IAuthService>(),
                       x.GetService<IRepositories>().UserRepository,
                       Config.GetConnectionString("EmailKey")
                ));

            //ModelServices
            services.AddSingleton<IBilingService, BilingService>();
            services.AddSingleton<IInvoiceService, InvoiceService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRentService, RentService>();
            services.AddSingleton<IVehicleService, VehicleService>();

            return services;
        }
    }
}
