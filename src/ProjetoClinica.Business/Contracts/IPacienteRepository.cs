using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Contracts
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<IEnumerable<Paciente>> ObterTodos();
        Task<Paciente> ObterPorId(int? id);
        Task<Paciente> ObterPacienteEFisioterapeutaPorId(int? id);
        void Adicionar(Paciente paciente);
        void Atualizar(Paciente paciente);
        void Remover(int id);
    }
}
