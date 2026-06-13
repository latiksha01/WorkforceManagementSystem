using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.EmployeeProjectAllocation;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class EmployeeProjectAllocationService : IEmployeeProjectAllocationService
    {
        private readonly IEmployeeProjectAllocationRepository _allocationRepository;

        public EmployeeProjectAllocationService(
            IEmployeeProjectAllocationRepository allocationRepository)
        {
            _allocationRepository = allocationRepository;
        }

        public async Task<IEnumerable<EmployeeProjectAllocationDto>> GetAllAllocationsAsync()
        {
            var allocations = await _allocationRepository.GetAllAsync();

            return allocations.Select(a => new EmployeeProjectAllocationDto
            {
                AllocationId = a.AllocationId,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                ProjectId = a.ProjectId,
                ProjectName = a.Project?.ProjectName ?? string.Empty,
                AssignedOn = a.AssignedOn,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            });
        }

        public async Task<EmployeeProjectAllocationDto?> GetAllocationByIdAsync(int id)
        {
            var a = await _allocationRepository.GetByIdAsync(id);

            if (a == null)
                return null;

            return new EmployeeProjectAllocationDto
            {
                AllocationId = a.AllocationId,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                ProjectId = a.ProjectId,
                ProjectName = a.Project?.ProjectName ?? string.Empty,
                AssignedOn = a.AssignedOn,
                Status = a.Status,
                CreatedBy = a.CreatedBy,
                CreatedOn = a.CreatedOn
            };
        }

        public async Task<EmployeeProjectAllocationDto> CreateAllocationAsync(
            CreateEmployeeProjectAllocationDto dto)
        {
            var allocation = new EmployeeProjectAllocation
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                AssignedOn = dto.AssignedOn,
                CreatedBy = dto.CreatedBy,
                Status = dto.Status
            };

            var created = await _allocationRepository.CreateAsync(allocation);

            return await GetAllocationByIdAsync(created.AllocationId)
                ?? throw new Exception("Allocation creation failed.");
        }

        public async Task<bool> UpdateAllocationAsync(
            UpdateEmployeeProjectAllocationDto dto)
        {
            var allocation = await _allocationRepository.GetByIdAsync(dto.AllocationId);

            if (allocation == null)
                return false;

            allocation.EmployeeId = dto.EmployeeId;
            allocation.ProjectId = dto.ProjectId;
            allocation.AssignedOn = dto.AssignedOn;
            allocation.Status = dto.Status;
            allocation.UpdatedBy = dto.UpdatedBy;

            await _allocationRepository.UpdateAsync(allocation);

            return true;
        }

        public async Task<bool> DeleteAllocationAsync(int id)
        {
            var allocation = await _allocationRepository.GetByIdAsync(id);

            if (allocation == null)
                return false;

            await _allocationRepository.DeleteAsync(allocation);

            return true;
        }
    }
}
