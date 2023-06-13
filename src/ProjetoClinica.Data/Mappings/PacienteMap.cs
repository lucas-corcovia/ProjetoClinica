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
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(150);
            builder.Property(x => x.Cpf).IsRequired().HasColumnName("Cpf").HasColumnType("VARCHAR").HasMaxLength(20);
            builder.Property(x => x.Idade).IsRequired().HasColumnName("Idade").HasColumnType("INT").HasMaxLength(10);
            builder.Property(x => x.Genero).IsRequired().HasColumnName("Genero").HasColumnType("CHAR").HasMaxLength(2);
            builder.Property(x => x.Telefone).IsRequired().HasColumnName("Telefone").HasColumnType("VARCHAR").HasMaxLength(35);
            builder.Property(x => x.ProfissaoAtual).IsRequired().HasColumnName("ProfissaoAtual").HasColumnType("VARCHAR").HasMaxLength(150);
            builder.Property(x => x.ProfissaoAnterior).IsRequired().HasColumnName("ProfissaoAnterior").HasColumnType("VARCHAR").HasMaxLength(150);
            builder.Property(x => x.Diagnostico).HasColumnName("Diagnostico").HasColumnType("VARCHAR").HasMaxLength(250);
            builder.Property(x => x.TratamentosAnteriores).HasColumnName("TratamentosAnteriores").HasColumnType("VARCHAR").HasMaxLength(250);
            builder.Property(x => x.Descricao).HasColumnName("Descricao").HasColumnType("VARCHAR").HasMaxLength(300);
            builder.Property(x => x.Foto).IsRequired(false).HasColumnName("Foto").HasColumnType("LONGBLOB");
            builder.Property(x => x.Apagado).IsRequired().HasColumnName("Apagado").HasColumnType("BIT").HasMaxLength(1);

            builder.HasOne(p => p.Fisioterapeuta)
                       .WithMany(f => f.Pacientes)
                       .HasForeignKey(p => p.FisioterapeutaId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
