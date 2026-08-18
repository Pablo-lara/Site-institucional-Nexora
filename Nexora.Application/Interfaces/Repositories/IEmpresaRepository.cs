using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa?> ObterDadosEmpresaAsync();
        Task<Empresa> CriarOuAtualizarAsync(Empresa empresa);
    }
}