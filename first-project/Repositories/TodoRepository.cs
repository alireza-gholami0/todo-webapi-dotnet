using first_project.Configurations;
using first_project.Models;
using first_project.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace first_project.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMongoCollection<TodoItem> _todoItems;
        public TodoRepository(IOptions<MongoDbSettings> setting, IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _todoItems = db.GetCollection<TodoItem>("Todos");
        }
        public async Task Create(TodoItem todoItem)
        {
            await _todoItems.InsertOneAsync(todoItem);
        }

        public async Task Delete(string id)
        {
            await _todoItems.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _todoItems.Find(_ => true).ToListAsync();
        }

        public async Task<TodoItem?> GetById(string id)
        {
            return await _todoItems.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, TodoItem todoItem)
        {
            await _todoItems.ReplaceOneAsync(t => t.Id == id, todoItem);
        }
    }
}
