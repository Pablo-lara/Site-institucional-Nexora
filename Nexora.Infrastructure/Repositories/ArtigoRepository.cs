using Microsoft.EntityFrameworkCore;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories
{
    public class ArtigoRepository : IArtigoRepository
    {
        private readonly NexoraDbContext _context;

        public ArtigoRepository(NexoraDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artigo>> ObterTodosAsync()
        {
            return await _context.Set<Artigo>()
                .OrderByDescending(a => a.DataPublicacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Artigo>> ObterPublicadosAsync()
        {
            return await _context.Set<Artigo>()
                .Where(a => a.Publicado)
                .OrderByDescending(a => a.DataPublicacao)
                .ToListAsync();
        }

        public async Task<Artigo?> ObterPorIdAsync(int id)
        {
            return await _context.Set<Artigo>().FindAsync(id);
        }

        public async Task<Artigo> CriarAsync(Artigo artigo)
        {
            await _context.Set<Artigo>().AddAsync(artigo);
            await _context.SaveChangesAsync();
            return artigo;
        }

        public async Task AtualizarAsync(Artigo artigo)
        {
            _context.Set<Artigo>().Update(artigo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Artigo artigo)
        {
            _context.Set<Artigo>().Remove(artigo);
            await _context.SaveChangesAsync();
        }
    }
}