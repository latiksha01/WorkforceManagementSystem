using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Project
{
    public class UpdateProjectDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";
    }
}
