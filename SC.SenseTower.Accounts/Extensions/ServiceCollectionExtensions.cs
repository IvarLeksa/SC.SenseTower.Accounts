using FluentValidation;
using MediatR;
using Polly;
using Polly.Extensions.Http;
using SC.SenseTower.Accounts.Services;
using SC.SenseTower.Accounts.Settings;
using SC.SenseTower.Common.Validators;
using System.Reflection;

namespace SC.SenseTower.Accounts.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IdentityService>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();
            services.AddHttpClient<IdentityService>(client =>
            {
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
                .AddPolicyHandler(_ => HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(settings.MaxAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddPolicyHandler(_ => HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(settings.BreakAfter, TimeSpan.FromSeconds(settings.BreakForSeconds)));
            return services;
        }
    }
}
