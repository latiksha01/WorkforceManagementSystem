using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Attendance
{
    public class CreateAttendanceDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public string? WorkMode { get; set; }

        [Required]
        public DateOnly AttendanceDate { get; set; }
    }
}