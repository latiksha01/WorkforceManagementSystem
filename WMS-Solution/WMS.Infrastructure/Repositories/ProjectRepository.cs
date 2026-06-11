using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly WmsDbContext _context;

        public ProjectRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Client)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
