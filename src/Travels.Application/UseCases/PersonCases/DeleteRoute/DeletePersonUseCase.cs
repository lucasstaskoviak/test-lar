using Travels.Application.Interfaces.Repositories;

namespace Travels.Application.UseCases.PersonCases.DeletePerson
{
    public class DeletePersonUseCase
    {
        private readonly IPersonRepository _repository;

        public DeletePersonUseCase(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(long id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
