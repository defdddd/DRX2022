using DRX.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.DependencyInjection
{
    public static class MapperInjection
    {
        public static IServiceCollection MapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
