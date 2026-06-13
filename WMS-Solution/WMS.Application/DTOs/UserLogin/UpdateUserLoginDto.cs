using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.UserLogin
{
    public class UpdateUserLoginDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }
    }
}
