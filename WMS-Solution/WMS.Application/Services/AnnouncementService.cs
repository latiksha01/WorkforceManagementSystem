using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Announcement;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();

            return announcements.Select(a => new AnnouncementDto
            {
                AnnouncementId = a.AnnouncementId,
                Title = a.Title,
                Message = a.Message,
                CreatedBy = a.CreatedBy,
                CreatedByName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                IsActive = a.IsActive,
                CreatedOn = a.CreatedOn
            });
        }

        public async Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id)
        {
            var a = await _announcementRepository.GetByIdAsync(id);

            if (a == null)
                return null;

            return new AnnouncementDto
            {
                AnnouncementId = a.AnnouncementId,
                Title = a.Title,
                Message = a.Message,
                CreatedBy = a.CreatedBy,
                CreatedByName = a.Employee != null
                    ? $"{a.Employee.FirstName} {a.Employee.LastName}"
                    : string.Empty,
                IsActive = a.IsActive,
                CreatedOn = a.CreatedOn
            };
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(CreateAnnouncementDto dto)
        {
            var announcement = new Announcement
            {
                Title = dto.Title,
                Message = dto.Message,
                CreatedBy = dto.CreatedBy,
                IsActive = true
            };

            var created = await _announcementRepository.CreateAsync(announcement);

            return await GetAnnouncementByIdAsync(created.AnnouncementId)
                   ?? throw new Exception("Announcement creation failed.");
        }

        public async Task<bool> UpdateAnnouncementAsync(UpdateAnnouncementDto dto)
        {
            var announcement = await _announcementRepository.GetByIdAsync(dto.AnnouncementId);

            if (announcement == null)
                return false;

            announcement.Title = dto.Title;
            announcement.Message = dto.Message;
            announcement.CreatedBy = dto.CreatedBy;
            announcement.IsActive = dto.IsActive;

            await _announcementRepository.UpdateAsync(announcement);

            return true;
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);

            if (announcement == null)
                return false;

            await _announcementRepository.DeleteAsync(announcement);

            return true;
        }
    }
}
