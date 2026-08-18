using Nexora.Application.DTOs.Usuarios;

namespace Nexora.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> AutenticarAsync(LoginDto dto);
        Task InitializarAdminPadraoAsync(); // Gera o admin inicial caso não exista no banco
    }
}