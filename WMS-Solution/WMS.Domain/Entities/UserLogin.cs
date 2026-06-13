using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities
{
    public class UserLogin : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public DateTime? LastLogin { get; set; }

        // Navigation Properties
        public Employee? Employee { get; set; }

        public Role? Role { get; set; }
    }
}
