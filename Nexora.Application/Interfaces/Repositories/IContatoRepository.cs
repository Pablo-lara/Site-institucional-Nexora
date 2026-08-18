using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato?> ObterPorIdAsync(int id);
        Task<Contato> CriarAsync(Contato contato);
        Task AtualizarAsync(Contato contato);
        Task DeletarAsync(Contato contato);
    }
}