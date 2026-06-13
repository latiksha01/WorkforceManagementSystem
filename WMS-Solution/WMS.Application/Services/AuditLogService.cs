using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.AuditLog;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
        {
            var logs = await _auditLogRepository.GetAllAsync();

            return logs.Select(a => new AuditLogDto
            {
                AuditId = a.AuditId,
                EntityName = a.EntityName,
                RecordId = a.RecordId,
                Action = a.Action,
                CreatedBy = a.CreatedBy,
                CreatedByName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                CreatedOn = a.CreatedOn
            });
        }

        public async Task<AuditLogDto?> GetAuditLogByIdAsync(int id)
        {
            var a = await _auditLogRepository.GetByIdAsync(id);

            if (a == null)
                return null;

            return new AuditLogDto
            {
                AuditId = a.AuditId,
                EntityName = a.EntityName,
                RecordId = a.RecordId,
                Action = a.Action,
                CreatedBy = a.CreatedBy,
                CreatedByName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                CreatedOn = a.CreatedOn
            };
        }

        public async Task<AuditLogDto> CreateAuditLogAsync(CreateAuditLogDto dto)
        {
            var log = new AuditLog
            {
                EntityName = dto.EntityName,
                RecordId = dto.RecordId,
                Action = dto.Action,
                CreatedBy = dto.CreatedBy
            };

            var created = await _auditLogRepository.CreateAsync(log);

            return await GetAuditLogByIdAsync(created.AuditId)
                   ?? throw new Exception("AuditLog creation failed.");
        }

        public async Task<bool> UpdateAuditLogAsync(UpdateAuditLogDto dto)
        {
            var log = await _auditLogRepository.GetByIdAsync(dto.AuditId);

            if (log == null)
                return false;

            log.EntityName = dto.EntityName;
            log.RecordId = dto.RecordId;
            log.Action = dto.Action;
            log.CreatedBy = dto.CreatedBy;

            await _auditLogRepository.UpdateAsync(log);

            return true;
        }

        public async Task<bool> DeleteAuditLogAsync(int id)
        {
            var log = await _auditLogRepository.GetByIdAsync(id);

            if (log == null)
                return false;

            await _auditLogRepository.DeleteAsync(log);

            return true;
        }
        public async Task LogAsync(
            string entityName,
            int recordId,
            string action,
            int createdBy)
        {
            var log = new AuditLog
            {
                EntityName = entityName,
                RecordId = recordId,
                Action = action,
                CreatedBy = createdBy
            };

            await _auditLogRepository.CreateAsync(log);
        }
    }
}
