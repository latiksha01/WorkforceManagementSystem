using System;
using System.Collections.Generic;
using System.Text;

using WMS.Application.DTOs.Attendance;

namespace WMS.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync();

        Task<AttendanceDto?> GetAttendanceByIdAsync(int id);

        Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto dto);

        Task<bool> UpdateAttendanceAsync(UpdateAttendanceDto dto);

        Task<bool> DeleteAttendanceAsync(int id);
    }
}
