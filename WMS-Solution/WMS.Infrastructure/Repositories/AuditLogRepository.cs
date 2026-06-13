using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly WmsDbContext _context;

        public AuditLogRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<AuditLog?> GetByIdAsync(int id)
        {
            return await _context.AuditLogs
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AuditId == id);
        }

        public async Task<AuditLog> CreateAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            return auditLog;
        }

        public async Task UpdateAsync(AuditLog auditLog)
        {
            _context.AuditLogs.Update(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuditLog auditLog)
        {
            _context.AuditLogs.Remove(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}
