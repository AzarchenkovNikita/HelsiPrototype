using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using MongoDB.Driver;

namespace HelsiPrototype.DAL;

public class MongoTaskListRepository : ITaskListRepository
{
    private readonly IMongoCollection<TaskList> _collection;

    public MongoTaskListRepository(MongoDbContext context)
    {
        _collection = context.TaskList;
    }

    public async Task CreateAsync(TaskList taskList)
    {
        await _collection.InsertOneAsync(taskList);
    }

    public async Task UpdateAsync(TaskList taskList)
    {
        var result = await _collection.ReplaceOneAsync(
            x => x.Id == taskList.Id,
            taskList
        );

        if (result.MatchedCount == 0)
            throw new Exception("TaskList not found");
    }

    public async Task UpdateRangeAsync(List<TaskList> taskLists)
    {
        if (taskLists == null || taskLists.Count == 0)
            return;

        var models = taskLists.Select(taskList =>
            new ReplaceOneModel<TaskList>(
                Builders<TaskList>.Filter.Eq(x => x.Id, taskList.Id),
                taskList)
            {
                IsUpsert = false
            }
        ).ToList();

        var result = await _collection.BulkWriteAsync(models);

        if (result.MatchedCount != taskLists.Count)
            throw new Exception("Some TaskLists were not found");
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount == 0)
            throw new Exception("TaskList not found");
    }

    public async Task<TaskList> GetAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TaskList>> GetRangeAsync(string userId, int skip, int take, bool orderByDescending)
    {
        var filter = Builders<TaskList>.Filter.Or(
            Builders<TaskList>.Filter.AnyEq(x => x.UserIdList, userId)
        );

        var query = _collection.Find(filter);

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

    public async Task<List<TaskList>> GetRangeAsync(string taskId)
    {
        var filter = Builders<TaskList>.Filter.Or(
            Builders<TaskList>.Filter.AnyEq(x => x.TaskIdList, taskId)
        );

        return await _collection.Find(filter).ToListAsync();
    }
}
