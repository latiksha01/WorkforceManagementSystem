using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly WmsDbContext _context;

        public DepartmentRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await _context.Departments.AddAsync(department);

            await _context.SaveChangesAsync();

            return department;
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();
        }
    }
}
