using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Business.Models;

namespace ProjetoClinica.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Logradouro)
               .IsRequired(false)
               .HasColumnType("VARCHAR").HasMaxLength(200);

            builder.Property(c => c.Numero)
                .IsRequired(false)
                .HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(c => c.Cep)
                .IsRequired(false)
                .HasColumnType("VARCHAR").HasMaxLength(8);

            builder.Property(c => c.Complemento)
                .HasColumnType("VARCHAR").HasMaxLength(250);

            builder.Property(c => c.Bairro)
                .IsRequired(false)
                .HasColumnType("VARCHAR").HasMaxLength(100);

            builder.Property(c => c.Cidade)
                .IsRequired(false)
                .HasColumnType("VARCHAR").HasMaxLength(100);

            builder.Property(c => c.Estado)
                .IsRequired(false)
                .HasColumnType("VARCHAR").HasMaxLength(50);
        }
    }
}
