using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Leave
{
    public class CreateLeaveDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(30)]
        public string LeaveType { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Reason { get; set; }

        [Required]
        public DateOnly FromDate { get; set; }

        [Required]
        public DateOnly ToDate { get; set; }
    }
}
