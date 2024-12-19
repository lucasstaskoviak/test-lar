using Travels.Application.Common;
using Travels.Application.Interfaces.Repositories;

namespace Travels.Application.UseCases.RouteCases.GetRoute;

public class GetRouteUseCase
{
    private readonly IRouteRepository _repository;

    public GetRouteUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<GetRouteDto>>> ExecuteAsync()
    {
        var routes = await _repository.GetAllAsync();
        if (routes == null || !routes.Any()) return Result<List<GetRouteDto>>.Failure("No routes found.");
        var routeDtos = routes.Select(route => new GetRouteDto(route.Id, route.Origin, route.Destination, route.Price)).ToList();

        return Result<List<GetRouteDto>>.Success(routeDtos);
    }
}
