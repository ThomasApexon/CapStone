using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.Utils
{
    public static class Bcrypt
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}