using Nexora.Application.DTOs.Orcamentos;

namespace Nexora.Application.Interfaces.Services
{
    public interface IOrcamentoService
    {
        Task<IEnumerable<OrcamentoResponseDto>> ObterTodosAsync();
        Task<OrcamentoResponseDto?> ObterPorIdAsync(int id);
        Task<OrcamentoResponseDto> SolicitarAsync(SolicitarOrcamentoDto dto);
        Task<bool> AtualizarStatusAsync(int id, AtualizarStatusOrcamentoDto dto);
        Task<bool> DeletarAsync(int id);
    }
}