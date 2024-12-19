using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Repositories;

public class PhoneRepository : IPhoneRepository
{
    private readonly List<Phone> _phones = new();
    private int _currentId = 1;

    public Task<List<Phone>> GetAllAsync()
    {
        return Task.FromResult(_phones);
    }

    public Task<Phone?> GetByIdAsync(long id)
    {
        var phone = _phones.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(phone);
    }

    public Task AddAsync(Phone phone)
    {
        phone.Id = _currentId++;
        _phones.Add(phone);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(long id)
    {
        var phone = _phones.FirstOrDefault(r => r.Id == id);
        if (phone != null)
        {
            _phones.Remove(phone);
        }
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Phone phone)
    {
        var index = _phones.FindIndex(r => r.Id == phone.Id);
        if (index != -1)
        {
            _phones[index] = phone;
        }
        await Task.CompletedTask;
    }
}
