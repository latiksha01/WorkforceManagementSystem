using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Announcement;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _announcementService.GetAllAnnouncementsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementService.GetAnnouncementByIdAsync(id);

            if (announcement == null)
                return NotFound();

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementDto dto)
        {
            var announcement = await _announcementService.CreateAnnouncementAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = announcement.AnnouncementId },
                announcement);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAnnouncementDto dto)
        {
            var result = await _announcementService.UpdateAnnouncementAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _announcementService.DeleteAnnouncementAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}