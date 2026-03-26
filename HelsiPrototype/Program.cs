using HelsiPrototype.DAL;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Middleware;
using HelsiPrototype.Model;
using HelsiPrototype.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<ITaskRepository, MongoTaskRepository>();
builder.Services.AddSingleton<IUserRepository, MongoUserRepository>();
builder.Services.AddSingleton<ITaskListRepository, MongoTaskListRepository>();

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskListService, TaskListService>();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
