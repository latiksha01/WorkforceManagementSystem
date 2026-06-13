using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Employee;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuditLogService _auditLogService;

        public EmployeeService( IEmployeeRepository employeeRepository, IAuditLogService auditLogService)
        {
            _employeeRepository = employeeRepository;
            _auditLogService = auditLogService;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender,
                DOB = e.DOB,
                DOJ = e.DOJ,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName ?? string.Empty,
                RoleId = e.RoleId,
                RoleName = e.Role?.RoleName ?? string.Empty,
                Status = e.Status,
                CreatedOn = e.CreatedOn
            });
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var e = await _employeeRepository.GetByIdAsync(id);

            if (e == null)
                return null;

            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender,
                DOB = e.DOB,
                DOJ = e.DOJ,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName ?? string.Empty,
                RoleId = e.RoleId,
                RoleName = e.Role?.RoleName ?? string.Empty,
                Status = e.Status,
                CreatedOn = e.CreatedOn
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(
    CreateEmployeeDto dto,
    int performedBy)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                DOB = dto.DOB,
                DOJ = dto.DOJ,
                DepartmentId = dto.DepartmentId,
                RoleId = dto.RoleId,
                Status = dto.Status
            };

            var createdEmployee = await _employeeRepository.CreateAsync(employee);

            await _auditLogService.LogAsync(
            "Employee",
            createdEmployee.EmployeeId,
            "Create",
            performedBy
        );


            return await GetEmployeeByIdAsync(createdEmployee.EmployeeId)
                   ?? throw new Exception("Employee creation failed.");
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                return false;

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Gender = dto.Gender;
            employee.DOB = dto.DOB;
            employee.DOJ = dto.DOJ;
            employee.DepartmentId = dto.DepartmentId;
            employee.RoleId = dto.RoleId;
            employee.Status = dto.Status;

            await _employeeRepository.UpdateAsync(employee);
            await _auditLogService.LogAsync(
                "Employee",
                employee.EmployeeId,
                "Update",
                employee.EmployeeId
            );

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return false;

            await _employeeRepository.DeleteAsync(employee);
            await _auditLogService.LogAsync(
            "Employee",
            employee.EmployeeId,
            "Delete",
            employee.EmployeeId
        );

            return true;
        }
    }
}
