using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Auth;
using WMS.Application.DTOs.UserLogin;

namespace WMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);

        Task LogAsync(
            string entityName,
            int recordId,
            string action,
            int createdBy);
    }
}
