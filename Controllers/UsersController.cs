using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers;

    [EnableCors("myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService tasksService)
        {
            _usersService = tasksService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsersAsync()
        {
            var users = await _usersService.GetAsync();
            return users.Select(user => UserToDto(user)).ToList();
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserAsync(string id)
        {
            var user = await _usersService.GetAsync(id);

            return user is null ? NotFound() : UserToDto(user);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _usersService.GetAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            user.Username = userDto.Username;

            await _usersService.UpdateAsync(id, user);

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Username = userDto.Username
            };

          await _usersService.CreateAsync(user);

          // ReSharper disable once Mvc.ActionNotResolved
          return CreatedAtAction(nameof(GetUserAsync), 
              new {id = user.Id}, UserToDto(user));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var toDoItem = await _usersService.GetAsync(id);
            if (toDoItem is null)
            {
                return NotFound();
            }

            await _usersService.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> UserExistsAsync(string id)
        {
            var users = await _usersService.GetAsync();
            return users.Any(x => x.Id == id);
        }

        private static UserDTO UserToDto(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
            };
    }
