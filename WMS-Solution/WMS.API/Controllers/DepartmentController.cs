using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Department;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            return Ok(departments);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var department =
                await _departmentService.CreateDepartmentAsync(createDepartmentDto);

            return CreatedAtAction(
                nameof(GetDepartmentById),
                new { id = department.DepartmentId },
                department);
        }

        // PUT: api/Department
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var result =
                await _departmentService.UpdateDepartmentAsync(updateDepartmentDto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result =
                await _departmentService.DeleteDepartmentAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}