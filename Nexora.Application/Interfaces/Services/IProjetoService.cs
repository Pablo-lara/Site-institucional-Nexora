using Nexora.Application.DTOs.Projetos;

namespace Nexora.Application.Interfaces.Services;

public interface IProjetoService
{
    Task<IEnumerable<ProjetoResponseDto>> ObterTodosAsync();

    Task<IEnumerable<ProjetoResponseDto>> ObterDestaquesAsync();

    Task<ProjetoResponseDto?> ObterPorIdAsync(int id);

    Task<ProjetoResponseDto> CriarAsync(CriarProjetoDto dto);

    Task<bool> AtualizarAsync(
        int id,
        AtualizarProjetoDto dto);

    Task<bool> RemoverAsync(int id);
}