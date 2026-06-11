using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int id);

        Task<Project> CreateAsync(Project project);

        Task UpdateAsync(Project project);

        Task DeleteAsync(Project project);
    }
}
