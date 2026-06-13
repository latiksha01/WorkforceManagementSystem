using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();

        Task<AuditLog?> GetByIdAsync(int id);

        Task<AuditLog> CreateAsync(AuditLog auditLog);

        Task UpdateAsync(AuditLog auditLog);

        Task DeleteAsync(AuditLog auditLog);


    }
}
