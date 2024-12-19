using Travels.Application.Common;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;

namespace Travels.Application.Interfaces;

public interface IGetCheapestRouteCommand
{
    Task<Result<CheapestTravelResponseDto>> FindCheapestRouteAsync(string from, string to);
}
