using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Announcement;

namespace WMS.Application.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync();

        Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id);

        Task<AnnouncementDto> CreateAnnouncementAsync(CreateAnnouncementDto dto);

        Task<bool> UpdateAnnouncementAsync(UpdateAnnouncementDto dto);

        Task<bool> DeleteAnnouncementAsync(int id);
    }
}
