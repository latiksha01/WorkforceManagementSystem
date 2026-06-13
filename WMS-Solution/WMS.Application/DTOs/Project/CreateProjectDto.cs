using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Project
{
    public class CreateProjectDto
    {
        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string Status { get; set; } = "Active";
    }
}
