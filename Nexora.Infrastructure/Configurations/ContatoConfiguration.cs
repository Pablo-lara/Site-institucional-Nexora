using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contatos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Telefone)
                .HasMaxLength(30);

            builder.Property(x => x.Assunto)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Mensagem)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(x => x.Lido)
                .IsRequired();

            builder.Property(x => x.DataEnvio)
                .IsRequired();
        }
    }
}
