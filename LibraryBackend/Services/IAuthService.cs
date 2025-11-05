using LibraryBackend.DTOs;

namespace LibraryBackend.Services
{
    // Interface defining authentication service operations
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserCreateDto userCreateDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserReadDto?> GetUserByIdAsync(int userId);
    }
}
