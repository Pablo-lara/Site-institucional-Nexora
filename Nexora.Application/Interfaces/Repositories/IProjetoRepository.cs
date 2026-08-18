using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories;

public interface IProjetoRepository
{
    Task<IEnumerable<Projeto>> ObterTodosAsync();

    Task<IEnumerable<Projeto>> ObterDestaquesAsync();

    Task<Projeto?> ObterPorIdAsync(int id);

    Task<bool> ExistePorNomeAsync(string nome);

    Task<Projeto> CriarAsync(Projeto projeto);

    Task AtualizarAsync(Projeto projeto);

    Task RemoverAsync(Projeto projeto);
}