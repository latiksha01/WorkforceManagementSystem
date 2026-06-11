using System;
using System.Collections.Generic;
using System.Text;
namespace WMS.Application.DTOs.Announcement
{
    public class AnnouncementDto
    {
        public int AnnouncementId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
