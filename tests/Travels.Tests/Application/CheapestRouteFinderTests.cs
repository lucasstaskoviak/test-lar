using Moq;
using FluentAssertions;
using Xunit;
using Travels.Application.Interfaces;
using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;

namespace Travels.Application.Tests
{
    public class CheapestRouteFinderTests
    {
        private readonly IGetCheapestRouteCommand _getCheapestRouteCommand;
        private readonly Mock<IRouteRepository> _routeRepositoryMock;

        public CheapestRouteFinderTests()
        {
            _routeRepositoryMock = new Mock<IRouteRepository>();

            _routeRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Route>
                {
                    new Route("GRU", "BRC", 10),
                    new Route("BRC", "SCL", 5),
                    new Route("GRU", "CDG", 75),
                    new Route("GRU", "SCL", 20),
                    new Route("GRU", "ORL", 56),
                    new Route("ORL", "CDG", 5),
                    new Route("SCL", "ORL", 20),
                    new Route("FRA", "LCA", 1)
                });

            _getCheapestRouteCommand = new GetCheapestRouteCommand(_routeRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_CheapestRoute_When_ValidRequest()
        {
            var result = await _getCheapestRouteCommand.FindCheapestRouteAsync("GRU", "CDG");
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var cheapestTravel = result.Value.CheapestTravel;
            cheapestTravel.Should().Be("GRU - BRC - SCL - ORL - CDG ao custo de $40");
        }

        [Fact]
        public async Task Should_Return_Failure_When_No_Route_Exists()
        {
            var result = await _getCheapestRouteCommand.FindCheapestRouteAsync("GRU", "FRA");
            result.Should().NotBeNull();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("No route available between the specified locations.");
        }

        [Fact]
        public async Task Should_Return_Failure_When_Locations_Do_Not_Exist()
        {
            var result = await _getCheapestRouteCommand.FindCheapestRouteAsync("GRU", "XYZ");
            result.Should().NotBeNull();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("One or both locations do not exist in the database.");
        }
    }
}
