using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? UpdatedOn { get; set; }
    }
}
