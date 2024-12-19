using Travels.Application.Interfaces.Repositories;
using Travels.Application.Common;
using Travels.Domain.Entities;

namespace Travels.Application.UseCases.RouteCases.AddRoute
{
    public class AddRouteUseCase
    {
        private readonly IRouteRepository _repository;

        public AddRouteUseCase(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Route>> ExecuteAsync(AddRouteDto dto)
        {
            try
            {
                var route = new Route(dto.Origin, dto.Destination, dto.Price);
                await _repository.AddAsync(route);

                return Result<Route>.Success(route);
            }
            catch (Exception ex)
            {
                return Result<Route>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
