using Microsoft.Extensions.DependencyInjection;
using Travels.Application.Interfaces.Repositories;
using Travels.Infrastructure.Repositories;

namespace Travels.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPhoneRepository, PhoneRepository>();
            
            return services;
        }
    }
}
