using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoClinica.Business;
using ProjetoClinica.Business.Models;

namespace ProjetoClinica.Data.Mappings
{
    public class FisioterapeutaMap : IEntityTypeConfiguration<Fisioterapeuta>
    {
        public void Configure(EntityTypeBuilder<Fisioterapeuta> builder)
        {
            builder.ToTable("Fisioterapeuta");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(150);
            builder.Property(x => x.Cpf).IsRequired().HasColumnName("Cpf").HasColumnType("VARCHAR").HasMaxLength(20);
            builder.Property(x => x.Rg).IsRequired().HasColumnName("Rg").HasColumnType("VARCHAR").HasMaxLength(20);
            builder.Property(x => x.Celular).IsRequired().HasColumnName("Celular").HasColumnType("VARCHAR").HasMaxLength(25);
            builder.Property(x => x.Especialidade).HasColumnName("Especialidade").HasColumnType("VARCHAR").HasMaxLength(50);
            builder.Property(x => x.Disponivel).IsRequired().HasColumnName("Disponivel").HasColumnType("BIT").HasMaxLength(1);
            builder.Property(x => x.Apagado).IsRequired().HasColumnName("Apagado").HasColumnType("BIT").HasMaxLength(1);

            // 1 : 1 => Fisioterapeuta : Endereco
            //builder.HasOne(f => f.Endereco)
            //    .WithOne(e => e.Fisioterapeuta);
        }
    }
}
