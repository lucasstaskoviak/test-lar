using Travels.Application.Common;
using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Application.UseCases.RouteCases.UpdateRoute;

public class UpdateRouteUseCase
{
    private readonly IRouteRepository _repository;

    public UpdateRouteUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Route>> ExecuteAsync(UpdateRouteDto dto)
    {
        var existingRoute = await _repository.GetByIdAsync(dto.Id);
        if (existingRoute == null)
        {
            return Result<Route>.Failure("Route not found.");
        }

        existingRoute.Origin = dto.Origin;
        existingRoute.Destination = dto.Destination;
        existingRoute.Price = dto.Price;

        await _repository.UpdateAsync(existingRoute);

        return Result<Route>.Success(existingRoute);
    }
}
