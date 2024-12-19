using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Repositories;

public class RouteRepository : IRouteRepository
{
    private readonly List<Route> _routes = new();
    private int _currentId = 1;

    public Task<List<Route>> GetAllAsync()
    {
        return Task.FromResult(_routes);
    }

    public Task<Route?> GetByIdAsync(long id)
    {
        var route = _routes.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(route);
    }

    public Task AddAsync(Route route)
    {
        route.Id = _currentId++;
        _routes.Add(route);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(long id)
    {
        var route = _routes.FirstOrDefault(r => r.Id == id);
        if (route != null)
        {
            _routes.Remove(route);
        }
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Route route)
    {
        var index = _routes.FindIndex(r => r.Id == route.Id);
        if (index != -1)
        {
            _routes[index] = route;
        }
        await Task.CompletedTask;
    }
}
