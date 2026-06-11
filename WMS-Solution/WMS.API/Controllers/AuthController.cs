using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.UserLogin;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDto dto)
        {
            var result =
                await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(
                    new { message = "Invalid credentials" });

            return Ok(result);
        }
    }
}