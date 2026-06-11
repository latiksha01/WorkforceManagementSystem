using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.EmployeeProjectAllocation
{
    public class CreateEmployeeProjectAllocationDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateOnly AssignedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        public bool Status { get; set; } = true;
    }
}
