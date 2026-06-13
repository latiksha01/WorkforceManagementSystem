using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class EmployeeProjectAllocationRepository : IEmployeeProjectAllocationRepository
    {
        private readonly WmsDbContext _context;

        public EmployeeProjectAllocationRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeProjectAllocation>> GetAllAsync()
        {
            return await _context.EmployeeProjectAllocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .ToListAsync();
        }

        public async Task<EmployeeProjectAllocation?> GetByIdAsync(int id)
        {
            return await _context.EmployeeProjectAllocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(a => a.AllocationId == id);
        }

        public async Task<EmployeeProjectAllocation> CreateAsync(EmployeeProjectAllocation allocation)
        {
            await _context.EmployeeProjectAllocations.AddAsync(allocation);
            await _context.SaveChangesAsync();

            return allocation;
        }

        public async Task UpdateAsync(EmployeeProjectAllocation allocation)
        {
            _context.EmployeeProjectAllocations.Update(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeeProjectAllocation allocation)
        {
            _context.EmployeeProjectAllocations.Remove(allocation);
            await _context.SaveChangesAsync();
        }
    }
}
