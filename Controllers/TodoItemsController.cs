using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VariacaoApi.Models;

namespace VariacaoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()

        {

            return await _context.TodoItems.ToListAsync();

        }

        // GET: api/TodoItems/5
        [HttpGet("{day}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int day)
        {
            var todoItem = await _context.TodoItems.FindAsync(day);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{day}")]
        public async Task<IActionResult> PutTodoItem(int day, TodoItem todoItem)
        {
            if (day != todoItem.Day)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(day))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { day = todoItem.Day }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{day}")]
        public async Task<IActionResult> DeleteTodoItem(int day)
        {
            var todoItem = await _context.TodoItems.FindAsync(day);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int day)
        {
            return _context.TodoItems.Any(e => e.Day == day);
        }
    }
}
