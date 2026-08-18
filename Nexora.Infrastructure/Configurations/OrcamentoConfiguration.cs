using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Configurations
{
    public class OrcamentoConfiguration : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.ToTable("Orcamentos");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Servico)
                   .WithMany()
                   .HasForeignKey(x => x.ServicoId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.NomeCliente)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Telefone)
                .HasMaxLength(30);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(x => x.FaixaOrcamento)
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.DataSolicitacao)
                .IsRequired();
        }
    }
}
