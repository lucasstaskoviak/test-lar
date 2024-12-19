using Travels.Application.Interfaces.Repositories;
using Travels.Application.Common;
using Travels.Domain.Entities;
using Travels.Domain.Enums;

namespace Travels.Application.UseCases.PersonCases.UpdatePerson
{
    public class UpdatePersonUseCase
    {
        private readonly IPersonRepository _repository;
        private readonly IPhoneRepository _phoneRepository;

        public UpdatePersonUseCase(IPersonRepository repository, IPhoneRepository phoneRepository)
        {
            _repository = repository;
            _phoneRepository = phoneRepository;
        }

        public async Task<Result<Person>> ExecuteAsync(UpdatePersonDto dto)
        {
            try
            {
                var person = await _repository.GetByIdAsync(dto.Id);
                if (person == null)
                {
                    return Result<Person>.Failure("Person not found.");
                }

                person.Name = dto.Name;
                person.CPF = dto.CPF;
                person.IsActive = dto.IsActive;

                foreach (var phone in person.Phones.ToList())
                {
                    person.RemovePhone(phone);
                    await _phoneRepository.DeleteAsync(phone.Id);
                }

                foreach (var phoneDto in dto.Phones)
                {
                    if (!Enum.TryParse<PhoneType>(phoneDto.Type, true, out var phoneType))
                    {
                        return Result<Person>.Failure($"Invalid phone type: {phoneDto.Type}");
                    }

                    var newPhone = new Phone(phoneType, phoneDto.Number);
                    await _phoneRepository.AddAsync(newPhone);
                    person.AddPhone(newPhone);
                }

                await _repository.UpdateAsync(person);

                return Result<Person>.Success(person);
            }
            catch (Exception ex)
            {
                return Result<Person>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}