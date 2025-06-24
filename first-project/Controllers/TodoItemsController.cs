using first_project.Models;
using first_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoItemsController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _todoService.GetAllTodoItems();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem([FromRoute] string id)
    {
        var todoItem = await _todoService.GetTodoItemById(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItem todoItem)
    {
        await _todoService.CreateTodoItem(todoItem);
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

        await _todoService.UpdateTodoItem(id, todoItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TodoItem>> DeleteTodoItem([FromRoute] string id)
    {
        var todoItem = await _todoService.DeleteTodoItem(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        return Ok(todoItem);
    }
}

