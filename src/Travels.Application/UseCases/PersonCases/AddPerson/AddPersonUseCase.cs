using Travels.Application.Interfaces.Repositories;
using Travels.Application.Common;
using Travels.Domain.Entities;
using Travels.Domain.Enums;

namespace Travels.Application.UseCases.PersonCases.AddPerson
{
    public class AddPersonUseCase
    {
        private readonly IPersonRepository _repository;
        private readonly IPhoneRepository _phoneRepository; // Supondo que você tenha um repositório para Phone

        public AddPersonUseCase(IPersonRepository repository, IPhoneRepository phoneRepository)
        {
            _repository = repository;
            _phoneRepository = phoneRepository;
        }

        public async Task<Result<Person>> ExecuteAsync(AddPersonDto dto)
        {
            try
            {
                foreach (var phone in dto.Phones)
                {
                    if (!Enum.TryParse<PhoneType>(phone.Type, true, out var phoneType))
                    {
                        return Result<Person>.Failure($"Invalid phone type: {phone.Type}. The value must be Celular, Residencial or Comercial");
                    }
                }

                var person = new Person(dto.Name, dto.CPF, DateTime.Now, dto.IsActive);
                await _repository.AddAsync(person);

                foreach (var phone in dto.Phones)
                {
                    var phoneType = Enum.Parse<PhoneType>(phone.Type, true);
                    var newPhone = new Phone(phoneType, phone.Number);

                    await _phoneRepository.AddAsync(newPhone);

                    person.AddPhone(newPhone);
                }

                return Result<Person>.Success(person);
            }
            catch (Exception ex)
            {
                return Result<Person>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
