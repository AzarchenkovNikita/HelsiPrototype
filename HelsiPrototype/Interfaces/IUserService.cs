using HelsiPrototype.DTO;
using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface IUserService
{
    Task<string> CreateAsync(string userName);

    Task DeleteAsync(string id);
    Task<List<User>> GetRangeAsync();
}
