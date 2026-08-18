using Microsoft.EntityFrameworkCore;
using Nexora.Application.Interfaces.Repositories;
using Nexora.Domain.Entities;
using Nexora.Infrastructure.Data;

namespace Nexora.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly NexoraDbContext _context;

        public EmpresaRepository(NexoraDbContext context)
        {
            _context = context;
        }

        public async Task<Empresa?> ObterDadosEmpresaAsync()
        {
            // Como é um registro único institucional, pegamos sempre o primeiro
            return await _context.Set<Empresa>().FirstOrDefaultAsync();
        }

        public async Task<Empresa> CriarOuAtualizarAsync(Empresa empresa)
        {
            var existente = await ObterDadosEmpresaAsync();

            if (existente == null)
            {
                await _context.Set<Empresa>().AddAsync(empresa);
            }
            else
            {
                _context.Entry(existente).CurrentValues.SetValues(empresa);
            }

            await _context.SaveChangesAsync();
            return existente ?? empresa;
        }
    }
}