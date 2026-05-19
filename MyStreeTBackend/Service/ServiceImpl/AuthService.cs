using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Utils;

namespace MyStreeTBackend.Service.ServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly IuserService _userService;
        private readonly TokenService _tokenService;
        public AuthService(IuserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        public Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = _userService.GetUserByEmailAsync(loginDto.Email).Result;
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    // Generate JWT token
                    var token = _tokenService.GenerateJwtToken(user);
                    return Task.FromResult(token);
                }
            }
            throw new UnauthorizedAccessException("Invalid email or password");
        }
    }
}