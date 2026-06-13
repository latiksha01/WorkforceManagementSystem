using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Department;

namespace WMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();

        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);

        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);

        Task<bool> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);

        Task<bool> DeleteDepartmentAsync(int id);
    }
}
