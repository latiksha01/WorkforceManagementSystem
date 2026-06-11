using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Announcement
{
    public class UpdateAnnouncementDto
    {
        [Required]
        public int AnnouncementId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
