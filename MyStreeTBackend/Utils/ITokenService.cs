using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Utils
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
    }
}