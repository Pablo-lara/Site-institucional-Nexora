using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Data
{
    public class NexoraDbContext : DbContext
    {
        public NexoraDbContext(DbContextOptions<NexoraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public DbSet<Servico> Servicos => Set<Servico>();

        public DbSet<Projeto> Projetos => Set<Projeto>();

        public DbSet<Artigo> Artigos => Set<Artigo>();

        public DbSet<Contato> Contatos => Set<Contato>();

        public DbSet<Orcamento> Orcamentos => Set<Orcamento>();

        public DbSet<Empresa> Empresas => Set<Empresa>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(NexoraDbContext).Assembly);
        }
    }
}
