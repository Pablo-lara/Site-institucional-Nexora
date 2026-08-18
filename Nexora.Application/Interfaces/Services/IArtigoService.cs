using Nexora.Application.DTOs.Artigos;

namespace Nexora.Application.Interfaces.Services
{
    public interface IArtigoService
    {
        Task<IEnumerable<ArtigoResponseDto>> ObterTodosAsync();
        Task<IEnumerable<ArtigoResponseDto>> ObterPublicadosAsync();
        Task<ArtigoResponseDto?> ObterPorIdAsync(int id);
        Task<ArtigoResponseDto> CriarAsync(CriarArtigoDto dto);
        Task<bool> AtualizarAsync(int id, AtualizarArtigoDto dto);
        Task<bool> DeletarAsync(int id);

    }
}