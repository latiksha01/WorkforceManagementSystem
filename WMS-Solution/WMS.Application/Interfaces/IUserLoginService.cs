using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.UserLogin;

namespace WMS.Application.Interfaces
{
    public interface IUserLoginService
    {
        Task<IEnumerable<UserLoginDto>> GetAllUsersAsync();

        Task<UserLoginDto?> GetUserByIdAsync(int id);

        Task<UserLoginDto> CreateUserAsync(CreateUserLoginDto dto);

        Task<bool> UpdateUserAsync(UpdateUserLoginDto dto);

        Task<bool> DeleteUserAsync(int id);
    }
}
