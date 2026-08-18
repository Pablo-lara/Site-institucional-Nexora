using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexora.Application.DTOs.Usuarios;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Application.Interfaces.Services;
using Nexora.Domain.Entities;
using Nexora.Domain.Enums;

namespace Nexora.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> AutenticarAsync(LoginDto dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            if (usuario == null) return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);
            if (!senhaValida) return null;

            var token = GerarJwtToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil.ToString()
            };
        }

        public async Task InitializarAdminPadraoAsync()
        {
            var adminExistente = await _usuarioRepository.ObterPorEmailAsync("admin@nexora.com");
            if (adminExistente == null)
            {
                var adminPadrao = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@nexora.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Perfil = PerfilUsuario.Admin,
                    DataCriacao = DateTime.UtcNow
                };

                await _usuarioRepository.CriarAsync(adminPadrao);
            }
        }

        private string GerarJwtToken(Usuario usuario)
        {
            var jwtKey = _configuration["Jwt:SecretKey"] ?? "MinhaChaveSuperSecretaECompridaNexora2026!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "NexoraAPI",
                audience: _configuration["Jwt:Audience"] ?? "NexoraFrontend",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}