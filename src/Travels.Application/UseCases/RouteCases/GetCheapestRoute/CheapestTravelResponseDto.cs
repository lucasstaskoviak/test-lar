namespace Travels.Application.UseCases.RouteCases.GetCheapestRoute;

public class CheapestTravelResponseDto
{
    public string CheapestTravel { get; set; }

    public CheapestTravelResponseDto(List<string> path, decimal totalCost)
    {
        CheapestTravel = $"{string.Join(" - ", path)} ao custo de ${totalCost}";
     }
}

