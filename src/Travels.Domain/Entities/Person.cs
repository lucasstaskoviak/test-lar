namespace Travels.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }
    public List<Phone> Phones { get; set; } = new();

    public Person(string name, string cpf, DateTime birthDate, bool isActive)
    {
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        IsActive = isActive;
    }

    public void AddPhone(Phone phone)
    {
        Phones.Add(phone);
    }
    
    public void RemovePhone(Phone phone)
    {
        Phones.Remove(phone);
    }
}
