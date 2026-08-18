using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IArtigoRepository
    {
        Task<IEnumerable<Artigo>> ObterTodosAsync();
        Task<IEnumerable<Artigo>> ObterPublicadosAsync();
        Task<Artigo?> ObterPorIdAsync(int id);
        Task<Artigo> CriarAsync(Artigo artigo);
        Task AtualizarAsync(Artigo artigo);
        Task DeletarAsync(Artigo artigo);
    }
}