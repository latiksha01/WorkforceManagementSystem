using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.EmployeeProjectAllocation;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectAllocationController : ControllerBase
    {
        private readonly IEmployeeProjectAllocationService _allocationService;

        public EmployeeProjectAllocationController(
            IEmployeeProjectAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _allocationService.GetAllAllocationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allocation = await _allocationService.GetAllocationByIdAsync(id);

            if (allocation == null)
                return NotFound();

            return Ok(allocation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateEmployeeProjectAllocationDto dto)
        {
            var allocation = await _allocationService.CreateAllocationAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = allocation.AllocationId },
                allocation);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateEmployeeProjectAllocationDto dto)
        {
            var result = await _allocationService.UpdateAllocationAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _allocationService.DeleteAllocationAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
