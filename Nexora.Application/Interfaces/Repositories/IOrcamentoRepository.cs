using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IOrcamentoRepository
    {
        Task<IEnumerable<Orcamento>> ObterTodosAsync();
        Task<Orcamento?> ObterPorIdAsync(int id);
        Task<Orcamento> CriarAsync(Orcamento orcamento);
        Task AtualizarAsync(Orcamento orcamento);
        Task DeletarAsync(Orcamento orcamento);
    }
}