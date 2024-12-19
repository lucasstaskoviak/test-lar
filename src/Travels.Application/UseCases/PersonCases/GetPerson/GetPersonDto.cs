namespace Travels.Application.UseCases.PersonCases.GetPerson;

public class GetPersonDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }
    public List<PhoneDto> Phones { get; set; } = new();

    public GetPersonDto(long id, string name, string cpf, DateTime birthDate, bool isActive, List<PhoneDto> phones)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        IsActive = isActive;
        Phones = phones;
    }
}

