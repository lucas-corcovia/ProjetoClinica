using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Data.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasColumnName("Email").HasColumnType("VARCHAR").HasMaxLength(50);
            builder.Property(x => x.Senha).IsRequired().HasColumnName("Senha").HasColumnType("VARCHAR").HasMaxLength(50);
            builder.Property(x => x.Apagado).IsRequired().HasColumnName("Apagado").HasColumnType("BIT").HasMaxLength(1);
        }
    }
}
