using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Service
{
    public interface IuserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(UserRegistrationDTO user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> IsUserAdminAsync(Guid id);
    }
}