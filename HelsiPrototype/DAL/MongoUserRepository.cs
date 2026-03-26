using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using MongoDB.Driver;

namespace HelsiPrototype.DAL;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public MongoUserRepository(MongoDbContext context)
    {
        _collection = context.User;
    }

    public async Task CreateAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount == 0)
            throw new Exception("User not found");
    }

    public async Task<User> GetAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetRangeAsync()
    {
        return await _collection
            .Find(_ => true)
            .ToListAsync();
    }
}
