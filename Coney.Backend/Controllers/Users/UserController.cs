using Coney.Backend.DTOs.Users;
using Coney.Backend.Services.Users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // GET: api/User/getUsers
    // Returns a list of all users.
    [HttpGet("getUsers")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // GET: api/User/getUserById/{id}
    // Returns a single user by ID.
    [HttpGet("getUserById/{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    // POST: api/User/createUser
    // Create a new user.
    [HttpPost("createUser")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
    {
        await _userService.AddUserAsync(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = userDto.Email }, userDto);
    }

    // PUT: api/User/updateUser/{id}
    // Updates an existing user with ID.
    [HttpPut("updateUser/{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        await _userService.UpdateUserAsync(id, updateUserDto);
        return NoContent(); 
    }

    // DELETE: api/User/deleteUser/{id}
    // Delete the user with ID.
    [HttpDelete("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
