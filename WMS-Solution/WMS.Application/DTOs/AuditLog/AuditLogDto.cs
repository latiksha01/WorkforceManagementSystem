using System;
using System.Collections.Generic;
using System.Text;
namespace WMS.Application.DTOs.AuditLog
{
    public class AuditLogDto
    {
        public int AuditId { get; set; }

        public string EntityName { get; set; } = string.Empty;

        public int RecordId { get; set; }

        public string Action { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
    }
}
