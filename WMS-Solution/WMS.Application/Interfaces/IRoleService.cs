using System;
using System.Collections.Generic;
using System.Text;

using WMS.Application.DTOs.Role;

namespace WMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();

        Task<RoleDto?> GetRoleByIdAsync(int id);

        Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);

        Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto);

        Task<bool> DeleteRoleAsync(int id);
    }
}
