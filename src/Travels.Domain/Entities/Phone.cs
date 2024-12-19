using Travels.Domain.Enums;

namespace Travels.Domain.Entities;

public class Phone
{
    public long Id { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; }

    public Phone(PhoneType type, string number)
    {
        Type = type;
        Number = number;
    }
}
