using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly WmsDbContext _context;

        public AnnouncementRepository(WmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<Announcement?> GetByIdAsync(int id)
        {
            return await _context.Announcements
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AnnouncementId == id);
        }

        public async Task<Announcement> CreateAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();

            return announcement;
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Announcement announcement)
        {
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
        }
    }
}
