using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class Project : BaseEntity
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation Properties
        public Client? Client { get; set; }

        public ICollection<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }
            = new List<EmployeeProjectAllocation>();
    }
}
