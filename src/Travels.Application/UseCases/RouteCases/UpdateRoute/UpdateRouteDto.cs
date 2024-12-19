namespace Travels.Application.UseCases.RouteCases.UpdateRoute;

public record UpdateRouteDto(int Id, string Origin, string Destination, decimal Price);