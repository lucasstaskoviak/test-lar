using Travels.Domain.Entities;

namespace Travels.Application.Interfaces.Repositories;

public interface IPersonRepository
{
    Task AddAsync(Person person);
    Task<Person?> GetByIdAsync(long id);
    Task<List<Person>> GetAllAsync();
    Task UpdateAsync(Person person);
    Task DeleteAsync(long id);
}