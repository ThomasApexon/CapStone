using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Service;

namespace MyStreeTBackend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registrationDto)
        {
            var (success, message, user) = await _authService.RegisterAsync(registrationDto);
            
            if (!success)
            {
                var errorResponse = new ApiResponse<object>(false, message, null, new List<string> { message });
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<User>(true, message, user);
            return CreatedAtAction(nameof(Register), response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var (success, message, token) = await _authService.LoginAsync(loginDto);

            if (!success)
            {
                var errorResponse = new ApiResponse<object>(false, message, null, new List<string> { message });
                return Unauthorized(errorResponse);
            }

            var response = new ApiResponse<string>(true, message, token);
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var response = new ApiResponse<object>(true, "Logout successful", null);
            return Ok(response);
        }
    }
}
