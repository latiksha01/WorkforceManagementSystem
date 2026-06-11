using System;
using System.Collections.Generic;
using System.Text;
namespace WMS.Application.DTOs.Project
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public int? ClientId { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
    }
}
