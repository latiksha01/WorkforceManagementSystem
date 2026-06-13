using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        [Key]
        public int AnnouncementId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Property

        [ForeignKey(nameof(CreatedBy))]
        public Employee? Employee { get; set; }
    }

}
