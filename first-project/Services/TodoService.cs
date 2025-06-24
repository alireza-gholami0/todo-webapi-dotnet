using first_project.Models;
using first_project.Repositories.Interfaces;

namespace first_project.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            return await _todoRepository.GetAll();
        }
        public async Task<TodoItem?> GetTodoItemById(string id)
        {
            return await _todoRepository.GetById(id);
        }
        public async Task CreateTodoItem(TodoItem todoItem)
        {
            await _todoRepository.Create(todoItem);
        }
        public async Task UpdateTodoItem(string id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                throw new ArgumentException("ID mismatch", nameof(id));
            }
            await _todoRepository.Update(id, todoItem);
        }
        public async Task<TodoItem?> DeleteTodoItem(string id)
        {
            var todoItem = await _todoRepository.GetById(id);
            if (todoItem == null)
            {
                return null;
            }

            await _todoRepository.Delete(id);
            return todoItem;
        }
    }
}
