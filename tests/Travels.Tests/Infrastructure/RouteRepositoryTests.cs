using FluentAssertions;
using Travels.Domain.Entities;
using Travels.Infrastructure.Repositories;
using Xunit;

namespace Travels.Tests.Repositories
{
    public class RouteRepositoryTests
    {
        private readonly RouteRepository _routeRepository;

        public RouteRepositoryTests()
        {
            _routeRepository = new RouteRepository();
        }

        [Fact]
        public async Task AddAsync_Should_Add_New_Route()
        {
            // Arrange
            var route = new Route("GRU", "CDG", 500m);

            // Act
            await _routeRepository.AddAsync(route);
            var allRoutes = await _routeRepository.GetAllAsync();

            // Assert
            allRoutes.Should().HaveCount(1);
            allRoutes.First().Should().BeEquivalentTo(route, options => options.Excluding(r => r.Id));
            allRoutes.First().Id.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_Route()
        {
            // Arrange
            var route1 = new Route("GRU", "CDG", 500m);
            var route2 = new Route("JFK", "LHR", 700m);

            await _routeRepository.AddAsync(route1);
            await _routeRepository.AddAsync(route2);

            // Act
            var result = await _routeRepository.GetByIdAsync(2);

            // Assert
            result.Should().NotBeNull();
            result!.Origin.Should().Be("JFK");
            result.Destination.Should().Be("LHR");
            result.Price.Should().Be(700m);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Existing_Route()
        {
            // Arrange
            var route = new Route("GRU", "CDG", 500m);
            await _routeRepository.AddAsync(route);

            var updatedRoute = new Route("GRU", "JFK", 600m) { Id = 1 };

            // Act
            await _routeRepository.UpdateAsync(updatedRoute);
            var result = await _routeRepository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Origin.Should().Be("GRU");
            result.Destination.Should().Be("JFK");
            result.Price.Should().Be(600m);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Route()
        {
            // Arrange
            var route1 = new Route("GRU", "CDG", 500m);
            var route2 = new Route("JFK", "LHR", 700m);

            await _routeRepository.AddAsync(route1);
            await _routeRepository.AddAsync(route2);

            // Act
            await _routeRepository.DeleteAsync(1);
            var allRoutes = await _routeRepository.GetAllAsync();

            // Assert
            allRoutes.Should().HaveCount(1);
            allRoutes.First().Id.Should().Be(2);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
        {
            // Act
            var result = await _routeRepository.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }
    }
}
