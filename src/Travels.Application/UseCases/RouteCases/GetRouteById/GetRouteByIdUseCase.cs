using Travels.Application.Common;
using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Application.UseCases.RouteCases.GetRouteById;

public class GetRouteByIdUseCase
{
    private readonly IRouteRepository _repository;

    public GetRouteByIdUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetRouteByIdDto?>> ExecuteAsync(long id)
    {
        var route = await _repository.GetByIdAsync(id);
        if (route == null) return Result<GetRouteByIdDto?>.Failure("Route not found.");
        var routeDto = new GetRouteByIdDto(route.Id, route.Origin, route.Destination, route.Price);

        return Result<GetRouteByIdDto?>.Success(routeDto);
    }
}
