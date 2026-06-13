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
            return Ok(await _leaveService.GetAllLeavesAsync());
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