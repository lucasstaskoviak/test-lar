using Travels.Application.Common;
using Travels.Application.Interfaces.Repositories;

namespace Travels.Application.UseCases.PersonCases.GetPerson;

public class GetPersonUseCase
{
    private readonly IPersonRepository _repository;

    public GetPersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<GetPersonDto>>> ExecuteAsync()
    {
        var persons = await _repository.GetAllAsync();
        if (persons == null || !persons.Any()) return Result<List<GetPersonDto>>.Failure("No persons found.");
        var personDtos = persons.Select(person => new GetPersonDto(
            person.Id,
            person.Name,
            person.CPF,
            person.BirthDate,
            person.IsActive,
            person.Phones.Select(phone => new PhoneDto(phone.Id, phone.Type.ToString(), phone.Number)).ToList()
            )).ToList();

        return Result<List<GetPersonDto>>.Success(personDtos);
    }
}
