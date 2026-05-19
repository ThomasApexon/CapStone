using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.DTO
{
    public class UserRegistrationDTO
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}