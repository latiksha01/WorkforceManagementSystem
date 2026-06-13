using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.AuditLog;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _auditLogService.GetAllAuditLogsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var log = await _auditLogService.GetAuditLogByIdAsync(id);

            if (log == null)
                return NotFound();

            return Ok(log);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuditLogDto dto)
        {
            var log = await _auditLogService.CreateAuditLogAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = log.AuditId },
                log);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuditLogDto dto)
        {
            var result = await _auditLogService.UpdateAuditLogAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _auditLogService.DeleteAuditLogAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}