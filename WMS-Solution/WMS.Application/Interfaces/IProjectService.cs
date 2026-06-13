using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Project;

namespace WMS.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();

        Task<ProjectDto?> GetProjectByIdAsync(int id);

        Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);

        Task<bool> UpdateProjectAsync(UpdateProjectDto dto);

        Task<bool> DeleteProjectAsync(int id);
    }
}
