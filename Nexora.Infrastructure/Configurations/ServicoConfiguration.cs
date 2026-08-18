using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Configurations
{
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("Servicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Preco)
                .HasPrecision(18, 2);

            builder.Property(x => x.ImagemUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();
        }
    }
}
