using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTOs
{
    // Data Transfer Object(DTO) for creating Book entities
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; } = null!; // avoid compilation error using "null!"

        [Required]
        public string Author { get; set; } = null!;

        public string? ISBN { get; set; }

        [Required]
        public bool IsAvailable{ get; set; }
    }
}