using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);

    Task DeleteAsync(string id);
    Task<User> GetAsync(string id);
    Task<List<User>> GetRangeAsync();

}
