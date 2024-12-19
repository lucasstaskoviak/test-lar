using Travels.Application.Common;
using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Application.UseCases.PersonCases.GetPersonById;

public class GetPersonByIdUseCase
{
    private readonly IPersonRepository _repository;

    public GetPersonByIdUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetPersonByIdDto?>> ExecuteAsync(long id)
    {
        var person = await _repository.GetByIdAsync(id);
        if (person == null) return Result<GetPersonByIdDto?>.Failure("Person not found.");
        var personDto = new GetPersonByIdDto(
            person.Id,
            person.Name,
            person.CPF,
            person.BirthDate,
            person.IsActive,
            person.Phones.Select(phone => new PhoneDto(phone.Id, phone.Type.ToString(), phone.Number)).ToList()
            );

        return Result<GetPersonByIdDto?>.Success(personDto);
    }
}
