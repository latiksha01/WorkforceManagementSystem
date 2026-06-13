using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface ILeaveRepository
    {
        Task<IEnumerable<Leave>> GetAllAsync();

        Task<Leave?> GetByIdAsync(int id);

        Task<Leave> CreateAsync(Leave leave);

        Task UpdateAsync(Leave leave);

        Task DeleteAsync(Leave leave);
    }
}