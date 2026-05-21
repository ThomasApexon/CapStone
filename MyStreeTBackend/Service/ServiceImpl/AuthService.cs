using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;
using MyStreeTBackend.Utils;

namespace MyStreeTBackend.Service.ServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly IuserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthService(IuserService userService, ITokenService tokenService, IUserRepository userRepository)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string Message, User User)> RegisterAsync(UserRegistrationDTO registrationDto)
        {
            // Check if user already exists
            var existingUser = await _userService.GetUserByEmailAsync(registrationDto.Email);
            if (existingUser != null)
            {
                return (false, "Email already registered", null);
            }

            // Hash password
            var passwordHash = await HashPasswordAsync(registrationDto.PasswordHash);

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registrationDto.Email,
                PasswordHash = passwordHash,
                IsAdmin = false
            };

            // Save to database
            var createdUser = await _userRepository.CreateUserAsync(user);
            return (true, "User registered successfully", createdUser);
        }

        public async Task<(bool Success, string Message, string Token)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return (false, "Invalid email or password", null);
            }

            if (!await ValidatePasswordAsync(loginDto.Password, user.PasswordHash))
            {
                return (false, "Invalid email or password", null);
            }

            var token = _tokenService.GenerateJwtToken(user);
            return (true, "Login successful", token);
        }

        public async Task<bool> ValidatePasswordAsync(string password, string passwordHash)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.Verify(password, passwordHash));
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }
    }
}