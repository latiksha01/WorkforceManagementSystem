using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Project;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _projectService.GetAllProjectsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto dto)
        {
            var project = await _projectService.CreateProjectAsync(dto);

            return CreatedAtAction(
                nameof(GetProjectById),
                new { id = project.ProjectId },
                project);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto dto)
        {
            var result = await _projectService.UpdateProjectAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}