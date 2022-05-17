using SC.SenseTower.Accounts.Services;
using SC.SenseTower.Common.Extensions;
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
            Assembly.GetCallingAssembly().GetTypes()
                .Where(r => r.IsSubclassOf(typeof(BaseValidator<>)))
                .ForEach(r => services.AddScoped(r));
            return services;
        }
    }
}
