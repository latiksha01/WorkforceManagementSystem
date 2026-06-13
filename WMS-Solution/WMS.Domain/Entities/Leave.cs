using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace WMS.Domain.Entities
{
    public class Leave : BaseEntity
    {
        [Key]
        public int LeaveId { get; set; }

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

        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime AppliedOn { get; set; } = DateTime.Now;

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public Employee? ApprovedByEmployee { get; set; }
    }
}
