using DRX.Models;
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
            services.AddSingleton<IValidator<UserData>, UserValidator>();
            services.AddSingleton<IValidator<BilingData>, BilingValidator>();
            services.AddSingleton<IValidator<InvoiceData>, InvoiceValidator>();
            services.AddSingleton<IValidator<VehicleData>, VehicleValidator>();
            services.AddSingleton<IValidator<RentData>, RentValidator>();

            return services;
        }
    }
}
