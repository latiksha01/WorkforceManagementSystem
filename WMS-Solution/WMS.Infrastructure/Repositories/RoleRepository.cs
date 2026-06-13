using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WmsDbContext _context;

        public RoleRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
        }
    }
}
