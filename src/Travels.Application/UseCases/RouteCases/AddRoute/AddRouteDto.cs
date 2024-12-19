namespace Travels.Application.UseCases.RouteCases.AddRoute;

public class AddRouteDto
{
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
