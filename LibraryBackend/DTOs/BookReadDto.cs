namespace LibraryBackend.DTOs
{

    // Data Transfer Object(DTO) for reading Book entities
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!; // avoid compilation error using "null!"
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable{ get; set; }
        
    }
}