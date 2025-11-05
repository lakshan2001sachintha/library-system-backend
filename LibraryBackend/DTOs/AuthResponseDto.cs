namespace LibraryBackend.DTOs
{
    // DTO for authentication response containing user data and token
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserReadDto? User { get; set; }
        public string? Token { get; set; } // For future JWT implementation
    }
}
