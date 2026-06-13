using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.AuditLog;

namespace WMS.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();

        Task<AuditLogDto?> GetAuditLogByIdAsync(int id);

        Task<AuditLogDto> CreateAuditLogAsync(CreateAuditLogDto dto);

        Task<bool> UpdateAuditLogAsync(UpdateAuditLogDto dto);

        Task<bool> DeleteAuditLogAsync(int id);

        Task LogAsync( string entityName, int recordId, string action, int createdBy);
    }
}
