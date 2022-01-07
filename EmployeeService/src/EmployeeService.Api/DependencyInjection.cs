using EmployeeService.Core.Interfaces.Repositories;
using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Core.Services;
using EmployeeService.Infrastructure.Context;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }


            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            string dbConnectionString = string.Empty;
            if (!(bool)(appSettings?.ByPassKeyVault))
            {
                // Use for localhost
                dbConnectionString = configuration.GetConnectionString("Employee");
            }
            else
            {

            }
            //
            services.AddDbContext<EmployeeDbContext>(
                optionsAction: options => options.UseSqlServer(dbConnectionString),
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Transient);

            // Add application services.
            services.AddScoped<IEmployeeService, EmployeesService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Example mutiple implements 
            services.AddScoped<HeavyHorn>();
            services.AddScoped<LightHorn>();
            services.AddTransient<ServiceResolver>(serviceProvider => serviceTypeName =>
            {
                switch (serviceTypeName)
                {
                    case ServiceType.Heavy:
                        return serviceProvider.GetService<HeavyHorn>();
                    case ServiceType.Light:
                        return serviceProvider.GetService<LightHorn>();
                    default:
                        return null;
                }
            });



            services.AddHttpClient();
            return services;
        }
        // Instance for IHorn
        public delegate IHorn ServiceResolver(ServiceType serviceType);

        public enum ServiceType
        {
            Heavy,
            Light,

        }
    }
}
