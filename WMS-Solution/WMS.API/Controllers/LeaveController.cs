using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Leave;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allLeaves = await _leaveService.GetAllLeavesAsync();

            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                return Ok(allLeaves);
            }

            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim == null || !int.TryParse(employeeIdClaim, out int employeeId))
            {
                return Forbid();
            }

            var ownLeaves = allLeaves.Where(l => l.EmployeeId == employeeId);
            return Ok(ownLeaves);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leave = await _leaveService.GetLeaveByIdAsync(id);

            if (leave == null)
                return NotFound();

            return Ok(leave);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveDto dto)
        {
            var leave = await _leaveService.CreateLeaveAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = leave.LeaveId },
                leave);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLeaveDto dto)
        {
            var employeeId =
                int.Parse(User.FindFirst("EmployeeId")!.Value);

            if (dto.Status == "Approved" ||
                dto.Status == "Rejected")
            {
                dto.ApprovedBy = employeeId;
                dto.ApprovedOn = DateTime.UtcNow;
            }

            var result = await _leaveService.UpdateLeaveAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _leaveService.DeleteLeaveAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}