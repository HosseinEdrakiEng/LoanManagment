using Application.Abstraction;
using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
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
            services.AddScoped<INotRegisterationCreditPlanRequestRepository, NotRegisterationCreditPlanRequestRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {



            services.AddScoped<INotRegisterationCreditPlanRequestServices, NotRegisterationCreditPlanRequestServices>();
            return services;
        }
    }
}
