using System;
using System.Collections.Generic;
using System.Text;
using WMS.Application.DTOs.Client;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                ClientAddress = c.ClientAddress,
                ClientPhoneNumber = c.ClientPhoneNumber,
                ClientLocation = c.ClientLocation,
                Status = c.Status,
                CreatedOn = c.CreatedOn
            });
        }

        public async Task<ClientDto?> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return null;

            return new ClientDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientAddress = client.ClientAddress,
                ClientPhoneNumber = client.ClientPhoneNumber,
                ClientLocation = client.ClientLocation,
                Status = client.Status,
                CreatedOn = client.CreatedOn
            };
        }

        public async Task<ClientDto> CreateClientAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                ClientName = dto.ClientName,
                ClientAddress = dto.ClientAddress,
                ClientPhoneNumber = dto.ClientPhoneNumber,
                ClientLocation = dto.ClientLocation,
                Status = dto.Status
            };

            var createdClient = await _clientRepository.CreateAsync(client);

            return new ClientDto
            {
                ClientId = createdClient.ClientId,
                ClientName = createdClient.ClientName,
                ClientAddress = createdClient.ClientAddress,
                ClientPhoneNumber = createdClient.ClientPhoneNumber,
                ClientLocation = createdClient.ClientLocation,
                Status = createdClient.Status,
                CreatedOn = createdClient.CreatedOn
            };
        }

        public async Task<bool> UpdateClientAsync(UpdateClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(dto.ClientId);

            if (client == null)
                return false;

            client.ClientName = dto.ClientName;
            client.ClientAddress = dto.ClientAddress;
            client.ClientPhoneNumber = dto.ClientPhoneNumber;
            client.ClientLocation = dto.ClientLocation;
            client.Status = dto.Status;

            await _clientRepository.UpdateAsync(client);

            return true;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return false;

            await _clientRepository.DeleteAsync(client);

            return true;
        }
    }
}