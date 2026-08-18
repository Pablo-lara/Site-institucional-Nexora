using System;
using System.Collections.Generic;
using System.Text;
using Nexora.Application.DTOs.Servicos;

namespace Nexora.Application.Interfaces.Services
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoResponseDto>> ObterTodosAsync();

        Task<ServicoResponseDto?> ObterPorIdAsync(int id);

        Task<ServicoResponseDto> CriarAsync(CriarServicoDto dto);

        Task<bool> AtualizarAsync(int id, AtualizarServicoDto dto);

        Task<bool> RemoverAsync(int id);
    }
}
