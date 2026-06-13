using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WMS.Application.DTOs.Auth;
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

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
    ChangePasswordDto dto)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId =
                int.Parse(userIdClaim.Value);

            var result =
                await _authService.ChangePasswordAsync(
                    userId,
                    dto);

            if (!result)
            {
                return BadRequest(new
                {
                    message =
                        "Current password is incorrect or passwords do not match"
                });
            }

            return Ok(new
            {
                message = "Password changed successfully"
            });
        }
    }
}