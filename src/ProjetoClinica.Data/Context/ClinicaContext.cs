using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.Data.Mappings;

namespace ProjetoClinica.Data.Context
{
    public class ClinicaContext : DbContext, IUnitOfWork
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options)
           : base(options) { }

        public DbSet<Fisioterapeuta> Fisioterapeutas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FisioterapeutaMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
        }
    }
}
