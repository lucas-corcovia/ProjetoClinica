using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjetoClinica.Business.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PacienteService(IPacienteRepository pacienteRepository, IEnderecoRepository enderecoRepository)
        {
            _pacienteRepository = pacienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task AdicionarPaciente(Paciente paciente, Endereco endereco)
        {           
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _pacienteRepository.Adicionar(paciente);
                await _pacienteRepository.UnitOfWork.Commit();

                endereco.PacienteId = paciente.Id;
                _enderecoRepository.Adicionar(endereco);
                await _enderecoRepository.UnitOfWork.Commit();

                transaction.Complete();
            }
        }

        public async Task AtualizarPaciente(Paciente paciente)
        {
            _pacienteRepository.Atualizar(paciente);
            await _pacienteRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {       
           _enderecoRepository.Atualizar(endereco);
            await _enderecoRepository.UnitOfWork.Commit();
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
