using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Employee
{
    public class CreateEmployeeDto
    {
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

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string Status { get; set; } = "Active";
    }
}
