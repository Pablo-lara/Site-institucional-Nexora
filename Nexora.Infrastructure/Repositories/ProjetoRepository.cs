using Microsoft.EntityFrameworkCore;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories;

public class ProjetoRepository : IProjetoRepository
{
    private readonly NexoraDbContext _context;

    public ProjetoRepository(NexoraDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Projeto>> ObterTodosAsync()
    {
        return await _context.Projetos
            .AsNoTracking()
            .OrderByDescending(x => x.DataCriacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Projeto>> ObterDestaquesAsync()
    {
        return await _context.Projetos
            .AsNoTracking()
            .Where(x => x.Destaque && x.Ativo)
            .OrderByDescending(x => x.DataCriacao)
            .ToListAsync();
    }

    public async Task<Projeto?> ObterPorIdAsync(int id)
    {
        return await _context.Projetos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistePorNomeAsync(string nome)
    {
        return await _context.Projetos
            .AnyAsync(x => x.Nome == nome);
    }

    public async Task<Projeto> CriarAsync(Projeto projeto)
    {
        await _context.Projetos.AddAsync(projeto);

        await _context.SaveChangesAsync();

        return projeto;
    }

    public async Task AtualizarAsync(Projeto projeto)
    {
        _context.Projetos.Update(projeto);

        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Projeto projeto)
    {
        _context.Projetos.Remove(projeto);

        await _context.SaveChangesAsync();
    }
}