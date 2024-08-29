using System;
using System.ComponentModel.DataAnnotations;

namespace Coney.Backend.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]  // Verifica que el correo tenga un formato válido
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
