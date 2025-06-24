using first_project.Configurations;
using first_project.Models;
using first_project.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace first_project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _user;
        public UserRepository(IOptions<MongoDbSettings> setting, IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _user = db.GetCollection<User>("Users");
        }
        public async Task Create(User user)
        {
            await _user.InsertOneAsync(user);
        }
        public async Task Delete(string id)
        {
            await _user.DeleteOneAsync(u => u.Id == id);
        }
        public async Task<List<User>> GetAll()
        {
            return await _user.Find(_ => true).ToListAsync();
        }
        public async Task<User?> GetById(string id)
        {
            return await _user.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
        public async Task Update(string id, User user)
        {
            await _user.ReplaceOneAsync(u => u.Id == id, user);
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _user.Find(user => user.Email == email).FirstOrDefaultAsync();
        }
    }
}
