#nullable disable
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [EnableCors("myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly TasksService _tasksService;

        public ToDoController(TasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<List<ToDoItemDTO>>> GetTodoItemsAsync()
        {
            var tasks = await _tasksService.GetAsync();
            return tasks.Select(x => ItemToDto(x)).ToList();
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> GetToDoItemAsync(string id)
        {
            var toDoItem = await _tasksService.GetAsync(id);

            if (toDoItem is null)
            {
                return NotFound();
            }

            return ItemToDto(toDoItem);
        }

        // PUT: api/ToDo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItemAsync(string id, ToDoItemDTO toDoItemDto)
        {
            if (id != toDoItemDto.Id)
            {
                return BadRequest();
            }

            var toDoItem = await _tasksService.GetAsync(id);
            if (toDoItem is null)
            {
                return NotFound();
            }

            toDoItem.Name = toDoItemDto.Name;
            toDoItem.IsComplete = toDoItemDto.IsComplete;

            await _tasksService.UpdateAsync(id, toDoItem);

            return NoContent();
        }

        // POST: api/ToDo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> CreateToDoItemAsync(ToDoItemDTO toDoItemDto)
        {
            var toDoItem = new ToDoItem
            {
                IsComplete = toDoItemDto.IsComplete,
                Name = toDoItemDto.Name
            };

          await _tasksService.CreateAsync(toDoItem);

          // ReSharper disable once Mvc.ActionNotResolved
          return CreatedAtAction(nameof(GetToDoItemAsync),
                new {id = toDoItem.Id}, ItemToDto(toDoItem));
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(string id)
        {
            var toDoItem = await _tasksService.GetAsync(id);
            if (toDoItem is null)
            {
                return NotFound();
            }

            await _tasksService.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> ToDoItemExistsAsync(string id)
        {
            var tasks = await _tasksService.GetAsync();
            return tasks.Any(x => x.Id == id);
        }

        private static ToDoItemDTO ItemToDto(ToDoItem toDoItem) =>
            new ToDoItemDTO
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
    }
}