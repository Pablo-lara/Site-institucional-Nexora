using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Configurations;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("Projetos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ImagemUrl)
            .HasMaxLength(500);

        builder.Property(x => x.UrlProjeto)
            .HasMaxLength(500);

        builder.Property(x => x.Tecnologias)
            .HasMaxLength(500);

        builder.Property(x => x.Destaque)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Property(x => x.DataAtualizacao)
            .IsRequired(false);
    }
}