using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs.Auth
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
