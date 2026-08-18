using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly NexoraDbContext _context;

        public ServicoRepository(NexoraDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servico>> ObterTodosAsync()
        {
            return await _context.Servicos
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<Servico?> ObterPorIdAsync(int id)
        {
            return await _context.Servicos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Servico> CriarAsync(Servico servico)
        {
            await _context.Servicos.AddAsync(servico);

            await _context.SaveChangesAsync();

            return servico;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            return await _context.Servicos.AnyAsync(x => x.Nome == nome);
        }

        public async Task AtualizarAsync(Servico servico)
        {
            _context.Servicos.Update(servico);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Servico servico)
        {
            _context.Servicos.Remove(servico);

            await _context.SaveChangesAsync();
        }
    }
}
