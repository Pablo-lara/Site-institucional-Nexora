using Microsoft.EntityFrameworkCore;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly NexoraDbContext _context;

        public OrcamentoRepository(NexoraDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orcamento>> ObterTodosAsync()
        {
            return await _context.Set<Orcamento>()
                .Include(o => o.Servico)
                .OrderByDescending(o => o.DataSolicitacao)
                .ToListAsync();
        }

        public async Task<Orcamento?> ObterPorIdAsync(int id)
        {
            return await _context.Set<Orcamento>()
                .Include(o => o.Servico)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Orcamento> CriarAsync(Orcamento orcamento)
        {
            await _context.Set<Orcamento>().AddAsync(orcamento);
            await _context.SaveChangesAsync();
            return orcamento;
        }

        public async Task AtualizarAsync(Orcamento orcamento)
        {
            _context.Set<Orcamento>().Update(orcamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Orcamento orcamento)
        {
            _context.Set<Orcamento>().Remove(orcamento);
            await _context.SaveChangesAsync();
        }
    }
}