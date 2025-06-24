using first_project.Models;
using first_project.Models.DTOs;
using first_project.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace first_project.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }
        public async Task<User?> GetUserById(string id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task CreateUser(User user)
        {
            var existingUser = _userRepository.GetByEmail(user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with email '{user.Email}' already exists.");
            }

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _userRepository.Create(user);
        }
        public async Task UpdateUserName(string id, EditUserNameDto editUserNameDto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{id}' not found.");
            }
            user.Name = editUserNameDto.Name;
            await _userRepository.Update(id, user);
        }
        public async Task<User?> DeleteUser(string id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return null;
            }
            await _userRepository.Delete(id);
            return user;
        }
    }
}
