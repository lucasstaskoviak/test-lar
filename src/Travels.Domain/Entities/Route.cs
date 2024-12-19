namespace Travels.Domain.Entities;

public class Route
{
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public decimal Price { get; set; }

    public Route(string origin, string destination, decimal price)
    {
        Origin = origin;
        Destination = destination;
        Price = price;
    }
}
