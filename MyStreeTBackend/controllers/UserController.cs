using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;
using MyStreeTBackend.Service;

namespace MyStreeTBackend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IuserService _userService;
        public UserController(IuserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationDTO user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            var response = new ApiResponse<bool>(true, "User registered successfully", createdUser);
            return Ok(response);
        }
    }
}