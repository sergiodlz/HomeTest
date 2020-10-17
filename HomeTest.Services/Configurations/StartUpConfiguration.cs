using HomeTest.Services.Core;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTest.Services.Configurations
{
    public static class StartUpConfiguration
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
        }
    }
}