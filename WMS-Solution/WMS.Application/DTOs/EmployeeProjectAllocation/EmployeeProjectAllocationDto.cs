using System;
using System.Collections.Generic;
using System.Text;
namespace WMS.Application.DTOs.EmployeeProjectAllocation
{
    public class EmployeeProjectAllocationDto
    {
        public int AllocationId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public DateOnly AssignedOn { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
    }
}
