using Microsoft.EntityFrameworkCore;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly NexoraDbContext _context;

        public ContatoRepository(NexoraDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            return await _context.Set<Contato>()
                .OrderByDescending(c => c.DataEnvio)
                .ToListAsync();
        }

        public async Task<Contato?> ObterPorIdAsync(int id)
        {
            return await _context.Set<Contato>().FindAsync(id);
        }

        public async Task<Contato> CriarAsync(Contato contato)
        {
            await _context.Set<Contato>().AddAsync(contato);
            await _context.SaveChangesAsync();
            return contato;
        }

        public async Task AtualizarAsync(Contato contato)
        {
            _context.Set<Contato>().Update(contato);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Contato contato)
        {
            _context.Set<Contato>().Remove(contato);
            await _context.SaveChangesAsync();
        }
    }
}