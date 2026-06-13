using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WmsDbContext _context;

        public EmployeeRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
