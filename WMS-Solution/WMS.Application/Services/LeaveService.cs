using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Leave;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IEnumerable<LeaveDto>> GetAllLeavesAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync();

            return leaves.Select(l => new LeaveDto
            {
                LeaveId = l.LeaveId,
                EmployeeId = l.EmployeeId,
                EmployeeName = l.Employee != null
                    ? $"{l.Employee.FirstName} {l.Employee.LastName}"
                    : string.Empty,
                LeaveType = l.LeaveType,
                Reason = l.Reason,
                FromDate = l.FromDate,
                ToDate = l.ToDate,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                ApprovedBy = l.ApprovedBy,
                ApprovedByName = l.ApprovedByEmployee != null
                    ? $"{l.ApprovedByEmployee.FirstName} {l.ApprovedByEmployee.LastName}"
                    : null,
                ApprovedOn = l.ApprovedOn,
                CreatedOn = l.CreatedOn
            });
        }

        public async Task<LeaveDto?> GetLeaveByIdAsync(int id)
        {
            var l = await _leaveRepository.GetByIdAsync(id);

            if (l == null)
                return null;

            return new LeaveDto
            {
                LeaveId = l.LeaveId,
                EmployeeId = l.EmployeeId,
                EmployeeName = l.Employee != null
                    ? $"{l.Employee.FirstName} {l.Employee.LastName}"
                    : string.Empty,
                LeaveType = l.LeaveType,
                Reason = l.Reason,
                FromDate = l.FromDate,
                ToDate = l.ToDate,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                ApprovedBy = l.ApprovedBy,
                ApprovedByName = l.ApprovedByEmployee != null
                    ? $"{l.ApprovedByEmployee.FirstName} {l.ApprovedByEmployee.LastName}"
                    : null,
                ApprovedOn = l.ApprovedOn,
                CreatedOn = l.CreatedOn
            };
        }

        public async Task<LeaveDto> CreateLeaveAsync(CreateLeaveDto dto)
        {
            var leave = new Leave
            {
                EmployeeId = dto.EmployeeId,
                LeaveType = dto.LeaveType,
                Reason = dto.Reason,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Status = "Pending"
            };

            var created = await _leaveRepository.CreateAsync(leave);

            return await GetLeaveByIdAsync(created.LeaveId)
                ?? throw new Exception("Leave creation failed.");
        }

        public async Task<bool> UpdateLeaveAsync(UpdateLeaveDto dto)
        {
            var leave = await _leaveRepository.GetByIdAsync(dto.LeaveId);

            if (leave == null)
                return false;

            leave.EmployeeId = dto.EmployeeId;
            leave.LeaveType = dto.LeaveType;
            leave.Reason = dto.Reason;
            leave.FromDate = dto.FromDate;
            leave.ToDate = dto.ToDate;
            leave.Status = dto.Status;
            leave.ApprovedBy = dto.ApprovedBy;
            leave.ApprovedOn = dto.ApprovedOn;

            await _leaveRepository.UpdateAsync(leave);

            return true;
        }

        public async Task<bool> DeleteLeaveAsync(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);

            if (leave == null)
                return false;

            await _leaveRepository.DeleteAsync(leave);

            return true;
        }
    }
}
