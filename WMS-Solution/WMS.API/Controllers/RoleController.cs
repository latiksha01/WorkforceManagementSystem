using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Role;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var role = await _roleService.CreateRoleAsync(createRoleDto);

            return CreatedAtAction(
                nameof(GetRoleById),
                new { id = role.RoleId },
                role);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var result = await _roleService.UpdateRoleAsync(updateRoleDto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}