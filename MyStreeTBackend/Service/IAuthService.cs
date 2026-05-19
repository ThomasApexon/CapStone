using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;

namespace MyStreeTBackend.Service
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(LoginDto loginDto);
    }
}