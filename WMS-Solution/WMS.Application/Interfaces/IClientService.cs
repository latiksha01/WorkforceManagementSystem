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

        Task<ClientDto> CreateClientAsync(CreateClientDto dto);

        Task<bool> UpdateClientAsync(UpdateClientDto dto);

        Task<bool> DeleteClientAsync(int id);
    }
}
