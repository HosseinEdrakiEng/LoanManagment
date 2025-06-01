using Application.Abstraction;
using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Common;
using Helper;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureOption(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.Configure<InqueryConfig>(configuration.GetSection("InqueryConfig"));
            services.Configure<WalletConfig>(configuration.GetSection("WalletConfig"));
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
            services.AddScoped<ICreditPlanRepository, CreditPlanRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ICreditRequestServices, CreditRequestServices>();
            return services;
        }
    }
}
