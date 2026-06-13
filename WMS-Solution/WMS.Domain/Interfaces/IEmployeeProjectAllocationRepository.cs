using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IEmployeeProjectAllocationRepository
    {
        Task<IEnumerable<EmployeeProjectAllocation>> GetAllAsync();

        Task<EmployeeProjectAllocation?> GetByIdAsync(int id);

        Task<EmployeeProjectAllocation> CreateAsync(EmployeeProjectAllocation allocation);

        Task UpdateAsync(EmployeeProjectAllocation allocation);

        Task DeleteAsync(EmployeeProjectAllocation allocation);
    }
}
