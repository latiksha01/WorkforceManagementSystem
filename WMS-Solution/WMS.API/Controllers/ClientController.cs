using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Client;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clientService.GetAllClientsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDto dto)
        {
            var performedBy =
                int.Parse(User.FindFirst("EmployeeId")!.Value);

            var client =
                await _clientService.CreateClientAsync(
                    dto,
                    performedBy
                );

            return CreatedAtAction(
                nameof(GetClientById),
                new { id = client.ClientId },
                client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(UpdateClientDto dto)
        {
            var performedBy =
                int.Parse(User.FindFirst("EmployeeId")!.Value);

            var result =
                await _clientService.UpdateClientAsync(
                    dto,
                    performedBy
                );

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var performedBy =
                int.Parse(User.FindFirst("EmployeeId")!.Value);

            var result =
                await _clientService.DeleteClientAsync(
                    id,
                    performedBy
                );

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}