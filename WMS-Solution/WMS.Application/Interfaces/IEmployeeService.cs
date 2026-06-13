using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Employee;

namespace WMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);

        Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
