using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTOs
{
    public class BookUpdateDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable{ get; set; }
    }
}