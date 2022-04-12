using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Users.Models;

namespace ToDoApi.Users;

    
    [Route("api/[controller]")]
    [Authorize]
    [EnableCors("myAllowSpecificOrigins")]

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService tasksService)
        {
            _usersService = tasksService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            var users = await _usersService.GetAsync();
            return users.Select(user => UserToDto(user)).ToList();
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(string id)
        {
            var user = await _usersService.GetAsync(id);

            return user is null ? NotFound() : UserToDto(user);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UserDto userDto)
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
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password
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

        private static UserDto UserToDto(User user) =>
            new UserDto
            {
                Id = user.Id,
                Username = user.Username,
            };

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] LoginCredentials credentials) {
            var token = _usersService.Authenticate(credentials);
            return token.Result is null ? Unauthorized() : Ok(new{token=token.Result});
        }   

    }
