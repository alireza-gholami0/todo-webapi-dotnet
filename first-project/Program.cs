using first_project.Configurations;
using first_project.Infrastructure;
using first_project.Models;
using first_project.Repositories;
using first_project.Repositories.Interfaces;
using first_project.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

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
builder.Services.AddSingleton<TodoService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddSingleton<AuthService>();

builder.Services.AddDbContext<TodoContext>(options =>
   options.UseInMemoryDatabase("TodoList"));


builder.Services.AddControllers();

builder.Services.AddAuthentication()
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
