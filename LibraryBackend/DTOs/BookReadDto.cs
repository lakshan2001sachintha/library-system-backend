namespace LibraryBackend.DTOs
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable{ get; set; }
        
    }
}