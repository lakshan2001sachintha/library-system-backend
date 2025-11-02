using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Models
{
    // This class represent a Book entity in the database
    public class Book
    {
        public int Id { get; set; } // This is the Primary key

        [Required]
        public string Title { get; set; } = string.Empty; // avoid null value for the Title

        [Required]
        public string Author { get; set; } = string.Empty; // avoid null value for the Author
        
        public string? ISBN { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}