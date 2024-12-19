namespace Travels.Application.UseCases.RouteCases.GetRoute;

public class GetRouteDto
{
    public long Id { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public GetRouteDto(long id, string origin, string destination, decimal price)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Price = price;
    }
}

