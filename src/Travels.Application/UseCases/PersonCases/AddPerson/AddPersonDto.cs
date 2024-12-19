namespace Travels.Application.UseCases.PersonCases.AddPerson;

public class AddPersonDto
{
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<PhoneDto> Phones { get; set; } = new();
}
