#nullable disable
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Tasks.Models;

namespace ToDoApi.Tasks
{
    [Authorize]
    [EnableCors("myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksService _tasksService;

        public TasksController(TasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<List<TaskItemDto>>> GetTodoItemsAsync()
        {
            var tasks = await _tasksService.GetAsync(Util.User.GetUserId(HttpContext));
            return tasks.Select(x => ItemToDto(x)).ToList();
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetToDoItemAsync(string id)
        {
            var toDoItem = await _tasksService.GetAsync(id, Util.User.GetUserId(HttpContext));

            if (toDoItem is null)
            {
                return NotFound();
            }

            return ItemToDto(toDoItem);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItemAsync(string id, TaskItemDto taskItemDto)
        {
            if (id != taskItemDto.Id)
            {
                return BadRequest();
            }

            var toDoItem = await _tasksService.GetAsync(id, Util.User.GetUserId(HttpContext));
            if (toDoItem is null)
            {
                return NotFound();
            }

            toDoItem.Name = taskItemDto.Name;
            toDoItem.IsComplete = taskItemDto.IsComplete;

            await _tasksService.UpdateAsync(id, toDoItem);

            return NoContent();
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateToDoItemAsync(TaskItemDto taskItemDto)
        {
            var toDoItem = new TaskItem
            {
                IsComplete = taskItemDto.IsComplete,
                Name = taskItemDto.Name,
                Owner = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value
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

        private async Task<bool> TaskExistsAsync(string id)
        {
            var tasks = await _tasksService.GetAsync(id);
            return tasks.Any(x => x.Id == id);
        }

        private static TaskItemDto ItemToDto(TaskItem taskItem) =>
            new TaskItemDto
            {
                Id = taskItem.Id,
                Name = taskItem.Name,
                IsComplete = taskItem.IsComplete,
                Owner = taskItem.Owner
            };
    }
}