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
        return Ok(new { status = true, code = 200, data = users });
    }

    // GET: api/User/getUserById/{id}
    // Retrieves a user by ID.
    [HttpGet("getUserById/{id}")]
    public async Task<ActionResult<UserDto>> GetUserByID(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(new { status = true, code = 200, data = user });
    }

    // POST: api/User/createUser
    // Create a new user.
    [HttpPost("createUser")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
    {
        var createdUser = await _userService.AddUserAsync(userDto);
        return CreatedAtAction(
            nameof(GetUserByID), 
            new { id = createdUser.Id }, 
            new { status = true, code = 201, data = createdUser 
        });
    }

    // PUT: api/User/updateUser/{id}
    // Updates an existing user with ID.
    [HttpPut("updateUser/{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
        return Ok(new { status = true, code = 200, data = updatedUser });
    }

    // DELETE: api/User/deleteUser/{id}
    // Delete the user with ID.
    [HttpDelete("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok(new { status = true, code = 200, data = $"user {id} deleted"});
    }
}
