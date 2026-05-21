using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Service
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User User)> RegisterAsync(UserRegistrationDTO registrationDto);
        Task<(bool Success, string Message, string Token)> LoginAsync(LoginDto loginDto);
        Task<bool> ValidatePasswordAsync(string password, string passwordHash);
        Task<string> HashPasswordAsync(string password);
    }
}