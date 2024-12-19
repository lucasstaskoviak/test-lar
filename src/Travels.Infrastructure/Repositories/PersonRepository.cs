using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly List<Person> _persons = new();
    private int _currentId = 1;

    public Task<List<Person>> GetAllAsync()
    {
        return Task.FromResult(_persons);
    }

    public Task<Person?> GetByIdAsync(long id)
    {
        var person = _persons.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(person);
    }

    public Task AddAsync(Person person)
    {
        person.Id = _currentId++;
        _persons.Add(person);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(long id)
    {
        var person = _persons.FirstOrDefault(r => r.Id == id);
        if (person != null)
        {
            _persons.Remove(person);
        }
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Person person)
    {
        var index = _persons.FindIndex(r => r.Id == person.Id);
        if (index != -1)
        {
            _persons[index] = person;
        }
        await Task.CompletedTask;
    }
}
