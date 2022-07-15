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
            services.AddTransient<IAuthService>
            (
                provider => new AuthService(provider.GetService<IRepositories>(),
                    Config.GetConnectionString("MySecretKey"),
                    provider.GetService<IMapper>())
            );

            //EmailService
            services.AddTransient<IEmailService>
                (
                    x =>
                       new EmailService(x.GetService<IAuthService>(),
                       x.GetService<IRepositories>().UserRepository,
                       Config.GetConnectionString("EmailKey")
                ));

            //ModelServices
            services.AddTransient<IBilingService, BilingService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRentService, RentService>();
            services.AddTransient<IVehicleService, VehicleService>();

            return services;
        }
    }
}
