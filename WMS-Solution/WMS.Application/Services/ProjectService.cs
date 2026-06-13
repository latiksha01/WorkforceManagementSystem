using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Project;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IAuditLogService _auditLogService;

        public ProjectService(
            IProjectRepository projectRepository,
            IAuditLogService auditLogService)
        {
            _projectRepository = projectRepository;
            _auditLogService = auditLogService;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                ClientId = p.ClientId,
                ClientName = p.Client?.ClientName ?? string.Empty,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                CreatedOn = p.CreatedOn
            });
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var p = await _projectRepository.GetByIdAsync(id);

            if (p == null)
                return null;

            return new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                ClientId = p.ClientId,
                ClientName = p.Client?.ClientName ?? string.Empty,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                CreatedOn = p.CreatedOn
            };
        }

        public async Task<ProjectDto> CreateProjectAsync(
    CreateProjectDto dto,
    int performedBy)
        {
            var project = new Project
            {
                ProjectName = dto.ProjectName,
                ClientId = dto.ClientId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };

            var createdProject = await _projectRepository.CreateAsync(project);

            await _auditLogService.LogAsync(
    "Project",
    createdProject.ProjectId,
    "Create",
    performedBy
);

            return await GetProjectByIdAsync(createdProject.ProjectId)
                ?? throw new Exception("Project creation failed.");
        }

        public async Task<bool> UpdateProjectAsync(
    UpdateProjectDto dto,
    int performedBy)
        
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (project == null)
                return false;

            project.ProjectName = dto.ProjectName;
            project.ClientId = dto.ClientId;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Status = dto.Status;

            await _projectRepository.UpdateAsync(project);
            await _auditLogService.LogAsync(
    "Project",
    project.ProjectId,
    "Update",
    performedBy
);


            return true;
        }

        public async Task<bool> DeleteProjectAsync(
    int id,
    int performedBy)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                return false;

            await _projectRepository.DeleteAsync(project);
            await _auditLogService.LogAsync(
    "Project",
    project.ProjectId,
    "Delete",
    performedBy
);

            return true;
        }
    }
}