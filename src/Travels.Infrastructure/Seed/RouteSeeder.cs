using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Seed;

public class RouteSeeder
{
    private readonly IRouteRepository _repository;

    public RouteSeeder(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task SeedAsync()
    {
        var existingRoutes = await _repository.GetAllAsync();
        if (existingRoutes.Any())
        {
            return;
        }

        var routes = new List<Route>
        {
            new Route("GRU", "BRC", 10),
            new Route("BRC", "SCL", 5),
            new Route("GRU", "CDG", 75),
            new Route("GRU", "SCL", 20),
            new Route("GRU", "ORL", 56),
            new Route("ORL", "CDG", 5),
            new Route("SCL", "ORL", 20),
        };

        foreach (var route in routes)
        {
            await _repository.AddAsync(route);
        }
    }
}
