using System;
using System.Collections.Generic;
using System.Text;
using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();

        Task<Client?> GetByIdAsync(int id);

        Task<Client> CreateAsync(Client client);

        Task UpdateAsync(Client client);

        Task DeleteAsync(Client client);
    }
}
