using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Models
{
    // This class represents a User entity in the database
    public class User
    {
        public int Id { get; set; } // Primary key

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty; // Unique username

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Unique email

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Hashed password

        [MaxLength(100)]
        public string? FullName { get; set; } // Optional full name

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Account creation timestamp

        public DateTime? LastLoginAt { get; set; } // Last login timestamp
    }
}
