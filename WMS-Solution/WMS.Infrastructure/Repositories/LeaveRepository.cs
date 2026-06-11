using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly WmsDbContext _context;

        public LeaveRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Leave>> GetAllAsync()
        {
            return await _context.Leaves
                .Include(l => l.Employee)
                .Include(l => l.ApprovedByEmployee)
                .ToListAsync();
        }

        public async Task<Leave?> GetByIdAsync(int id)
        {
            return await _context.Leaves
                .Include(l => l.Employee)
                .Include(l => l.ApprovedByEmployee)
                .FirstOrDefaultAsync(l => l.LeaveId == id);
        }

        public async Task<Leave> CreateAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();

            return leave;
        }

        public async Task UpdateAsync(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Leave leave)
        {
            _context.Leaves.Remove(leave);
            await _context.SaveChangesAsync();
        }
    }
}
