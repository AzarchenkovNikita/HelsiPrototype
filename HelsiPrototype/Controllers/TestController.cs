using HelsiPrototype.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HelsiPrototype.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly IMongoClient _client;
    private readonly MongoDbSettings _settings;

    public TestController(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        _client = client;
        _settings = settings.Value;
    }

    [HttpGet("ping-db")]
    public async Task<IActionResult> Ping()
    {
        try {
            var db = _client.GetDatabase(_settings.DatabaseName);
            var result = await db.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
            return Ok(new
            {
                status = "MongoDB OK",
                response = result.ToString(),
                app_version = "v0.1.0"
            });
        } catch (Exception ex) {
            return StatusCode(500, ex.Message);
        }
    }
}
