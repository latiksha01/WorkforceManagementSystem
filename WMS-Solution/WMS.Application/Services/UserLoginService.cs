using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.UserLogin;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userRepository;

        public UserLoginService(IUserLoginRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserLoginDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserLoginDto
            {
                UserId = u.UserId,
                EmployeeId = u.EmployeeId,
                EmployeeName = u.Employee != null
                    ? $"{u.Employee.FirstName} {u.Employee.LastName}"
                    : string.Empty,
                Username = u.Username,
                RoleId = u.RoleId,
                RoleName = u.Role?.RoleName ?? string.Empty,
                LastLogin = u.LastLogin,
                CreatedOn = u.CreatedOn
            });
        }

        public async Task<UserLoginDto?> GetUserByIdAsync(int id)
        {
            var u = await _userRepository.GetByIdAsync(id);

            if (u == null)
                return null;

            return new UserLoginDto
            {
                UserId = u.UserId,
                EmployeeId = u.EmployeeId,
                EmployeeName = u.Employee != null
                    ? $"{u.Employee.FirstName} {u.Employee.LastName}"
                    : string.Empty,
                Username = u.Username,
                RoleId = u.RoleId,
                RoleName = u.Role?.RoleName ?? string.Empty,
                LastLogin = u.LastLogin,
                CreatedOn = u.CreatedOn
            };
        }

        public async Task<UserLoginDto> CreateUserAsync(CreateUserLoginDto dto)
        {
            var user = new UserLogin
            {
                EmployeeId = dto.EmployeeId,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId
            };

            var created = await _userRepository.CreateAsync(user);

            return await GetUserByIdAsync(created.UserId)
                ?? throw new Exception("User creation failed.");
        }

        public async Task<bool> UpdateUserAsync(UpdateUserLoginDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);

            if (user == null)
                return false;

            user.EmployeeId = dto.EmployeeId;
            user.Username = dto.Username;
            user.RoleId = dto.RoleId;

            await _userRepository.UpdateAsync(user);

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);

            return true;
        }
    }
}
