using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using MongoDB.Driver;

namespace HelsiPrototype.DAL;

public class MongoTaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskEntity> _collection;

    public MongoTaskRepository(MongoDbContext context)
    {
        _collection = context.Tasks;
    }

    public async Task<TaskEntity> GetAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TaskEntity>> GetRangeAsync(List<string> idRange)
    {
        return await _collection
            .Find(x => idRange.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<List<TaskEntity>> GetRangeAsync(int skip, int take, bool orderByDescending)
    {
        var query = _collection.Find(_ => true);

        if (orderByDescending)
        {
            query = query.SortByDescending(x => x.CreatedAt);
        } else {
            query = query.SortBy(x => x.CreatedAt);
        }

        return await query
            .Skip(skip)
            .Limit(take)
            .ToListAsync();
    }

    public async Task CreateAsync(TaskEntity task)
    {
        await _collection.InsertOneAsync(task);
    }

    public async Task UpdateAsync(TaskEntity task)
    {
        var result = await _collection.ReplaceOneAsync(
            x => x.Id == task.Id,
            task
        );

        if (result.MatchedCount == 0)
            throw new Exception("Task not found");
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount == 0)
            throw new Exception("Task not found");
    }
}
