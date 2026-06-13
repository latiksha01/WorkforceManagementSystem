using System;
using System.Collections.Generic;
using System.Text;
namespace WMS.Application.DTOs.UserLogin
{
    public class UserLoginDto
    {
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
