using LibraryBackend.Data;
using LibraryBackend.DTOs;
using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryBackend.Services
{
    // Service implementing authentication logic with password hashing
    public class AuthService : IAuthService
    {
        private readonly LibraryContext _context;

        public AuthService(LibraryContext context)
        {
            _context = context;
        }

        // Register a new user with password hashing
        public async Task<AuthResponseDto> RegisterAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username == userCreateDto.Username))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Username already exists"
                    };
                }

                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == userCreateDto.Email))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Email already exists"
                    };
                }

                // Hash the password
                string passwordHash = HashPassword(userCreateDto.Password);

                // Create new user entity
                var user = new User
                {
                    Username = userCreateDto.Username,
                    Email = userCreateDto.Email,
                    PasswordHash = passwordHash,
                    FullName = userCreateDto.FullName,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Return success response with user data
                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Registration successful",
                    User = MapToUserReadDto(user)
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        // Authenticate user and update last login timestamp
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                // Find user by username or email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == loginDto.UsernameOrEmail || 
                                            u.Email == loginDto.UsernameOrEmail);

                if (user == null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Invalid username/email or password"
                    };
                }

                // Verify password
                if (!VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Invalid username/email or password"
                    };
                }

                // Update last login timestamp
                user.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login successful",
                    User = MapToUserReadDto(user)
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        // Get user by ID
        public async Task<UserReadDto?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null ? MapToUserReadDto(user) : null;
        }

        // Hash password using SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Verify password against hash
        private bool VerifyPassword(string password, string passwordHash)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == passwordHash;
        }

        // Map User entity to UserReadDto
        private UserReadDto MapToUserReadDto(User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt
            };
        }
    }
}
