using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
