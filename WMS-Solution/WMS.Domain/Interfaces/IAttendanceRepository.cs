using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance> CreateAsync(Attendance attendance);

        Task UpdateAsync(Attendance attendance);

        Task DeleteAsync(Attendance attendance);
    }
}
