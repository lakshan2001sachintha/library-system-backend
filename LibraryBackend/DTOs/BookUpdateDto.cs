using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTOs
{

    // Data Transfer Object(DTO) for updating Book entities
    public class BookUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!; // avoid compilation error using "null!"

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = null!;
        public string? ISBN { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;
    }
}