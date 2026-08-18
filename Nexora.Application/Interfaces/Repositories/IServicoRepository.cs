using System;
using System.Collections.Generic;
using System.Text;
using Nexora.Domain.Entities;

namespace Nexora.Application.Interfaces.Repositories
{
    public interface IServicoRepository
    {
        Task<IEnumerable<Servico>> ObterTodosAsync();

        Task<Servico?> ObterPorIdAsync(int id);

        Task<Servico> CriarAsync(Servico servico);

        Task<bool> ExistePorNomeAsync(string nome);

        Task AtualizarAsync(Servico servico);

        Task RemoverAsync(Servico servico);

        
    }
}
