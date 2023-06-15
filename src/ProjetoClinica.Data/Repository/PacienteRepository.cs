using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.Data.Context;

namespace ProjetoClinica.Data.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ClinicaContext _context;

        public PacienteRepository(ClinicaContext clinicaContext)
        {
            _context = clinicaContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
        }

        public void Atualizar(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
        }

        public async Task<Paciente> ObterPorId(int? id)
        {
            return await _context.Pacientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.Apagado != true);
        }

        public async Task<IEnumerable<Paciente>> ObterTodos()
        {
            return await _context.Pacientes.Where(f => f.Apagado != true).AsNoTracking().ToListAsync();
        }

        public void Remover(int id)
        {
            var paciente = _context.Pacientes.FirstOrDefault(x => x.Id == id && x.Apagado != true);
            if (paciente != null)
            {
                paciente.Apagado = true;
                _context.Pacientes.Update(paciente);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Paciente> ObterPacienteEFisioterapeutaPorId(int? id)
        {
            var pacienteFisio =await _context.Pacientes.Include(p => p.Fisioterapeuta).FirstOrDefaultAsync(p=>p.Id == id);
            return pacienteFisio;
        }

        public async Task<Paciente> ObterPacienteEndereco(int? id)
        {
            return await _context.Pacientes.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
