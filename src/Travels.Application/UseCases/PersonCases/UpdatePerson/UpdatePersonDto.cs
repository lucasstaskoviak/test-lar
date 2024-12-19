namespace Travels.Application.UseCases.PersonCases.UpdatePerson;

public record UpdatePersonDto(int Id, string Name, string CPF, bool IsActive, List<PhoneDto> Phones);