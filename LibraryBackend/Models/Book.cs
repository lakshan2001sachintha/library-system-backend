using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Models
{
    public class Book
    {
        public int Id { get; set; } // This is the Primary key

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Author { get; set; }
        
        public string? ISBN { get; set; }

        public bool IsAvailable{ get; set; }
    }
}