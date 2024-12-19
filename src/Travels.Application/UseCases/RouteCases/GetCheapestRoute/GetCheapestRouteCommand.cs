using Travels.Application.Interfaces.Repositories;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Application.Common;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;

namespace Travels.Application.UseCases.RouteCases.GetCheapestRoute
{
    public class GetCheapestRouteCommand : IGetCheapestRouteCommand
    {
        private readonly IRouteRepository _routeRepository;

        public GetCheapestRouteCommand(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<Result<CheapestTravelResponseDto>> FindCheapestRouteAsync(string from, string to)
        {
            var allRoutes = await _routeRepository.GetAllAsync();
            var graph = BuildGraph(allRoutes);

            if (!graph.ContainsKey(from) || !graph.ContainsKey(to))
            {
                return Result<CheapestTravelResponseDto>.Failure("One or both locations do not exist in the database.");
            }

            var (totalCost, path) = Dijkstra(graph, from, to);

            if (path == null || !path.Any())
            {
                return Result<CheapestTravelResponseDto>.Failure("No route available between the specified locations.");
            }

            var airportsPath = path.Select(r => r.Origin).Append(to).ToList();
            var responseDto = new CheapestTravelResponseDto(airportsPath, totalCost);

            return Result<CheapestTravelResponseDto>.Success(responseDto);
        }

        private Dictionary<string, List<(string destination, decimal price)>> BuildGraph(List<Route> routes)
        {
            var graph = new Dictionary<string, List<(string destination, decimal price)>>();

            foreach (var route in routes)
            {
                if (!graph.ContainsKey(route.Origin))
                {
                    graph[route.Origin] = new List<(string destination, decimal price)>();
                }

                graph[route.Origin].Add((route.Destination, route.Price));

                if (!graph.ContainsKey(route.Destination))
                {
                    graph[route.Destination] = new List<(string destination, decimal price)>();
                }
            }

            return graph;
        }

        private (decimal totalCost, List<Route>? path) Dijkstra(
            Dictionary<string, List<(string destination, decimal price)>> graph,
            string origin,
            string destination)
        {
            var distances = new Dictionary<string, decimal>();
            var previous = new Dictionary<string, string>();
            var priorityQueue = new SortedSet<(decimal distance, string node)>();

            foreach (var node in graph.Keys)
            {
                distances[node] = decimal.MaxValue;
            }

            distances[origin] = 0;
            priorityQueue.Add((0, origin));

            while (priorityQueue.Any())
            {
                var (currentDistance, currentNode) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentNode == destination)
                {
                    break;
                }

                foreach (var (neighbor, weight) in graph[currentNode])
                {
                    var newDistance = currentDistance + weight;

                    if (newDistance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = newDistance;
                        previous[neighbor] = currentNode;
                        priorityQueue.Add((newDistance, neighbor));
                    }
                }
            }

            if (distances[destination] == decimal.MaxValue)
            {
                return (decimal.MaxValue, null);
            }

            var path = new List<Route>();
            var current = destination;

            while (previous.ContainsKey(current))
            {
                var prev = previous[current];
                var price = graph[prev].First(x => x.destination == current).price;
                path.Add(new Route(prev, current, price));
                current = prev;
            }

            path.Reverse();
            return (distances[destination], path);
        }

    }
}
