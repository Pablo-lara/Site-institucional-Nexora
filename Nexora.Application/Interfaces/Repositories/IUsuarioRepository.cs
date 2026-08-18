using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario> CriarAsync(Usuario usuario);
    }
}