using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public double? TotalHours { get; set; }

        [MaxLength(20)]
        public string? WorkMode { get; set; }

        [Required]
        public DateOnly AttendanceDate { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }
    }
}
