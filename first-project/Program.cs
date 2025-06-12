using Microsoft.EntityFrameworkCore;
using first_project.Models;
using first_project.Configurations;
using MongoDB.Driver;
using first_project.Infrastructure;
using first_project.Repositories.Interfaces;
using first_project.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSetting"));

builder.Services.AddSingleton<MongoClientFactory>();
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return sp.GetRequiredService<MongoClientFactory>().GetClient();
});

builder.Services.AddSingleton<ITodoRepository, TodoRepository>();



builder.Services.AddDbContext<TodoContext>(options =>
   options.UseInMemoryDatabase("TodoList"));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
