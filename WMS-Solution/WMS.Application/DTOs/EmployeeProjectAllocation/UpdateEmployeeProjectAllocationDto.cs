using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.EmployeeProjectAllocation
{
    public class UpdateEmployeeProjectAllocationDto
    {
        [Required]
        public int AllocationId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateOnly AssignedOn { get; set; }

        public bool Status { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
