using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;

namespace MyStreeTBackend.Service.ServiceImpl
{
    public class UserService : IuserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
        }   
        public Task<bool> CreateUserAsync(UserRegistrationDTO user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.PasswordHash))
            {
                if(_userRepository.GetUserByEmailAsync(user.Email).Result != null)
                {
                    throw new InvalidOperationException("User with this email already exists.");
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                var userEntity = mapper.Map<User>(user);
                var isSaved = _userRepository.CreateUserAsync(userEntity);
                return Task.FromResult(isSaved != null);
            }
            throw new ArgumentNullException(nameof(user));
        }

        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAdminAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}