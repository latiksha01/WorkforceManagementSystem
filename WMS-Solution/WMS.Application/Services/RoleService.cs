using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Role;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description,
                CreatedOn = r.CreatedOn
            });
        }

        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return null;

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                CreatedOn = role.CreatedOn
            };
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = new Role
            {
                RoleName = createRoleDto.RoleName,
                Description = createRoleDto.Description
            };

            var createdRole = await _roleRepository.CreateAsync(role);

            return new RoleDto
            {
                RoleId = createdRole.RoleId,
                RoleName = createdRole.RoleName,
                Description = createdRole.Description,
                CreatedOn = createdRole.CreatedOn
            };
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            var role = await _roleRepository.GetByIdAsync(updateRoleDto.RoleId);

            if (role == null)
                return false;

            role.RoleName = updateRoleDto.RoleName;
            role.Description = updateRoleDto.Description;

            await _roleRepository.UpdateAsync(role);

            return true;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return false;

            await _roleRepository.DeleteAsync(role);

            return true;
        }
    }
}
