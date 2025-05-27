using Application.Abstraction;
using Helper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureOption(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<NotificationConfig>(configuration.GetSection("NotificationConfig"));
            services.Configure<SsoConfig>(configuration.GetSection("SsoConfig"));
            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoanManagmentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoanManagmentContext")));

            services.AddScoped<ILoanManagmentDbContext, LoanManagmentDbContext>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services;
        }
    }
}
