namespace Travels.Application.UseCases.RouteCases.GetRouteById;

public class GetRouteByIdDto
{
    public long Id { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public GetRouteByIdDto(long id, string origin, string destination, decimal price)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Price = price;
    }
}

