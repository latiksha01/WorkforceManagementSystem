using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMS.Application.DTOs.Auth;
using WMS.Application.DTOs.UserLogin;
using WMS.Application.Interfaces;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserLoginRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserLoginRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                return null;

            user.LastLogin = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds);

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                Role = user.Role?.RoleName ?? ""
            };
        }
    }
}
