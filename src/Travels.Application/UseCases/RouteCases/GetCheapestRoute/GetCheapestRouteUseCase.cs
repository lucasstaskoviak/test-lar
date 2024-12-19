using Travels.Application.Interfaces;
using Travels.Application.Common;

namespace Travels.Application.UseCases.RouteCases.GetCheapestRoute;

public class GetCheapestRouteUseCase
{
    private readonly IGetCheapestRouteCommand _routeCommand;

    public GetCheapestRouteUseCase(IGetCheapestRouteCommand routeCommand)
    {
        _routeCommand = routeCommand;
    }

    public async Task<Result<CheapestTravelResponseDto>> ExecuteAsync(string from, string to)
    {
        return await _routeCommand.FindCheapestRouteAsync(from, to);
    }
}
