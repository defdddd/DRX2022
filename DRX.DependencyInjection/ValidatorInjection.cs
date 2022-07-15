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
            services.AddTransient<IValidator<UserDTO>, UserValidator>();
            services.AddTransient<IValidator<BilingDTO>, BilingValidator>();
            services.AddTransient<IValidator<InvoiceDTO>, InvoiceValidator>();
            services.AddTransient<IValidator<VehicleDTO>, VehicleValidator>();
            services.AddTransient<IValidator<RentDTO>, RentValidator>();

            return services;
        }
    }
}
