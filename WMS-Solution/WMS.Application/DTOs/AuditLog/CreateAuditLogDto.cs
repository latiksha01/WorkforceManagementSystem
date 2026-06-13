using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.AuditLog
{
    public class CreateAuditLogDto
    {
        [Required]
        public string EntityName { get; set; } = string.Empty;

        [Required]
        public int RecordId { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }
    }
}
