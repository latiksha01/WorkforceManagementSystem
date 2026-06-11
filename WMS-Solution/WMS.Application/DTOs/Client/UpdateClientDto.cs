using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Client
{
    public class UpdateClientDto
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; } = string.Empty;

        public string? ClientAddress { get; set; }

        public long? ClientPhoneNumber { get; set; }

        [MaxLength(20)]
        public string? ClientLocation { get; set; }

        public bool Status { get; set; }
    }
}
