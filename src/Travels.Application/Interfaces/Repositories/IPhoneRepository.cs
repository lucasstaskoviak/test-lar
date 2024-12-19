using Travels.Domain.Entities;

namespace Travels.Application.Interfaces.Repositories;

public interface IPhoneRepository
{
    Task AddAsync(Phone phone);
    Task<Phone?> GetByIdAsync(long id);
    Task<List<Phone>> GetAllAsync();
    Task UpdateAsync(Phone phone);
    Task DeleteAsync(long id);
}