using first_project.Models;
using first_project.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoItemsController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _todoRepository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem([FromRoute] string id)
    {
        var todoItem = await _todoRepository.GetById(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItem todoItem)
    {
        await _todoRepository.Create(todoItem);
        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItem>> PutTodoItem(
        [FromRoute] string id,
        [FromBody] TodoItem todoItem
        )
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }

        await _todoRepository.Update(id, todoItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TodoItem>> DeleteTodoItem([FromRoute] string id)
    {
        var todoItem = await _todoRepository.GetById(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        await _todoRepository.Delete(id);
        return Ok(todoItem);
    }
}

