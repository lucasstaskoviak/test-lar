namespace Travels.Application.UseCases.PersonCases
{
    public class PhoneDto
    {
        public long Id { get; set; }
         public string Type { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;

        public PhoneDto(long id, string type, string number)
        {
            Id = id;
            Type = type;
            Number = number;
        }
    }
}