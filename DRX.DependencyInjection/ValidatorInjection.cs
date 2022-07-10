using DRX.DTOs;
using DRX.Validators.ModelValidator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DependencyInjection
{
    public static class ValidatorInjection
    {
        public static IServiceCollection ValidationConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<UserDTO>, UserValidator>();
            services.AddSingleton<IValidator<BilingDTO>, BilingValidator>();
            services.AddSingleton<IValidator<InvoiceDTO>, InvoiceValidator>();
            services.AddSingleton<IValidator<VehicleDTO>, VehicleValidator>();
            services.AddSingleton<IValidator<RentDTO>, RentValidator>();

            return services;
        }
    }
}
