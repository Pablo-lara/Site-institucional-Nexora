using Nexora.Application.DTOs.Empresa;

namespace Nexora.Application.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaResponseDto?> ObterInformacoesAsync();
        Task<EmpresaResponseDto> SalvarInformacoesAsync(AtualizarEmpresaDto dto);
    }
}