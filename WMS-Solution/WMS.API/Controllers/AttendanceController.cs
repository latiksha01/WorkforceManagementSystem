using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Attendance;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allAttendance = await _attendanceService.GetAllAttendancesAsync();

            // Admin (and Manager) can see everyone's attendance
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                return Ok(allAttendance);
            }

            // Employee can only see their own attendance
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim == null || !int.TryParse(employeeIdClaim, out int employeeId))
            {
                return Forbid(); // logged in, but has no linked employee record
            }

            var ownAttendance = allAttendance.Where(a => a.EmployeeId == employeeId);
            return Ok(ownAttendance);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);

            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAttendanceDto dto)
        {
            var attendance = await _attendanceService.CreateAttendanceAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = attendance.AttendanceId },
                attendance);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAttendanceDto dto)
        {
            var result = await _attendanceService.UpdateAttendanceAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attendanceService.DeleteAttendanceAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}