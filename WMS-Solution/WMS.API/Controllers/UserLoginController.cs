using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.UserLogin;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginService _userService;

        public UserLoginController(IUserLoginService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserLoginDto dto)
        {
            var user = await _userService.CreateUserAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = user.UserId },
                user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserLoginDto dto)
        {
            var result = await _userService.UpdateUserAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}