using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role> CreateAsync(Role role);

        Task UpdateAsync(Role role);

        Task DeleteAsync(Role role);
    }
}
