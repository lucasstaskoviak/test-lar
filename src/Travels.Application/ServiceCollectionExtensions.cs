using Microsoft.Extensions.DependencyInjection;
using Travels.Application.UseCases.RouteCases.AddRoute;
using Travels.Application.UseCases.RouteCases.DeleteRoute;
using Travels.Application.UseCases.RouteCases.GetRoute;
using Travels.Application.UseCases.RouteCases.GetRouteById;
using Travels.Application.UseCases.RouteCases.UpdateRoute;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;

using Travels.Application.UseCases.PersonCases.AddPerson;
using Travels.Application.UseCases.PersonCases.GetPerson;
using Travels.Application.UseCases.PersonCases.GetPersonById;
using Travels.Application.UseCases.PersonCases.UpdatePerson;


namespace Travels.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<AddRouteUseCase>();
            services.AddTransient<GetRouteByIdUseCase>();
            services.AddTransient<GetRouteUseCase>();
            services.AddTransient<DeleteRouteUseCase>();
            services.AddTransient<UpdateRouteUseCase>();
            services.AddTransient<GetCheapestRouteUseCase>();

            services.AddTransient<AddPersonUseCase>();
            services.AddTransient<GetPersonUseCase>();
            services.AddTransient<GetPersonByIdUseCase>();
            services.AddTransient<UpdatePersonUseCase>();
            
            return services;
        }
    }
}
