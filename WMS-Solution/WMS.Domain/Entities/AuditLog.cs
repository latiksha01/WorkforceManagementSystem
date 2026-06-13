using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WMS.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public string EntityName { get; set; } = string.Empty;

        [Required]
        public int RecordId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Action { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        // Navigation Property
        [ForeignKey(nameof(CreatedBy))]
        public Employee? Employee { get; set; }
    }
}
