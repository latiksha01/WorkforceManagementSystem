using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAsync();

        Task<Announcement?> GetByIdAsync(int id);

        Task<Announcement> CreateAsync(Announcement announcement);

        Task UpdateAsync(Announcement announcement);

        Task DeleteAsync(Announcement announcement);
    }
}
