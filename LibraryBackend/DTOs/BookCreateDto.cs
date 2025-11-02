using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTOs
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Author { get; set; }
        public string? ISBN { get; set; }

        [Required]
        public bool IsAvailable{ get; set; }
    }
}