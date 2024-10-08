namespace Coney.Backend.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public required  string FirstName { get; set; }
        public required  string LastName { get; set; }
        public required  string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
