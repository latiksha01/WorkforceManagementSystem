using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IUserLoginRepository
    {
        Task<IEnumerable<UserLogin>> GetAllAsync();

        Task<UserLogin?> GetByIdAsync(int id);

        Task<UserLogin?> GetByUsernameAsync(string username);

        Task<UserLogin> CreateAsync(UserLogin user);

        Task UpdateAsync(UserLogin user);

        Task DeleteAsync(UserLogin user);
    }
}
