using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.EmployeeProjectAllocation;

namespace WMS.Application.Interfaces
{
    public interface IEmployeeProjectAllocationService
    {
        Task<IEnumerable<EmployeeProjectAllocationDto>> GetAllAllocationsAsync();

        Task<EmployeeProjectAllocationDto?> GetAllocationByIdAsync(int id);

        Task<EmployeeProjectAllocationDto> CreateAllocationAsync(CreateEmployeeProjectAllocationDto dto);

        Task<bool> UpdateAllocationAsync(UpdateEmployeeProjectAllocationDto dto);

        Task<bool> DeleteAllocationAsync(int id);
    }
}
