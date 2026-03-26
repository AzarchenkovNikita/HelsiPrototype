using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;

namespace HelsiPrototype.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CreateAsync(string userName)
    {
        if (userName.Equals(string.Empty))
            throw new Exception("Username is empty");

        User user = new User()
        {
            Name = userName
        };
        await _repository.CreateAsync(user);
        return user.Id;
    }

    public async Task DeleteAsync(string id)
    {
        User user = await _repository.GetAsync(id);

        if (user is null)
            throw new Exception("User not exist");

        await _repository.DeleteAsync(id);
    }

    public async Task<List<User>> GetRangeAsync()
    {
        return await _repository.GetRangeAsync();
    }
}
