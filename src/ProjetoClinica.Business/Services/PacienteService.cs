using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task AdicionarPaciente(Paciente paciente)
        {
            _pacienteRepository.Adicionar(paciente);
            await _pacienteRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarPaciente(Paciente paciente)
        {
            _pacienteRepository.Atualizar(paciente);
            await _pacienteRepository.UnitOfWork.Commit();
        }

        public async Task<Paciente> ObterPorId(int? id)
        {
            return await _pacienteRepository.ObterPorId(id);
        }

        public async Task<Paciente> ObterPacienteEFisioterapeutaPorId(int? id)
        {
            return await _pacienteRepository.ObterPacienteEFisioterapeutaPorId(id);
        }

        public async Task<IEnumerable<Paciente>> ObterTodos()
        {
            return await _pacienteRepository.ObterTodos();
        }

        public async Task RemoverPaciente(int id)
        {
            _pacienteRepository.Remover(id);
            await _pacienteRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _pacienteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }       
    }
}
