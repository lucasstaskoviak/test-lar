using Travels.Application.Interfaces.Repositories;

namespace Travels.Application.UseCases.RouteCases.DeleteRoute
{
    public class DeleteRouteUseCase
    {
        private readonly IRouteRepository _repository;

        public DeleteRouteUseCase(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(long id)
        {
            var route = await _repository.GetByIdAsync(id);
            if (route == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
