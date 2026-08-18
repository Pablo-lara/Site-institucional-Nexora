using Nexora.Application.DTOs.Contatos;

namespace Nexora.Application.Interfaces.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<ContatoResponseDto>> ObterTodosAsync();
        Task<ContatoResponseDto?> ObterPorIdAsync(int id);
        Task<ContatoResponseDto> EnviarAsync(EnviarContatoDto dto);
        Task<bool> MarcarComoLidaAsync(int id);
        Task<bool> DeletarAsync(int id);
    }
}