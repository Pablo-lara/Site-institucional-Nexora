using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities;

namespace Nexora.Infrastructure.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Telefone)
                .HasMaxLength(30);

            builder.Property(x => x.Endereco)
                .HasMaxLength(300);

            builder.Property(x => x.Instagram)
                .HasMaxLength(300);

            builder.Property(x => x.Linkedin)
                .HasMaxLength(300);

            builder.Property(x => x.Github)
                .HasMaxLength(300);

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(500);
        }
    }
}
