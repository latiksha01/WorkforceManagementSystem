using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Leave;

namespace WMS.Application.Interfaces
{
    public interface ILeaveService
    {
        Task<IEnumerable<LeaveDto>> GetAllLeavesAsync();

        Task<LeaveDto?> GetLeaveByIdAsync(int id);

        Task<LeaveDto> CreateLeaveAsync(CreateLeaveDto dto);

        Task<bool> UpdateLeaveAsync(UpdateLeaveDto dto);

        Task<bool> DeleteLeaveAsync(int id);
    }
}
