using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rescomac_back.business.Implement;
using rescomac_back.business.Interface;
using rescomac_back.repository.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rescomac_back
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RescomacDbContext>(options =>
               options.UseSqlServer(defaultConnectionString));

            services.AddScoped<ILoginService, LoginService>();

            var serviceProvider = services.BuildServiceProvider();
            try
            {
                var dbContext = serviceProvider.GetRequiredService<RescomacDbContext>();
                dbContext.Database.Migrate();
            }
            catch
            {
            }

            return services;
        }
    }
}