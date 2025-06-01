using Application.Abstraction;
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
            services.Configure<UserConfig>(configuration.GetSection("UserConfig"));
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
            services.AddScoped<ICreditRequestRepository, CreditRequestRepository>();
            services.AddScoped<ILimitationRepository, LimitationRepository>();
            
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ICreditRequestServices, CreditRequestServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletServices, WalletServices>();
            services.AddScoped<IInquiryServices, InquiryServices>();
            

            services.AddScoped<ICreditRequestStrategy, UploadDocumentStrategy>();
            services.AddScoped<ICreditRequestStrategy, LoanProcessingStrategy>();

            services.AddScoped<CreditRequestStrategyFactory>();

            //To Do
            services.AddSingleton<BackgroundWorkerService>();
            return services;
        }
    }
}
