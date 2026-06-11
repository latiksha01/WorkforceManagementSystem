using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Attendance;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();

            return attendances.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                AttendanceDate = a.AttendanceDate,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut,
                TotalHours = a.TotalHours,
                WorkMode = a.WorkMode,
                CreatedOn = a.CreatedOn
            });
        }

        public async Task<AttendanceDto?> GetAttendanceByIdAsync(int id)
        {
            var a = await _attendanceRepository.GetByIdAsync(id);

            if (a == null)
                return null;

            return new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                AttendanceDate = a.AttendanceDate,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut,
                TotalHours = a.TotalHours,
                WorkMode = a.WorkMode,
                CreatedOn = a.CreatedOn
                
            };
        }

        public async Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto dto)
        {
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                AttendanceDate = dto.AttendanceDate,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                WorkMode = dto.WorkMode,
                TotalHours = dto.CheckOut.HasValue
        ? (dto.CheckOut.Value - dto.CheckIn).TotalHours
        : null
            };

            var created = await _attendanceRepository.CreateAsync(attendance);

            return await GetAttendanceByIdAsync(created.AttendanceId)
                   ?? throw new Exception("Attendance creation failed.");
        }

        public async Task<bool> UpdateAttendanceAsync(UpdateAttendanceDto dto)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(dto.AttendanceId);

            if (attendance == null)
                return false;

            attendance.EmployeeId = dto.EmployeeId;
            attendance.AttendanceDate = dto.AttendanceDate;
            attendance.CheckIn = dto.CheckIn;
            attendance.CheckOut = dto.CheckOut;
            attendance.WorkMode = dto.WorkMode;

            attendance.TotalHours = dto.CheckOut.HasValue
                ? (dto.CheckOut.Value - dto.CheckIn).TotalHours
                : null;

            await _attendanceRepository.UpdateAsync(attendance);

            return true;
        }

        public async Task<bool> DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
                return false;

            await _attendanceRepository.DeleteAsync(attendance);

            return true;
        }
    }
}
