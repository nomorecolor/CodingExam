using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Services;
using CodingExam.Infrastructure.Context;
using CodingExam.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CodingExam.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<CodingExamContext>();

            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
