using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly WmsDbContext _context;

        public UserLoginRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserLogin>> GetAllAsync()
        {
            return await _context.UserLogins
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<UserLogin?> GetByIdAsync(int id)
        {
            return await _context.UserLogins
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<UserLogin?> GetByUsernameAsync(string username)
        {
            return await _context.UserLogins
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserLogin> CreateAsync(UserLogin user)
        {
            await _context.UserLogins.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(UserLogin user)
        {
            _context.UserLogins.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserLogin user)
        {
            _context.UserLogins.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
