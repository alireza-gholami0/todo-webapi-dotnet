using first_project.Models;

namespace first_project.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAll();
        Task<TodoItem?> GetById(string id);
        Task Create(TodoItem todoItem);
        Task Update(string id, TodoItem todoItem);
        Task Delete(string id);
    }
}
