using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Client;

namespace WMS.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();

        Task<ClientDto?> GetClientByIdAsync(int id);

        Task<ClientDto> CreateClientAsync(
    CreateClientDto dto,
    int performedBy);

        Task<bool> UpdateClientAsync(
            UpdateClientDto dto,
            int performedBy);

        Task<bool> DeleteClientAsync(
            int id,
            int performedBy);
    }
}
