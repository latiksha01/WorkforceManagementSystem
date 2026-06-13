using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class EmployeeProjectAllocation : BaseEntity
    {
        [Key]
        public int AllocationId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateOnly AssignedOn { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = string.Empty;

        public bool Status { get; set; } = true;

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation Properties
        public Employee? Employee { get; set; }

        public Project? Project { get; set; }
    }
}
