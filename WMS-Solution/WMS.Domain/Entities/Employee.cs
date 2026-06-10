using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public char Gender { get; set; }

        [Required]
        public DateOnly DOB { get; set; }

        [Required]
        public DateOnly DOJ { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation Properties
        public Department? Department { get; set; }

        public Role? Role { get; set; }
        public ICollection<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; } = new List<EmployeeProjectAllocation>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();

        public ICollection<Announcement> Announcements { get; set; }  = new List<Announcement>();

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

        public UserLogin? UserLogin { get; set; }
    }
}
