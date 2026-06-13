using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Department;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();

            return departments.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                CreatedOn = d.CreatedOn
            });
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return null;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Description = department.Description,
                CreatedOn = department.CreatedOn
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department
            {
                DepartmentName = createDepartmentDto.DepartmentName,
                Description = createDepartmentDto.Description
            };

            var createdDepartment = await _departmentRepository.CreateAsync(department);

            return new DepartmentDto
            {
                DepartmentId = createdDepartment.DepartmentId,
                DepartmentName = createdDepartment.DepartmentName,
                Description = createdDepartment.Description,
                CreatedOn = createdDepartment.CreatedOn
            };
        }

        public async Task<bool> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await _departmentRepository.GetByIdAsync(updateDepartmentDto.DepartmentId);

            if (department == null)
                return false;

            department.DepartmentName = updateDepartmentDto.DepartmentName;
            department.Description = updateDepartmentDto.Description;

            await _departmentRepository.UpdateAsync(department);

            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return false;

            await _departmentRepository.DeleteAsync(department);

            return true;
        }
    }
}
