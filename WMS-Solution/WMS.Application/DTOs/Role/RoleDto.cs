using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs.Role
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
