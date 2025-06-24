using first_project.Models;

namespace first_project.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(string id);
        Task Create(User user);
        Task Update(string id, User user);
        Task Delete(string id);
        Task<User?> GetByEmail(string email);
    }
}
