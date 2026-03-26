using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HelsiPrototype.Model;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<TaskEntity> Tasks =>
        _database.GetCollection<TaskEntity>("Tasks");

    public IMongoCollection<TaskList> TaskList =>
        _database.GetCollection<TaskList>("TaskList");

    public IMongoCollection<User> User =>
        _database.GetCollection<User>("User");
}
